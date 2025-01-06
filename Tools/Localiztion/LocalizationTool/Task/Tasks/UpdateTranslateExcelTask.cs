using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Ranges;
using System.Text;

namespace LocalizationTool
{
    [LocalizationTask("Update")]
    internal class UpdateTranslateExcelTask : LocalizationTask
    {
        [LocalizationTaskParameter("mainExcelPath")]
        public string mainExcelPath;
        [LocalizationTaskParameter("translateFolderPath")]
        public string translateFolderPath;
        [LocalizationTaskParameter("languages")]
        public string languages;
        [LocalizationTaskParameter("exportPatchFolderPath")]
        public string exportPatchFolderPath;


        private string _tag;
        private bool _NeedExportPatch => !string.IsNullOrEmpty(exportPatchFolderPath);

        public override void Execute()
        {
            var languageList = languages.Split(',');

            // 如果语言列表为空，则不处理
            if (languageList.Length == 0)
            {
                Debug.LogInfo("语言列表为空");
                return;
            }
            Debug.LogProcess($"=== 开始更新多语言Patch表\n主表路径：{mainExcelPath}\n翻译表文件夹路径：{translateFolderPath}\n语言列表：{languages}");

            _tag = GenerateTaskTag();

            if (!Directory.Exists(translateFolderPath))
            {
                Directory.CreateDirectory(translateFolderPath);
            }

            var keyTextsMap = GetMainKeyTextsMap();

            foreach (var language in languageList)
            {
                UpdateExcel(language, keyTextsMap);
            }
        }

        private string GenerateTaskTag()
        {
            // 根据时间生成一个唯一的任务标签
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        private Dictionary<string, List<TranslateKeyText>> GetMainKeyTextsMap()
        {
            Debug.LogProcess($"=== 开始读取主表：{mainExcelPath}");
            var recorded = new HashSet<TranslateKeyText>();
            var result = new Dictionary<string, List<TranslateKeyText>>();
            using (var package = new ExcelPackage(new FileInfo(mainExcelPath)))
            {
                foreach (var worksheet in package.Workbook.Worksheets)
                {
                    // 检查表头
                    if (worksheet.Cells[1, 1].Value.ToString() != ExcelUtils.KEY_COLUMN_TITLE || worksheet.Cells[1, 2].Value.ToString() != ExcelUtils.TEXT_COLUMN_TITLE)
                    {
                        continue;
                    }

                    Debug.LogProcess($"=== 读取 {worksheet.Name}");

                    var keyTexts = new List<TranslateKeyText>();
                    result.Add(worksheet.Name, keyTexts);

                    for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
                    {
                        var key = worksheet.Cells[i, 1].Value?.ToString();
                        var text = worksheet.Cells[i, 2].Value?.ToString();

                        if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(text))
                        {
                            Debug.LogError($"翻译表中存在空白的Key或Text。 位置：{package.File.FullName}:{worksheet.Name} => {ExcelUtils.GetCellPositionText(i, 1)}");
                            continue;
                        }

                        if (!recorded.Any(x => x.key == key))
                        {
                            recorded.Add(new TranslateKeyText { key = key, text = text });
                            keyTexts.Add(new TranslateKeyText { key = key, text = text });
                        }
                        else
                        {
                            Debug.LogError($"翻译表中存在重复的key，已经跳过。Key:{key}\t\t\t位置：{package.File.FullName}:{worksheet.Name} => {ExcelUtils.GetCellPositionText(i, 1)}");
                        }
                    }
                }

                Debug.LogProcess($"=== 主表读取完毕");
            }
            return result;
        }

        private void UpdateExcel(string language, Dictionary<string, List<TranslateKeyText>> keyTextsMap)
        {
            var excelFilePath = Path.Combine(translateFolderPath, $"{language}.xlsx");

            Debug.LogProcess($"=== 开始更新翻译表: {excelFilePath}");

            using (var package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                foreach (var (worksheetName, mainKeyTexts) in keyTextsMap)
                {
                    var sheet = package.Workbook.Worksheets.FirstOrDefault(x => x.Name == worksheetName);
                    if (sheet == null)
                    {
                        sheet = package.Workbook.Worksheets.Add(worksheetName);
                        sheet.Cells[1, 1].Value = ExcelUtils.KEY_COLUMN_TITLE;
                        sheet.Cells[1, 2].Value = ExcelUtils.TEXT_COLUMN_TITLE;
                        sheet.Cells[1, 3].Value = ExcelUtils.TRANSLATE_COLUMN_TITLE;
                    }
                    else
                    {
                        // 检查表头
                        var hasKeyTile = sheet.Cells[1, 1].Value?.ToString() == ExcelUtils.KEY_COLUMN_TITLE;
                        var hasTextTile = sheet.Cells[1, 2].Value?.ToString() == ExcelUtils.TEXT_COLUMN_TITLE;
                        var hasTranslateTile = sheet.Cells[1, 3].Value?.ToString() == ExcelUtils.TRANSLATE_COLUMN_TITLE;
                        if (!hasKeyTile || !hasTextTile || !hasTranslateTile)
                        {
                            continue;
                        }
                    }
                    var translateKeyTexts = new Dictionary<TranslateKeyText, string>();
                    for (var i = 2; i <= sheet.Dimension.End.Row; i++)
                    {
                        var key = sheet.Cells[i, 1].Value?.ToString();
                        var text = sheet.Cells[i, 2].Value?.ToString();
                        var translate = sheet.Cells[i, 3].Value?.ToString();
                        var tag = sheet.Cells[i, 4].Value?.ToString();
                        if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(text))
                        {
                            continue;
                        }
                        var keyText = new TranslateKeyText { key = key, text = text, tag = tag };
                        if (translateKeyTexts.ContainsKey(keyText))
                        {
                            Debug.LogError($"翻译表中存在重复的Key: {keyText.key}\t\t\t位置：{package.File.Name}:{worksheetName} => {ExcelUtils.GetCellPositionText(i, 1)}");
                        }
                        else
                        {
                            translateKeyTexts.Add(keyText, translate);
                        }
                    }

                    // key 和主表一致但是 text 不一致的 KeyText
                    var needUpdateKeyTexts1 = new HashSet<TranslateKeyText>();
                    // key 和主表不一致的 KeyText
                    var needUpdateKeyTexts2 = new HashSet<TranslateKeyText>();

                    foreach (var keyText in mainKeyTexts)
                    {
                        var hasSameKey = translateKeyTexts.Any(x => x.Key.key == keyText.key);
                        if (hasSameKey)
                        {
                            var sameKey = translateKeyTexts.First(x => x.Key.key == keyText.key);
                            if (sameKey.Key.text != keyText.text)
                            {
                                needUpdateKeyTexts1.Add(keyText);
                                translateKeyTexts.Remove(keyText);
                            }
                        }
                        else
                        {
                            needUpdateKeyTexts2.Add(keyText);
                            translateKeyTexts.Remove(keyText);
                        }

                    }

                    DrawSheet(translateKeyTexts, needUpdateKeyTexts1, needUpdateKeyTexts2, sheet);
                }
                ConclusionExcel(package, language);
                GeneratePatchExcel(package, language);
                package.TrySave();
            }
        }

        private void DrawSheet(Dictionary<TranslateKeyText, string> existedKeyTexts, HashSet<TranslateKeyText> needUpdateKeyTexts1, HashSet<TranslateKeyText> needUpdateKeyTexts2, ExcelWorksheet sheet)
        {
            // 删除从2行开始的所有行
            for (var i = sheet.Dimension.End.Row; i >= 2; i--)
            {
                sheet.DeleteRow(i);
            }

            var row = 2;
            foreach (var (keyText, translate) in existedKeyTexts)
            {
                sheet.Cells[row, 1].Value = keyText.key;
                sheet.Cells[row, 2].Value = keyText.text;
                sheet.Cells[row, 3].Value = translate;
                sheet.Cells[row, 4].Value = keyText.tag;
                row++;
            }

            foreach (var keyText in needUpdateKeyTexts1)
            {
                sheet.Cells[row, 1].Value = keyText.key;
                sheet.Cells[row, 2].Value = keyText.text;
                sheet.Cells[row, 3].Value = string.Empty;
                sheet.Cells[row, 4].Value = string.Empty;
                row++;
            }

            foreach (var keyText in needUpdateKeyTexts2)
            {
                sheet.Cells[row, 1].Value = keyText.key;
                sheet.Cells[row, 2].Value = keyText.text;
                sheet.Cells[row, 3].Value = string.Empty;
                sheet.Cells[row, 4].Value = string.Empty;
                row++;
            }
        }
        // 总结
        private void ConclusionExcel(ExcelPackage package, string language)
        {
            // 删除 key 或者 text 列为空的行
            foreach (var worksheet in package.Workbook.Worksheets)
            {
                // 检查表头
                if (worksheet.Cells[1, 1].Value?.ToString() != ExcelUtils.KEY_COLUMN_TITLE
                    || worksheet.Cells[1, 2].Value.ToString() != ExcelUtils.TEXT_COLUMN_TITLE
                    || worksheet.Cells[1, 3].Value.ToString() != ExcelUtils.TRANSLATE_COLUMN_TITLE)
                {
                    continue;
                }
                for (int i = worksheet.Dimension.End.Row; i >= 2; i--)
                {
                    var key = worksheet.Cells[i, 1].Value?.ToString();
                    var text = worksheet.Cells[i, 2].Value?.ToString();
                    if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(text))
                    {
                        worksheet.DeleteRow(i);
                    }
                }
            }

            // 修改一下 Sheet 样式
            foreach (var worksheet in package.Workbook.Worksheets)
            {
                // 检查表头
                if (worksheet.Cells[1, 1].Value?.ToString() != ExcelUtils.KEY_COLUMN_TITLE
                    || worksheet.Cells[1, 2].Value.ToString() != ExcelUtils.TEXT_COLUMN_TITLE
                    || worksheet.Cells[1, 3].Value.ToString() != ExcelUtils.TRANSLATE_COLUMN_TITLE)
                {
                    continue;
                }

                var range = worksheet.Cells[1, 1, worksheet.Dimension.End.Row, worksheet.Dimension.End.Column];
                //把所有单元格设置为所有框线
                var border = range.Style.Border;
                border.Top.Style = border.Left.Style = border.Right.Style = border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                // 设置列宽
                range.AutoFitColumns();
            }

            // 检查未填充Translate的行，如果有未填充的Key，就警告并标记为橙色背景
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
                    var translate = worksheet.Cells[i, 3].Value?.ToString();

                    if (string.IsNullOrEmpty(translate))
                    {
                        worksheet.Cells[i, 1, i, 3].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Cells[i, 1, i, 3].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Orange);
                    }
                }
            }
            Debug.LogProcess($"=== 更新翻译表完毕");
        }

        private void GeneratePatchExcel(ExcelPackage package, string language)
        {
            var notTranslateTextInfos = new List<(string sheetName, string key, string text, int row, int column)>();
            var notTranslateTaggedTextInfos = new Dictionary<string, List<(string sheetName, string key, string text, int row, int column)>>();

            // 检查未填充Translate的行，如果有未填充的Key，就警告并标记为橙色背景
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
                    var translate = worksheet.Cells[i, 3].Value?.ToString();

                    if (string.IsNullOrEmpty(translate))
                    {
                        var key = worksheet.Cells[i, 1].Value?.ToString();
                        var text = worksheet.Cells[i, 2].Value?.ToString();
                        var rowTag = worksheet.Cells[i, 4].Value?.ToString();
                        if (string.IsNullOrEmpty(rowTag))
                        {
                            worksheet.Cells[i, 4].Value = _tag;
                            notTranslateTextInfos.Add((worksheet.Name, key, text, i, 3));
                        }
                        else
                        {
                            if (notTranslateTaggedTextInfos.TryGetValue(rowTag, out var list))
                            {
                                list.Add((worksheet.Name, key, text, i, 3));
                            }
                            else
                            {
                                list = new List<(string sheetName, string key, string text, int row, int column)> { (worksheet.Name, key, text, i, 3) };
                                notTranslateTaggedTextInfos.Add(rowTag, list);
                            }
                        }
                    }
                }
            }

            if (!Directory.Exists(exportPatchFolderPath))
            {
                Directory.CreateDirectory(exportPatchFolderPath);
            }
            var exportFileName = $"{language}.xlsx";
            // 删除已经存在的Patch表
            var exportFilePath = Path.Combine(exportPatchFolderPath, exportFileName);
            ExcelUtils.TryDeleteFile(exportFilePath);
            Debug.LogInfo($"### 生成未翻译的文本Patch表：{exportFilePath}");
            using (var exportPackage = new ExcelPackage(new FileInfo(exportFilePath)))
            {
                if (notTranslateTextInfos.Count > 0)
                {
                    GeneratePatchSheet(exportPackage, _tag, notTranslateTextInfos);
                }
                foreach (var (tag, info) in notTranslateTaggedTextInfos)
                {
                    GeneratePatchSheet(exportPackage, tag, info);
                }
                exportPackage.TrySave();
            }
        }

        private void GeneratePatchSheet(ExcelPackage exportPackage, string tag, List<(string sheetName, string key, string text, int row, int column)> notTranslateTextInfos)
        {
            var exportSheet = exportPackage.Workbook.Worksheets.Add(tag);

            exportSheet.Cells[1, 1].Value = ExcelUtils.KEY_COLUMN_TITLE;
            exportSheet.Cells[1, 2].Value = ExcelUtils.TEXT_COLUMN_TITLE;
            exportSheet.Cells[1, 3].Value = ExcelUtils.TRANSLATE_COLUMN_TITLE;

            var row = 2;
            foreach (var (_, key, text, _, _) in notTranslateTextInfos)
            {
                exportSheet.Cells[row, 1].Value = key;
                exportSheet.Cells[row, 2].Value = text;
                exportSheet.Cells[row, 3].Value = string.Empty;
                Debug.LogInfo($"\t### Text: {text}");
                row++;
            }

            var range = exportSheet.Cells[1, 1, exportSheet.Dimension.End.Row, exportSheet.Dimension.End.Column];
            range.AutoFitColumns();

            Debug.LogProcess("=== 生成未翻译的文本Patch表完毕");
        }

        class TranslateKeyText
        {
            public string key;
            public string text;
            public string tag;

            public override string ToString()
            {
                return $"{key}|{text}|{tag}";
            }
        }
    }
}