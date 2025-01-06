namespace Game
{
    public abstract class UI2DNode : UI2D
    {
        private string _className;
        public string ClassName
        {
            get
            {
                if (string.IsNullOrEmpty(_className))
                {
                    _className = GetType().FullName;
                }
                return _className;
            }
        }
        public override bool IsShow
        {
            get => _isShow;
            protected set
            {
                _isShow = value;
                if (_isShow)
                {
                    OnShow();
                }
                else
                {
                    OnHide();
                }
            }
        }
    }
}
