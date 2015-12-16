using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace PawnPlus.Core
{
    public enum EventKey : byte
    {
        /// <summary>
        /// Event raised when a docked document is changed.
        /// </summary>
        ActiveDocumentChanged,

        /// <summary>
        /// Event raised when the caret position is changed.
        /// </summary>
        CaretPositionChanged,

        /// <summary>
        /// Event raised when the compilation is canceled.
        /// </summary>
        CompilationCanceled,

        /// <summary>
        /// Event raised when the compilation pending cancellation.
        /// </summary>
        CompilationCanceling,

        /// <summary>
        /// Event raised when the compilation is completed.
        /// </summary>
        CompilationCompleted,

        /// <summary>
        /// Event raised when the compilation has started.
        /// </summary>
        CompilationStarted,

        /// <summary>
        /// Event raised when the compilation is about to start.
        /// </summary>
        CompilationStarting,

        /// <summary>
        /// Event raised when the download progress is changed.
        /// </summary>
        DownloadProgressChanged,

        /// <summary>
        /// Event raised when the download progress is completed.
        /// </summary>
        DownloadProgressComplete,

        /// <summary>
        /// Event raised when a project item is added.
        /// </summary>
        ItemAdded,

        /// <summary>
        /// Event raised when a project item is deleted.
        /// </summary>
        ItemDeleted,

        /// <summary>
        /// Event raised when a project item is renamed.
        /// </summary>
        ItemRenamed,

        /// <summary>
        /// Event raised when a plugin is loaded.
        /// </summary>
        PluginLoaded,

        /// <summary>
        /// Event raised when the project is closed.
        /// </summary>
        ProjectClosed,

        /// <summary>
        /// Event raised when the project is opened.
        /// </summary>
        ProjectOpened,

        /// <summary>
        /// Event raised when application status is changed.
        /// </summary>
        StatusChanged,

        /// <summary>
        /// Event raised when text is about to be copied.
        /// </summary>
        TextCopying,

        /// <summary>
        /// Event raised when text is about to be cutted.
        /// </summary>
        TextCutting
    }

    public static class EventStorage
    {
        private static Dictionary<EventKey, List<dynamic>> listeners = new Dictionary<EventKey, List<dynamic>>();

        /// <summary>
        /// Add a function as listener for an event.
        /// </summary>
        /// <typeparam name="T1">Type of the sender.</typeparam>
        /// <typeparam name="T2">Type of the arguments.</typeparam>
        /// <param name="eventKey">Event key.</param>
        /// <param name="callback">Function to be added as listener.</param>
        public static void AddListener<T1, T2>(EventKey eventKey, Action<T1, T2> callback)
        {
            // Check if we have the key in our list.
            if (listeners.ContainsKey(eventKey) == true)
            {
                // Add the callback to list. 
                listeners[eventKey].Add(callback);
            }
            else
            {
                // Create a new list with this callback.
                listeners.Add(eventKey, new List<dynamic> { callback });
            }
        }

        /// <summary>
        /// Remove listener for an event.
        /// </summary>
        /// <typeparam name="T1">Type of the sender.</typeparam>
        /// <typeparam name="T2">Type of the arguments.</typeparam>
        /// <param name="eventKey">Event key.</param>
        /// <param name="callback">Function to be removed.</param>
        public static void RemoveListener<T1, T2>(EventKey eventKey, Action<T1, T2> callback)
        {
            // Check if we have the key in our list.
            if (listeners.ContainsKey(eventKey) == true)
            {
                foreach (Action<T1, T2> currentCallback in listeners[eventKey].ToList())
                {
                    if (currentCallback == callback)
                    {
                        // Remove the callback from the list if we found it.
                        listeners[eventKey].Remove(currentCallback);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Raise an event.
        /// </summary>
        /// <param name="eventKey">Event key.</param>
        /// <param name="arguments">Arguments for the event.</param>
        /// <returns></returns>
        internal static void Fire(EventKey eventKey, params object[] arguments)
        {
            if (listeners.ContainsKey(eventKey) == true)
            {
                foreach (Delegate function in listeners[eventKey])
                {
                    if (function != null)
                    {
                        // Invoke the function.
                        function.DynamicInvoke(arguments);
                    }
                }
            }
        }
    }
}