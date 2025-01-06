using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public abstract class UIMessageBoxPanel : UI2DPanel
    {
        protected class MessageBoxData
        {
            public string Title;
            public string Message;
            public string OkText;
            public string CancelText;
            public object[] ExtraDatas;
            public IUniTaskSource TCS;
        }

        public override UI2DPanelLayer Layer => UI2DPanelLayer.Popup;

        public override UI2DPanelOptions Options => UI2DPanelOptions.None;

        private Queue<MessageBoxData> _dataQueue = new Queue<MessageBoxData>();

        protected MessageBoxData _currentData;

        protected abstract void ShowBoxView();

        public void OpenMessageBox(string title, string message, string okText, string cancelText, IUniTaskSource tcs, params object[] extraDatas)
        {
            var data = new MessageBoxData
            {
                Title = title,
                Message = message,
                OkText = okText,
                CancelText = cancelText,
                ExtraDatas = extraDatas,
                TCS = tcs
            };
            _dataQueue.Enqueue(data);
            if (_dataQueue.Count == 1)
            {
                Next();
            }
        }

        protected void Next()
        {
            if (!_dataQueue.TryDequeue(out var data))
            {
                Close();
                return;
            }
            _currentData = data;
            ShowBoxView();
        }
    }

    public enum OkCancelResult
    {
        Ok,
        Cancel
    }
    public static class MessageBox
    {
        public static async UniTask ShowOk(string message, string title = null, string okText = null)
        {
            okText = string.IsNullOrEmpty(okText) ? "确定".Translate() : okText;
            var tcs = new UniTaskCompletionSource();
            var panel = SSS.Get<UIService>().OpenPanel<UIMessageBoxOKPanel>();
            panel.OpenMessageBox(title, message, okText, null, tcs);
            await tcs.Task;
        }

        public static async UniTask<OkCancelResult> ShowOkCancel(string message, string title = null, string okText = null, string cancelText = null)
        {
            okText = string.IsNullOrEmpty(okText) ? "确定".Translate() : okText;
            cancelText = string.IsNullOrEmpty(cancelText) ? "取消".Translate() : cancelText;
            var tcs = new UniTaskCompletionSource<OkCancelResult>();
            var panel = SSS.Get<UIService>().OpenPanel<UIMessageBoxOkCancelPanel>();
            panel.OpenMessageBox(title, message, okText, cancelText, tcs);
            return await tcs.Task;
        }
    }
}
