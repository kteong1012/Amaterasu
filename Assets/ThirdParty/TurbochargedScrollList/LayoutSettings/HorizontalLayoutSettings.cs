﻿using System;

namespace Jing.TurbochargedScrollList
{
    [Serializable]
    public class HorizontalLayoutSettings
    {
        public enum Alignment
        {
            Left,
            Center,
            Right,
        }

        public Alignment alignment = Alignment.Left;
        /// <summary>
        /// 距离左边的内边距
        /// </summary>
        public float paddingLeft = 0;

        /// <summary>
        /// 距离右边的内边距
        /// </summary>
        public float paddingRight = 0;

        /// <summary>
        /// 列表项间距
        /// </summary>
        public float gap = 0;
    }
}
