using OfficeOpenXml;

namespace LocalizationTool
{
    [LocalizationTask("Patch")]
    internal class PatchTranslationTask : LocalizationTask
    {
        [LocalizationTaskParameter("languages")]
        public string languages;
        [LocalizationTaskParameter("patchFolderPath")]
        public string patchFolderPath;
        [LocalizationTaskParameter("translateFolderPath")]
        public string translateFolderPath;

        public override void Execute()
        {
            var languageList = languages.Split(',');
            foreach (var language in languageList)
            {
                Patch(language);
            }
        }


        private void Patch(string language)
        {
            var filePath = Path.Combine(patchFolderPath, language + ".xlsx");
            if (!File.Exists(filePath))
            {
                return;
            }

            using (var patchPackage = new ExcelPackage(new FileInfo(filePath)))
            {
                foreach (var worksheet in patchPackage.Workbook.Worksheets)
                {
                    // 检查表头
                    if (worksheet.Cells[1, 1].Value?.ToString() != ExcelUtils.KEY_COLUMN_TITLE
                        || worksheet.Cells[1, 2].Value.ToString() != ExcelUtils.TEXT_COLUMN_TITLE
                        || worksheet.Cells[1, 3].Value.ToString() != ExcelUtils.TRANSLATE_COLUMN_TITLE)
                    {
                        continue;
                    }

                    var tag = worksheet.Name;
                    var data = new Dictionary<string, string>();
                    for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
                    {
                        var key = worksheet.Cells[i, 1].Value?.ToString();
                        var translate = worksheet.Cells[i, 3].Value?.ToString();
                        if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(translate))
                        {
                            continue;
                        }
                        data.Add(key, translate);
                    }

                    if (data.Count > 0)
                    {
                        FillTranslateExcel(language, tag, data);
                    }
                }
            }
        }

        private void FillTranslateExcel(string language, string tag, Dictionary<string, string> data)
        {
            var translateExcelPath = Path.Combine(translateFolderPath, language + ".xlsx");
            if (!File.Exists(translateExcelPath))
            {
                Debug.LogError($"翻译表不存在：{translateExcelPath}");
                return;
            }

            using (var package = new ExcelPackage(new FileInfo(translateExcelPath)))
            {
                Debug.LogProcess($"=== 开始填充翻译表 {package.File.FullName}");
                foreach (var worksheet in package.Workbook.Worksheets)
                {
                    // 检查表头
                    if (worksheet.Cells[1, 1].Value?.ToString() != ExcelUtils.KEY_COLUMN_TITLE
                        || worksheet.Cells[1, 2].Value.ToString() != ExcelUtils.TEXT_COLUMN_TITLE
                        || worksheet.Cells[1, 3].Value.ToString() != ExcelUtils.TRANSLATE_COLUMN_TITLE)
                    {
                        continue;
                    }

                    for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
                    {
                        var rowTag = worksheet.Cells[i, 4].Value?.ToString();
                        if (rowTag != tag)
                        {
                            continue;
                        }

                        var key = worksheet.Cells[i, 1].Value?.ToString();
                        if (data.TryGetValue(key, out var text))
                        {
                            worksheet.Cells[i, 3].Value = text;
                            //清除翻译标记
                            worksheet.Cells[i, 4].Value = null;

                            Debug.LogInfo($"### 在{language}->Sheet【{worksheet.Name}】中填充了翻译：{key} => {text}");

                            //背景色设置为白色
                            worksheet.Cells[i, 1, i, 3].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            worksheet.Cells[i, 1, i, 3].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                        }
                    }
                }

                package.TrySave();
            }
        }
    }
}