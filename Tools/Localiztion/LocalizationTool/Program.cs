using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LocalizationTool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


            var taskType = args[0];
            var task = LocalizationTaskManager.GetTask(taskType);
            if (task == null)
            {
                Debug.LogError("未找到任务类型：" + taskType);
                return;
            }
            var taskArgs = args.Skip(1).ToArray();
            Debug.LogProcess($"=== 开始处理任务：{taskType}");
            task.ParseParams(taskArgs);
            task.Execute();
            Debug.LogProcess("=== 处理完成");
        }

        //private static void CollectTranslateSources()
        //{
        //    var mainExcelPath = Options.Instance.MainExcelPath;
        //    var configTextPath = Options.Instance.ConfigTextPath;
        //}

        //private static void UpdatePatchExcels()
        //{
        //    var mainExcelPath = Options.Instance.MainExcelPath;
        //    var configTextPath = Options.Instance.ConfigTextPath;
        //    var translateFolderPath = Options.Instance.TranslateFolderPath;
        //    var languages = Options.Instance.Languages.Split(',');

        //    // 如果语言列表为空，则不处理
        //    if (languages.Length == 0)
        //    {
        //        Debug.LogInfo("语言列表为空");
        //        return;
        //    }

        //    Debug.LogProcess($"=== 开始更新多语言Patch表\n主表路径：{mainExcelPath}\n配置文本路径：{configTextPath}\n翻译表文件夹路径：{translateFolderPath}\n语言列表：{string.Join(",", languages)}");

        //    if (!Directory.Exists(translateFolderPath))
        //    {
        //        Directory.CreateDirectory(translateFolderPath);
        //    }

        //    var mainKeys = MainExcelReader.Read(mainExcelPath);


        //    void TryMerge(string name, HashSet<TranslateSource> sources)
        //    {
        //        if (mainKeys.TryGetValue(name, out var s))
        //        {
        //            s.UnionWith(sources);
        //        }
        //        else
        //        {
        //            mainKeys.Add(name, sources);
        //        }
        //    }

        //    var configTextKeys = ConfigTextReader.Read(configTextPath);
        //    var configTextSheetName = "配置表文本";
        //    TryMerge(configTextSheetName, configTextKeys);

        //    foreach (var language in languages)
        //    {
        //        var translateExcelPath = Path.Combine(translateFolderPath, language + ".xlsx");
        //        using (var package = new ExcelPackage(new FileInfo(translateExcelPath)))
        //        {
        //            var tips = TranslateExcelUpdater.Update(package, mainKeys);
        //            if (tips.Count > 0)
        //            {
        //                Debug.LogInfo($"翻译表中还有未翻译项，文件：{translateExcelPath}\n{string.Join("\n", tips)}");
        //            }
        //        }
        //    }

        //    // 打开翻译表文件夹
        //    Debug.LogProcess($"打开 {translateFolderPath}");
        //    System.Diagnostics.Process.Start("explorer.exe", new DirectoryInfo(translateFolderPath).FullName);
        //}

        //private static void ExportLanguageDatas()
        //{
        //    Debug.LogProcess("=== 开始导出多语言数据");
        //    var translateFolderPath = Options.Instance.TranslateFolderPath;
        //    var languages = Options.Instance.Languages.Split(',');

        //    foreach (var language in languages)
        //    {
        //        var translateExcelPath = Path.Combine(translateFolderPath, language + ".xlsx");
        //        using (var package = new ExcelPackage(new FileInfo(translateExcelPath)))
        //        {
        //            LanguageDataExporter.Export(package, language);
        //        }
        //    }
        //}
    }
}
