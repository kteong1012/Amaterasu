namespace Game
{
    [System.Flags]
    public enum UI2DPanelOptions
    {
        None = 0,
        NotPlayOpenAnim = 1,
        NotPlayCloseAnim = 2,
        NotPlayAnim = NotPlayOpenAnim | NotPlayCloseAnim,
        AllowMultiOpen = 4,
    }
}
