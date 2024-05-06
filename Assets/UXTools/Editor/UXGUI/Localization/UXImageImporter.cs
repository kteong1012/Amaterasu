using UnityEditor;
using UnityEngine;
using ThunderFireUITool;
using System.IO;
using System.Linq;

namespace UnityEngine.UI
{
    class MyTexturePostprocessor : AssetPostprocessor
    {
        void OnPreprocessTexture()
        {
            if (assetPath.Contains(UXGUIConfig.LocalizationFolder) && SwitchSetting.CheckValid(SwitchSetting.SwitchType.AutoConvertTex))
            {
                TextureImporter importer = (TextureImporter)assetImporter;
                importer.textureType = TextureImporterType.Sprite;
            }
        }
    }

    public class UXImageImporter
    {
        [MenuItem(ThunderFireUIToolConfig.Menu_Localization + "/导入图片包 (Import Localization Images)", false, 54)]
        private static void ImportImages()
        {
            string path = Utils.SelectFolder(false);
            if (path != null)
            {
                string[] files = Directory.GetFiles(path, "*.png", SearchOption.AllDirectories);
                foreach (int j in UXGUIConfig.availableLanguages)
                {
                    Directory.CreateDirectory(UXGUIConfig.LocalizationFolder + UXImage.suffix[j]);
                }
                foreach (string file in files)
                {
                    string fileName = file.Split('\\', '/').Last();
                    if (fileName.Length < 7) continue;
                    string ext = fileName.Substring(fileName.Length - 7, 3);
                    foreach (int j in UXGUIConfig.availableLanguages)
                    {
                        if (ext == "_" + UXImage.suffix[j])
                        {
                            string dest = UXGUIConfig.LocalizationFolder + UXImage.suffix[j] + "/" + fileName;
                            File.Copy(file, dest, true);
                            AssetDatabase.ImportAsset(dest);
                            break;
                        }
                    }
                }
            }
        }
    }
}