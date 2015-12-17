using PawnPlus.Core.Exceptions;
using System;
using System.Globalization;
using System.Resources;

namespace PawnPlus.Core
{
    public static class Localization
    {
        public static CultureInfo Culture 
		{ 
			get
			{
				return resourceCulture;
			}
			 
			set
			{
				resourceCulture = value;

				try
				{
					resourceManager = new ResourceManager(string.Format("PawnPlus.Core.Localizations.{0}", value), typeof(Localization).Assembly);

					// Check if the resource is valid.
					resourceManager.GetString("");
				}
				catch (Exception ex)
				{
					Logger.Write(ex);

					// If there is a problem with resource, change it to english.
					resourceManager = new ResourceManager(string.Format("PawnPlus.Core.Localizations.en-US", value), typeof(Localization).Assembly);

					// Show the exception to user.
					ExceptionHandler.HandledException(ex);
				}
			}
		}

		private static CultureInfo resourceCulture;

        private static ResourceManager resourceManager = new ResourceManager("PawnPlus.Core.Localizations.en-US", typeof(Localization).Assembly);

		/// <summary>
		/// How a button with "Cancel" text will be displayed.
		/// </summary>
		public static string Text_Cancel
		{
			get
			{
				return resourceManager.GetString("Text_Cancel", Culture);
			}
		}

		/// <summary>
		/// How a button with "OK" text will be displayed.
		/// </summary>
		public static string Text_OK
		{
			get
			{
				return resourceManager.GetString("Text_OK", Culture);
			}
		}

		/// <summary>
		/// How "gamemode" word will be shown.
		/// </summary>
		public static string Text_Gamemode
		{
			get
			{
				return resourceManager.GetString("Text_Gamemode", Culture);
			}
		}

		/// <summary>
		/// How "include" word will be shown.
		/// </summary>
		public static string Text_Include
		{
			get
			{
				return resourceManager.GetString("Text_Include", Culture);
			}
		}

		/// <summary>
		/// How "filterscript" word will be shown.
		/// </summary>
		public static string Text_Filterscript
		{
			get
			{
				return resourceManager.GetString("Text_Filterscript", Culture);
			}
		}

		/// <summary>
		/// Label to show "Options:" in "Compiler Options" form.
		/// </summary>
		public static string Text_Options
		{
			get
			{
				return resourceManager.GetString("Text_Options", Culture);
			}
		}

		public static string Text_CaseSensitive
		{
			get
			{
				return resourceManager.GetString("Text_CaseSensitive", Culture);
			}
		}

		public static string Text_FindPrevious
		{
			get
			{
				return resourceManager.GetString("Text_FindPrevious", Culture);
			}
		}

		public static string Text_ReplaceAll
		{
			get
			{
				return resourceManager.GetString("Text_ReplaceAll", Culture);
			}
		}

		public static string Text_WholeWord
		{
			get
			{
				return resourceManager.GetString("Text_WholeWord", Culture);
			}
		}

		public static string Text_FindWhat
		{
			get
			{
				return resourceManager.GetString("Text_FindWhat", Culture);
			}
		}

		/// <summary>
		/// Used for "Look In" combo box in "Find and Replace" form.
		/// </summary>
		public static string Text_AllDocuments
		{
			get
			{
				return resourceManager.GetString("Text_AllDocuments", Culture);
			}
		}

		/// <summary>
		/// Used for "Look In" combo box in "Find and Replace" form.
		/// </summary>
		public static string Text_CurrentDocument
		{
			get
			{
				return resourceManager.GetString("Text_CurrentDocument", Culture);
			}
		}

		/// <summary>
		/// Used for "Look In" combo box's label in "Find and Replace" form.
		/// </summary>
		public static string Text_LookIn
		{
			get
			{
				return resourceManager.GetString("Text_LookIn", Culture);
			}
		}

		/// <summary>
		/// Name for "Find and Replace" form.
		/// </summary>
		public static string Name_FindReplace
		{
			get
			{
				return resourceManager.GetString("Name_FindReplace", Culture);
			}
		}

		public static string Text_ReplaceWith
		{
			get
			{
				return resourceManager.GetString("Text_ReplaceWith", Culture);
			}
		}

		public static string Text_Find
		{
			get
			{
				return resourceManager.GetString("Text_Find", Culture);
			}
		}

		public static string Text_Replace
		{
			get
			{
				return resourceManager.GetString("Text_Replace", Culture);
			}
		}

		/// <summary>
		/// Label in "Go To Line" form.
		/// </summary>
		public static string Text_LineNumber
		{
			get
			{
				return resourceManager.GetString("Text_LineNumber", Culture);
			}
		}

		/// <summary>
		/// Name for "Go To Line" form.
		/// </summary>
		public static string Name_GoToLine
		{
			get
			{
				return resourceManager.GetString("Name_GoToLine", Culture);
			}
		}

		public static string Text_CheckingFiles
		{
			get
			{
				return resourceManager.GetString("Text_CheckingFiles", Culture);
			}
		}

		public static string Text_CompilerFilesCopied
		{
			get
			{
				return resourceManager.GetString("Text_CompilerFilesCopied", Culture);
			}
		}

		public static string Text_ServerFilesCopied
		{
			get
			{
				return resourceManager.GetString("Text_ServerFilesCopied", Culture);
			}
		}

		public static string Text_CompilerFilesCopying
		{
			get
			{
				return resourceManager.GetString("Text_CompilerFilesCopying", Culture);
			}
		}

		public static string Text_ServerFilesCopying
		{
			get
			{
				return resourceManager.GetString("Text_ServerFilesCopying", Culture);
			}
		}

		public static string Text_CompilerFilesDownloading
		{
			get
			{
				return resourceManager.GetString("Text_CompilerFilesDownloading", Culture);
			}
		}

		public static string Text_ServerFilesDownloading
		{
			get
			{
				return resourceManager.GetString("Text_ServerFilesDownloading", Culture);
			}
		}

		public static string Text_CompilerFilesError
		{
			get
			{
				return resourceManager.GetString("Text_CompilerFilesError", Culture);
			}
		}

		public static string Text_ServerFilesError
		{
			get
			{
				return resourceManager.GetString("Text_ServerFilesError", Culture);
			}
		}

		public static string Text_CompilerFilesUnpacking
		{
			get
			{
				return resourceManager.GetString("Text_CompilerFilesUnpacking", Culture);
			}
		}

		public static string Text_ServerFilesUnpacking
		{
			get
			{
				return resourceManager.GetString("Text_ServerFilesUnpacking", Culture);
			}
		}

		public static string Text_VersionChecking
		{
			get
			{
				return resourceManager.GetString("Text_VersionChecking", Culture);
			}
		}

		public static string Text_VersionSettings
		{
			get
			{
				return resourceManager.GetString("Text_VersionSettings", Culture);
			}
		}

		public static string Text_Starting
		{
			get
			{
				return resourceManager.GetString("Text_Starting", Culture);
			}
		}

		public static string Text_Build
		{
			get
			{
				return resourceManager.GetString("Text_Build", Culture);
			}
		}

		public static string Text_Compile
		{
			get
			{
				return resourceManager.GetString("Text_Compile", Culture);
			}
		}

		public static string Text_CompilerOptions
		{
			get
			{
				return resourceManager.GetString("Text_CompilerOptions", Culture);
			}
		}

		public static string Text_Edit
		{
			get
			{
				return resourceManager.GetString("Text_Edit", Culture);
			}
		}

		public static string Text_Copy
		{
			get
			{
				return resourceManager.GetString("Text_Copy", Culture);
			}
		}

		public static string Text_Cut
		{
			get
			{
				return resourceManager.GetString("Text_Cut", Culture);
			}
		}

		public static string Text_FindNext
		{
			get
			{
				return resourceManager.GetString("Text_FindNext", Culture);
			}
		}

		public static string Text_GoTo
		{
			get
			{
				return resourceManager.GetString("Text_GoTo", Culture);
			}
		}

		public static string Text_Paste
		{
			get
			{
				return resourceManager.GetString("Text_Paste", Culture);
			}
		}

		public static string Text_Redo
		{
			get
			{
				return resourceManager.GetString("Text_Redo", Culture);
			}
		}

		public static string Text_Undo
		{
			get
			{
				return resourceManager.GetString("Text_Undo", Culture);
			}
		}

		public static string Text_File
		{
			get
			{
				return resourceManager.GetString("Text_File", Culture);
			}
		}

		public static string Text_Close
		{
			get
			{
				return resourceManager.GetString("Text_Close", Culture);
			}
		}

		public static string Text_CloseProject
		{
			get
			{
				return resourceManager.GetString("Text_CloseProject", Culture);
			}
		}

		public static string Text_Exit
		{
			get
			{
				return resourceManager.GetString("Text_Exit", Culture);
			}
		}

		public static string Text_New
		{
			get
			{
				return resourceManager.GetString("Text_New", Culture);
			}
		}

		public static string Text_Open
		{
			get
			{
				return resourceManager.GetString("Text_Open", Culture);
			}
		}

		public static string Text_Project
		{
			get
			{
				return resourceManager.GetString("Text_Project", Culture);
			}
		}

		/// <summary>
		/// It must have a formatter. Text for "Save" button.
		/// </summary>
		public static string Text_Save
		{
			get
			{
				return resourceManager.GetString("Text_Save", Culture);
			}
		}

		public static string Text_SaveAll
		{
			get
			{
				return resourceManager.GetString("Text_SaveAll", Culture);
			}
		}

		/// <summary>
		/// It must have a formatter. Text for "Save As" button.
		/// </summary>
		public static string Text_SaveAs
		{
			get
			{
				return resourceManager.GetString("Text_SaveAs", Culture);
			}
		}

		/// <summary>
		/// Version of application.
		/// </summary>
		public static string Status_Version
		{
			get
			{
				return resourceManager.GetString("Status_Version", Culture);
			}
		}

		/// <summary>
		/// It must have a formatter. Text for "Column" label in status bar.
		/// </summary>
		public static string Status_Column
		{
			get
			{
				return resourceManager.GetString("Status_Column", Culture);
			}
		}

		/// <summary>
		/// It must have a formatter. Text for "Line" label in status bar.
		/// </summary>
		public static string Status_Line
		{
			get
			{
				return resourceManager.GetString("Status_Line", Culture);
			}
		}

		public static string Text_Browse
		{
			get
			{
				return resourceManager.GetString("Text_Browse", Culture);
			}
		}

		public static string Text_Create
		{
			get
			{
				return resourceManager.GetString("Text_Create", Culture);
			}
		}

		public static string Error_PathEmpty
		{
			get
			{
				return resourceManager.GetString("Error_PathEmpty", Culture);
			}
		}

		public static string Error_EmptyName
		{
			get
			{
				return resourceManager.GetString("Error_EmptyName", Culture);
			}
		}

		public static string Text_Name
		{
			get
			{
				return resourceManager.GetString("Text_Name", Culture);
			}
		}

		public static string Text_Path
		{
			get
			{
				return resourceManager.GetString("Text_Path", Culture);
			}
		}

		public static string Text_Type
		{
			get
			{
				return resourceManager.GetString("Text_Type", Culture);
			}
		}

		public static string Status_Ready
		{
			get
			{
				return resourceManager.GetString("Status_Ready", Culture);
			}
		}

		public static string Status_Compiled
		{
			get
			{
				return resourceManager.GetString("Status_Compiled", Culture);
			}
		}

		public static string Status_CompiledError
		{
			get
			{
				return resourceManager.GetString("Status_CompiledError", Culture);
			}
		}

		public static string Status_Compiling
		{
			get
			{
				return resourceManager.GetString("Status_Compiling", Culture);
			}
		}

		public static string Status_EmptyText
		{
			get
			{
				return resourceManager.GetString("Status_EmptyText", Culture);
			}
		}

		public static string Status_SavingFiles
		{
			get
			{
				return resourceManager.GetString("Status_SavingFiles", Culture);
			}
		}

		public static string Text_SelectedItem
		{
			get
			{
				return resourceManager.GetString("Text_SelectedItem", Culture);
			}
		}

		public static string Text_Delete
		{
			get
			{
				return resourceManager.GetString("Text_Delete", Culture);
			}
		}

		public static string Text_Rename
		{
			get
			{
				return resourceManager.GetString("Text_Rename", Culture);
			}
		}

		public static string Text_ShowInExplorer
		{
			get
			{
				return resourceManager.GetString("Text_ShowInExplorer", Culture);
			}
		}

		public static string Text_Add
		{
			get
			{
				return resourceManager.GetString("Text_Add", Culture);
			}
		}

		public static string Text_Folder
		{
			get
			{
				return resourceManager.GetString("Text_Folder", Culture);
			}
		}

		/// <summary>
		/// Name for "Project Explorer" form.
		/// </summary>
		public static string Name_ProjectExplorer
		{
			get
			{
				return resourceManager.GetString("Name_ProjectExplorer", Culture);
			}
		}

		/// <summary>
		/// Name for "Output" form.
		/// </summary>
		public static string Name_Output
		{
			get
			{
				return resourceManager.GetString("Name_Output", Culture);
			}
		}

		public static string Text_LoadingPlugins
		{
			get
			{
				return resourceManager.GetString("Text_LoadingPlugins", Culture);
			}
		}

		/// <summary>
		/// Used to show "Plugin {0} loaded." when a plugin is loaded.
		/// </summary>
		public static string Text_PluginLoaded
		{
			get
			{
				return resourceManager.GetString("Text_PluginLoaded", Culture);
			}
		}

		public static string Exception_InvalidPlugin
		{
			get
			{
				return resourceManager.GetString("Exception_InvalidPlugin", Culture);
			}
		}

		public static string Text_UnhandledException
		{
			get
			{
				return resourceManager.GetString("Text_UnhandledException", Culture);
			}
		}

		public static string Exception_ProjectFileNotFound
		{
			get
			{
				return resourceManager.GetString("Exception_ProjectFileNotFound", Culture);
			}
		}

		public static string Exception_FileNotFound
		{
			get
			{
				return resourceManager.GetString("Exception_FileNotFound", Culture);
			}
		}

		public static string Text_HandledExceptionOccurred
		{
			get
			{
				return resourceManager.GetString("Text_HandledExceptionOccurred", Culture);
			}
		}

		/// <summary>
		/// Name for "ExceptionForm" form.
		/// </summary>
		public static string Name_HandledException
		{
			get
			{
				return resourceManager.GetString("Name_HandledException", Culture);
			}
		}

		/// <summary>
		/// Name for "ExceptionForm" form.
		/// </summary>
		public static string Name_UnhandledException
		{
			get
			{
				return resourceManager.GetString("Name_UnhandledException", Culture);
			}
		}

		/// <summary>
		/// Name for "ExceptionForm" form.
		/// </summary>
		public static string Name_UIException
		{
			get
			{
				return resourceManager.GetString("Name_UIException", Culture);
			}
		}

		public static string Text_FatalWindowsForms
		{
			get
			{
				return resourceManager.GetString("Text_FatalWindowsForms", Culture);
			}
		}

		/// <summary>
		/// Text for log file.
		/// </summary>
		public static string Text_UnhandledExceptionOccured
		{
			get
			{
				return resourceManager.GetString("Text_UnhandledExceptionOccured", Culture);
			}
		}

		public static string Text_HandledException
		{
			get
			{
				return resourceManager.GetString("Text_HandledException", Culture);
			}
		}

		public static string Text_UIException
		{
			get
			{
				return resourceManager.GetString("Text_UIException", Culture);
			}
		}

			}
		}