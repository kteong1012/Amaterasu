using Cysharp.Threading.Tasks;
using Game.Log;
using Jing.TurbochargedScrollList;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Game
{
    public enum GMButtonActionType
    {
        Simple,
        OneParam,
        TwoParam,
    }
    public class GMButtonData
    {
        public string name;
        public GMButtonActionType actionType;
        public Action<string[]> onClick;
        public string[] paramTexts;
    }

    [UI2D("UIGMPanel")]
    public class UIGMPanel : UI2DPanel
    {
        public ScrollList buttonScrollList;

        private List<GMButtonData> _buttonDatas = new List<GMButtonData>();

        protected override void OnCreate()
        {
            buttonScrollList.SetAsGrid();
            buttonScrollList.onRenderItem = OnRenderItem;
        }

        protected override void OnShow()
        {
            // _buttonDatas.Clear();
            //
            //
            //
            // buttonScrollList.UpdateData(_buttonDatas);
        }

        private bool CheckIsInt(string text, out int result, bool needMessagebox = true)
        {
            if (!int.TryParse(text, out result))
            {
                if (needMessagebox)
                {
                    MessageBox.ShowOk("角色ID必须是整数").Forget();
                }
                return false;
            }
            return true;
        }

        private void AddSimpleButton(string name, Action onClick, string[] paramTexts = null, bool closeGMPanel = true)
        {
            _buttonDatas.Add(new GMButtonData()
            {
                name = name,
                actionType = GMButtonActionType.Simple,
                onClick = (param) =>
                {
                    onClick();
                    if (closeGMPanel)
                    {
                        Close();
                    }
                },
                paramTexts = paramTexts
            });
        }

        private void AddOneParamButton(string name, Action<string> onClick, string[] paramTexts = null, bool closeGMPanel = true)
        {
            _buttonDatas.Add(new GMButtonData()
            {
                name = name,
                actionType = GMButtonActionType.OneParam,
                onClick = (param) =>
                {
                    onClick(param[0]);
                    if (closeGMPanel)
                    {
                        Close();
                    }
                },
                paramTexts = paramTexts
            });
        }

        private void AddTwoParamButton(string name, Action<string, string> onClick, string[] paramTexts = null, bool closeGMPanel = true)
        {
            _buttonDatas.Add(new GMButtonData()
            {
                name = name,
                actionType = GMButtonActionType.TwoParam,
                onClick = (param) =>
                {
                    onClick(param[0], param[1]);
                    if (closeGMPanel)
                    {
                        Close();
                    }
                },
                paramTexts = paramTexts
            });
        }

        private void OnRenderItem(ScrollListItem item, object data, bool isFresh)
        {
            var cell = item as UIGMButtonCell;
            var buttonData = data as GMButtonData;

            cell.SetData(buttonData);
        }
    }
}
