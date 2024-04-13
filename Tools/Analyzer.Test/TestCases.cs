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
            {
                a = 2;
            }
        }
    }
    public class TestLOG001
    {
        public class TestGameService : GameService
        {

        }
    }

    public class TestLOG002
    {
        [GameService(GameServiceLifeSpan.None)]
        public class TestGameService : GameService
        {

        }
    }
}
#endif
