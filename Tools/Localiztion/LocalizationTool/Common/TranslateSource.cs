namespace LocalizationTool
{
    internal class TranslateSource
    {
        public string key;
        public string text;

        public override bool Equals(object obj)
        {
            if (obj is TranslateSource other)
            {
                return key == other.key && text == other.text;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return key.GetHashCode() ^ text.GetHashCode();
        }
    }
}