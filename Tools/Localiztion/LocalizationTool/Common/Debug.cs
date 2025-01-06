namespace LocalizationTool
{
    public static class Debug
    {
        public static void LogProcess(string message)
        {
            var lastColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = lastColor;
        }

        public static void LogError(string message)
        {
            var lastColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = lastColor;
        }

        public static void LogInfo(string message)
        {
            var lastColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ForegroundColor = lastColor;
        }
    }
}