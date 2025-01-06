namespace Game
{
    public struct UI2DInfo
    {
        public string prefabPath;
    }
    public class UI2DAttribute : System.Attribute
    {
        public UI2DInfo info;

        public UI2DAttribute(string prefabPath)
        {
            info = new UI2DInfo
            {
                prefabPath = prefabPath
            };
        }
    }
}
