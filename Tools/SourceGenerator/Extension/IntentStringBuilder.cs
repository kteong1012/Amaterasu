using System.Text;

namespace Analyzer.src.Extension
{
    public class IntentStringBuilder
    {
        private Intent _intent;
        private StringBuilder _sb;

        public IntentStringBuilder(int depth)
        {
            _intent = new Intent(depth);
            _sb = new StringBuilder();
        }

        public void Append(string value)
        {
            _sb.Append(value);
        }

        public void AppendLine(string value)
        {
            _sb.Append(_intent.ToString());
            _sb.AppendLine(value);
        }

        public void ApplineEmptyLine()
        {
            _sb.AppendLine();
        }

        public override string ToString()
        {
            return _sb.ToString();
        }

        //++
        public static IntentStringBuilder operator ++(IntentStringBuilder builder)
        {
            builder._intent++;
            return builder;
        }

        //--
        public static IntentStringBuilder operator --(IntentStringBuilder builder)
        {
            builder._intent--;
            return builder;
        }
    }
}
