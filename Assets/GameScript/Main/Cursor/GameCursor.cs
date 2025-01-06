using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public enum CursorType
    {
        Basic_Normal,
        State_Attack,
        State_Heal,
        State_Invalid
    }


    public class GameCursor
    {
        private static Dictionary<CursorType, Texture2D> _cursorTextureDict = new Dictionary<CursorType, Texture2D>();

        public static CursorType CurrentCursorType { get; private set; }

        public static void Init()
        {
            _cursorTextureDict.Add(CursorType.Basic_Normal, Resources.Load<Texture2D>("Cursors/basic_01"));
            _cursorTextureDict.Add(CursorType.State_Attack, Resources.Load<Texture2D>("Cursors/Bonus_15"));
            _cursorTextureDict.Add(CursorType.State_Heal, Resources.Load<Texture2D>("Cursors/Bonus_10"));
            _cursorTextureDict.Add(CursorType.State_Invalid, Resources.Load<Texture2D>("Cursors/Bonus_44"));
        }

        public static void SetCursorType(CursorType cursorType)
        {
            CurrentCursorType = cursorType;
            Cursor.SetCursor(_cursorTextureDict[cursorType], new Vector2(10.5f, 2.24f), CursorMode.Auto);
        }

        public static void SetCursorVisible(bool visible)
        {
            Cursor.visible = visible;
        }
    }
}
