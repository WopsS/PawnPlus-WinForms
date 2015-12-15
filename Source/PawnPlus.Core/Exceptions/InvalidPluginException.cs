using System;

namespace PawnPlus.Core.Exceptions
{
    public class InvalidPluginException : Exception
    {
        public InvalidPluginException()
        {
        }

        public InvalidPluginException(string pluginName) : base(string.Format(Localization.Exception_InvalidPlugin, pluginName))
        {
        }
    }
}
