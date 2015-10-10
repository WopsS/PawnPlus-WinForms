using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace PawnPlus.Language
{
    public static class LanguageManager
    {
        private static CultureInfo cultureInfo;
        private static ResourceManager resourceManager;

        /// <summary>
        /// Get a language text from resources.
        /// </summary>
        /// <param name="item">Language item to get.</param>
        /// <returns>Returns text.</returns>
        public static string GetText(LanguageEnum item)
        {
            if (cultureInfo.CompareInfo != Thread.CurrentThread.CurrentCulture.CompareInfo)
            {
                SetLanguage(cultureInfo);
            }

            return resourceManager.GetString(GetDescription(item));
        }

        /// <summary>
        /// Set language culture for the application.
        /// </summary>
        /// <param name="cultureInfo"><see cref="CultureInfo"/> to be set.</param>
        public static void SetLanguage(CultureInfo cultureInfo)
        {
            LanguageManager.cultureInfo = cultureInfo;
            resourceManager = new ResourceManager(string.Format("PawnPlus.Language.Languages.{0}", cultureInfo.ToString()), typeof(LanguageManager).Assembly); // Use this way because I don't want "satellite assembly" for language.

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }

        /// <summary>
        /// Get description for an enum value.
        /// </summary>
        /// <param name="item">Value of the enum.</param>
        /// <returns>Returns description of the value.</returns>
        private static string GetDescription(LanguageEnum item)
        {
            FieldInfo fieldInfo = item.GetType().GetField(item.ToString());
            DescriptionAttribute[] descriptionAttribute = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (descriptionAttribute.Length > 0)
            {
                return descriptionAttribute[0].Description;
            }

            return item.ToString();
        }
    }
}
