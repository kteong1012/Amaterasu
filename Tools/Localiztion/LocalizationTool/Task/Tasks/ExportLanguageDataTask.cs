using OfficeOpenXml;
using System.Text.Json;

namespace LocalizationTool
{
    [LocalizationTask("Export")]
    internal class ExportLanguageDataTask : LocalizationTask
    {
        [LocalizationTaskParameter("translateFolderPath", description: "Excel路径", required: true)]
        public string translateFolderPath;
        [LocalizationTaskParameter("outputDir", description: "输出路径", required: true)]
        public string outputDir;
        [LocalizationTaskParameter("dataType", description: "导出数据类型，可选值：Bytes | Json", required: true)]
        public string dataType;
        [LocalizationTaskParameter("languages")]
        public string languages;

        public override void Execute()
        {
            var languages = this.languages.Split(',');
            foreach (var language in languages)
            {
                var excelFileName = language + ".xlsx";
                var excelFilePath = Path.Combine(translateFolderPath, excelFileName);
                using (var package = new ExcelPackage(new FileInfo(excelFilePath)))
                {
                    ExportExcel(package, language);
                }
            }
        }
        private void ExportExcel(ExcelPackage package, string language)
        {
            Debug.LogProcess($"=== 开始导出翻译表 {package.File.FullName}");
            var keyValues = new Dictionary<string, string>();
            foreach (var worksheet in package.Workbook.Worksheets)
            {
                // 如果A1不是#KEY，B1不是#TEXT，则跳过
                if (worksheet.Cells[1, 1].Value?.ToString() != "#KEY" || worksheet.Cells[1, 2].Value.ToString() != "#TEXT")
                {
                    Debug.LogInfo($" 检测到翻译表格式不正确，已跳过。位置：{package.File.FullName}:{worksheet.Name}");
                    continue;
                }

                Debug.LogProcess($"=== 开始导出翻译表 {worksheet.Name}");
                for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
                {
                    var key = worksheet.Cells[i, 1].Value?.ToString();
                    var text = worksheet.Cells[i, 3].Value?.ToString();

                    if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(text))
                    {
                        Debug.LogInfo($"翻译表中存在空白的Key或Text。 位置：{package.File.FullName}:{worksheet.Name} => {ExcelUtils.GetCellPositionText(i, 1)}");
                        continue;
                    }

                    if (!keyValues.ContainsKey(key))
                    {
                        keyValues.Add(key, text);
                    }
                }
            }
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }
            if (dataType == "Bytes")
            {
                ExportToBytes(keyValues, language, Path.Combine(outputDir, language + ".bytes"));
            }
            else if (dataType == "Json")
            {
                ExportToJson(keyValues, language, Path.Combine(outputDir, language + ".json"));
            }
            else
            {
                throw new Exception("不支持的数据类型：" + dataType);
            }
        }

        private static void ExportToJson(Dictionary<string, string> keyValues, string language, string outputPath)
        {
            var jsonString = JsonSerializer.Serialize(keyValues, new JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping });

            File.WriteAllText(outputPath, jsonString);
            Debug.LogProcess($"### 导出Json文件成功: {outputPath}");
        }

        private static void ExportToBytes(Dictionary<string, string> keyValues, string language, string outputPath)
        {
            var buf = new ByteBuf();

            buf.WriteSize(keyValues.Count);
            foreach (var (key, value) in keyValues)
            {
                buf.WriteString(key);
                buf.WriteString(value);
            }

            File.WriteAllBytes(outputPath, buf.Bytes);

            Debug.LogProcess($"### 导出Bytes文件成功: {outputPath}");
        }
    }
}
