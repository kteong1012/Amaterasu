namespace Game
{
    public class UI2DAttribute : System.Attribute
    {
        public UI2DPanelInfo info;

        public UI2DAttribute(string prefabPath)
        {
            info = new UI2DPanelInfo
            {
                prefabPath = prefabPath
            };
        }
    }
}
