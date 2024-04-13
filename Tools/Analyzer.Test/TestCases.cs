#define TEST_ON
#if TEST_ON

using Game;

namespace Analyzer.Test
{
    public class TestSTD001
    {
        public void Test()
        {
            int a = 1;
            if (a == 1)
                a = 2;
        }
    }
    public class TestSTD002
    {
        public class TestGameService : GameService
        {

        }
    }

}
#endif
