using TMPro;

namespace Game
{
    public interface ILocalizationHandler
    {
        TMP_FontAsset GetFont();
        string GetText(string key, string defaultText = null);

    }
}
