namespace Game
{
    public static class StringExtensions
    {
        public static bool IsInvalidFileName(string fileName)
        {
            if (fileName.IsNullOrWhiteSpace())
            {
                return true;
            }
            return fileName.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) >= 0;
        }

        public static string Translate(this string text, string defaultText = null)
        {
            return Localization.LocalizationHandler.GetText(text, defaultText);
        }
    }
}