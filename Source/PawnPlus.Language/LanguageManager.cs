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

        public static string GetText(LanguageEnum Item)
        {
            if (cultureInfo.CompareInfo != Thread.CurrentThread.CurrentCulture.CompareInfo)
            {
                SetLanguage(cultureInfo);
            }
            
            return resourceManager.GetString(GetDescription(Item));
        }

        public static void SetLanguage(CultureInfo cultureInfo)
        {
            LanguageManager.cultureInfo = cultureInfo;
            resourceManager = new ResourceManager(string.Format("PawnPlus.Language.Languages.{0}", cultureInfo.ToString()), typeof(LanguageManager).Assembly); // Use this way because I don't want "satellite assembly" for language.

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }

        private static string GetDescription(LanguageEnum Item)
        {
            FieldInfo fieldInfo = Item.GetType().GetField(Item.ToString());
            DescriptionAttribute[] descriptionAttribute = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (descriptionAttribute.Length > 0)
            {
                return descriptionAttribute[0].Description;
            }

            return Item.ToString();
        }
    }
}
