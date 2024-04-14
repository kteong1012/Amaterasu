//#define TEST_ON
#if TEST_ON

using Game;
using YooAsset;

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

    public class TestSTD002
    {
        public int A;
        private int b;

        public int C { get; set; }
        public int D { get; private set; }

        struct TestStruct
        {
            public int A;
            private int b;
            protected int C { get; set; }
            int D {  get; set; }
        }
    }

    public class TestLOG001
    {
        [GameService(GameServiceLifeSpan.Game)]
        public class TestGameService : GameService
        {

        }
    }

    public class TestLOG002
    {
    }

    public class TestLOG003
    {
        public void Test()
        {
            YooAsset.YooAssets.LoadSceneAsync("test");
        }
    }
}
#endif
