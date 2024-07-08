namespace Analyzer.src.Extension
{
    public class Intent
    {
        private int _depth;
        public Intent(int depth)
        {
            _depth = depth;
        }

        public override string ToString()
        {
            return new string('\t', _depth);
        }

        //++
        public static Intent operator ++(Intent intent)
        {
            intent._depth++;
            return intent;
        }

        //--
        public static Intent operator --(Intent intent)
        {
            intent._depth--;
            return intent;
        }
    }
}
