using System.Globalization;
using System.Threading;

namespace PawnPlus.Language
{
    public static class Manager
    {
        /// <summary>
        /// Set language culture for the application.
        /// </summary>
        /// <param name="cultureInfo"><see cref="CultureInfo"/> to be set.</param>
        public static void Set(CultureInfo cultureInfo)
        {
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
    }
}
