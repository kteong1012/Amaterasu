#define TEST_ON
#if TEST_ON

using System;
using Game;
using UnityEngine;
using YooAsset;

namespace Analyzer.Test
{
    //public class TestSTD001
    //{
    //    public void Test()
    //    {
    //        int a = 1;
    //        if (a == 1)
    //            a = 2;
    //    }
    //}

    //public class TestSTD002
    //{
    //    public int A;
    //    private int b;

    //    public int C { get; set; }
    //    public int D { get; private set; }

    //    struct TestStruct
    //    {
    //        public int A;
    //        private int b;
    //        protected int C { get; set; }
    //        int D { get; set; }
    //    }
    //}

    //public class TestLOG001
    //{
    //    [GameService(GameServiceDomain.Game)]
    //    public class TestGameService : GameService
    //    {

    //    }
    //}

    //public class TestLOG002
    //{
    //}

    //public class TestLOG003
    //{
    //    public void Test()
    //    {
    //        YooAsset.YooAssets.LoadSceneAsync("test");
    //    }
    //}

    //public class TestLOG004
    //{
    //    public void Test()
    //    {
    //        Debug.Log("test");
    //    }
    //}

    public class TestLOG005
    {
        public void Test()
        {
            int a = 1;
            int b = 5;
            int c = 3 + 5;
            string s = "abc";
            string ss = null;
            bool b1 = true;
        }
    }
}
#endif
