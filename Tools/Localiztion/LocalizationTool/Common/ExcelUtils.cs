using OfficeOpenXml;

namespace LocalizationTool
{
    internal static class ExcelUtils
    {
        public const string KEY_COLUMN_TITLE = "#KEY";
        public const string TEXT_COLUMN_TITLE = "#TEXT";
        public const string TRANSLATE_COLUMN_TITLE = "#TRANSLATE";

        public static string GetCellPositionText(int row, int column)
        {
            return $"{ExcelCellAddress.GetColumnLetter(column)}{row}";
        }

        public static void TrySave(this ExcelPackage package, int retry = 10)
        {
            for (int i = 0; i < retry; i++)
            {
                try
                {
                    package.Save();
                    return;
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"保存Excel文件失败，可能被占用，请尝试关闭占用程序，重试次数：{i + 1}/{retry}，错误信息：{e.Message}");
                    System.Console.ReadKey();
                }
            }
            throw new System.Exception("保存Excel文件失败");
        }

        public static void TryDeleteFile(string path, int retry = 10)
        {
            // 如果文件不存在，直接返回
            if (!System.IO.File.Exists(path))
            {
                return;
            }

            for (int i = 0; i < retry; i++)
            {
                try
                {
                    System.IO.File.Delete(path);
                    return;
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"删除文件失败，可能被占用，请尝试关闭占用程序，重试次数：{i + 1}/{retry}，错误信息：{e.Message}");
                    System.Console.ReadKey();
                }
            }
        }
    }
}