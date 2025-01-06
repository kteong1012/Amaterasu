using OfficeOpenXml;
using System.Text;

namespace LocalizationTool
{
    [LocalizationTask("Collect")]
    internal class CollectMainExcelTask : LocalizationTask
    {
        private const string _codeSheetName = "C#文本";
        private const string _prefabSheetName = "预制体文本";
        private const string _configSheetName = "配置表文本";


        [LocalizationTaskParameter("mainExcelPath", description: "主表路径", required: true)]
        public string mainExcelPath;

        [LocalizationTaskParameter("codeTextPath", description: "来自C#代码的翻译文本路径", required: false)]
        public string codeTextPath;

        [LocalizationTaskParameter("prefabTextPath", description: "来自Prefab的翻译文本路径", required: false)]
        public string prefabTextPath;

        [LocalizationTaskParameter("configTextPath", description: "来自Config的翻译文本路径", required: false)]
        public string configTextPath;

        public override void Execute()
        {
            // 此任务主要做收集，不需要去重，但是会警告重复的key
            using (var package = new ExcelPackage(new FileInfo(mainExcelPath)))
            {
                // 读取C#代码文本
                var codeSources = ReadTextFile(codeTextPath);
                DrawSheet(package, _codeSheetName, codeSources);

                // 读取Prefab文本
                var prefabSources = ReadTextFile(prefabTextPath);
                DrawSheet(package, _prefabSheetName, prefabSources);

                // 读取配置表文本
                var configSources = ReadTextFile(configTextPath);
                DrawSheet(package, _configSheetName, configSources);

                // 总结
                Conclusion(package);

                // 保存
                package.TrySave();
            }
        }

        private HashSet<string> ReadTextFile(string path)
        {
            if (!File.Exists(path))
            {
                return new HashSet<string>();
            }
            Debug.LogProcess($"=== 开始读取文本: {path}");
            var result = new HashSet<string>();
            // 读取txt文本，每一行的内容作为一个Key
            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                var key = line;
                if (string.IsNullOrEmpty(key))
                {
                    continue;
                }
                result.Add(key);
            }

            return result;
        }

        private void DrawSheet(ExcelPackage package, string sheetName, HashSet<string> sources)
        {
            if (sources == null || sources.Count == 0)
            {
                return;
            }
            var worksheet = package.Workbook.Worksheets.FirstOrDefault(x => x.Name == sheetName);
            if (worksheet == null)
            {
                worksheet = package.Workbook.Worksheets.Add(sheetName);
                worksheet.Cells[1, 1].Value = ExcelUtils.KEY_COLUMN_TITLE;
                worksheet.Cells[1, 2].Value = ExcelUtils.TEXT_COLUMN_TITLE;
            }
            if (worksheet.Cells[1, 1].Value?.ToString() != ExcelUtils.KEY_COLUMN_TITLE || worksheet.Cells[1, 2].Value.ToString() != ExcelUtils.TEXT_COLUMN_TITLE)
            {
                worksheet.Cells.Clear();
                worksheet.Cells[1, 1].Value = ExcelUtils.KEY_COLUMN_TITLE;
                worksheet.Cells[1, 2].Value = ExcelUtils.TEXT_COLUMN_TITLE;
            }

            var existKeys = new HashSet<string>();
            for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
            {
                var key = worksheet.Cells[i, 1].Value?.ToString();
                existKeys.Add(key);
            }

            var rowIndex = worksheet.Dimension.End.Row + 1;
            foreach (var source in sources)
            {
                if (!existKeys.Contains(source))
                {
                    worksheet.Cells[rowIndex, 1].Value = source;
                    worksheet.Cells[rowIndex, 2].Value = source;
                    Debug.LogInfo($"### 在Sheet【{sheetName}】中添加了新的Key: {source}");
                    rowIndex++;
                }
            }
        }

        // 总结
        private void Conclusion(ExcelPackage package)
        {
            // 删除 key 列为空的行
            foreach (var worksheet in package.Workbook.Worksheets)
            {
                for (int i = worksheet.Dimension.End.Row; i >= 2; i--)
                {
                    var key = worksheet.Cells[i, 1].Value?.ToString();
                    if (string.IsNullOrEmpty(key))
                    {
                        worksheet.DeleteRow(i);
                    }
                }
            }

            // 修改一下 Sheet 样式
            foreach (var worksheet in package.Workbook.Worksheets)
            {
                // 检查表头
                if (worksheet.Cells[1, 1].Value?.ToString() != ExcelUtils.KEY_COLUMN_TITLE || worksheet.Cells[1, 2].Value.ToString() != ExcelUtils.TEXT_COLUMN_TITLE)
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

            // 检查未填充Text的Key，如果有未填充的Key，就警告并标记为橙色背景
            foreach (var worksheet in package.Workbook.Worksheets)
            {
                // 从第二行开始，如果A列有值但是B列为空白，就标为橙色背景，否则就是白色背景
                for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
                {
                    var key = worksheet.Cells[i, 1].Value?.ToString();
                    var text = worksheet.Cells[i, 2].Value?.ToString();
                    if (!string.IsNullOrEmpty(key) && string.IsNullOrEmpty(text))
                    {
                        worksheet.Cells[i, 1, i, 2].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Cells[i, 1, i, 2].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Orange);

                        Debug.LogError($"未填充Text的Key: {key}\t\t\t\t\t位置：{worksheet.Name} => {ExcelUtils.GetCellPositionText(i, 2)}");
                    }
                }
            }

            // 检查重复Key，如果有重复的Key，就警告并标记为红色背景
            var repeatKeys = new Dictionary<string, RepeatKeyInfo>();

            foreach (var worksheet in package.Workbook.Worksheets)
            {
                // 检查表头
                if (worksheet.Cells[1, 1].Value?.ToString() != ExcelUtils.KEY_COLUMN_TITLE || worksheet.Cells[1, 2].Value.ToString() != ExcelUtils.TEXT_COLUMN_TITLE)
                {
                    continue;
                }

                for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
                {
                    var key = worksheet.Cells[i, 1].Value?.ToString();
                    // 如果key为空，则跳过
                    if (string.IsNullOrEmpty(key))
                    {
                        continue;
                    }

                    if (repeatKeys.TryGetValue(key, out var repeatKey))
                    {
                        repeatKey.isRepeat = true;
                        repeatKey.cellInfos.Add(new CellInfo { sheetName = worksheet.Name, row = i, column = 1, isFirst = false });
                    }
                    else
                    {
                        repeatKeys.Add(key, new RepeatKeyInfo
                        {
                            key = key,
                            isRepeat = false,
                            cellInfos = new List<CellInfo> { new CellInfo { sheetName = worksheet.Name, row = i, column = 1, isFirst = true } }
                        });
                    }
                }
            }

            // 处理重复的Key
            if (repeatKeys.Any(x => x.Value.isRepeat))
            {
                // 警告
                var sb = new StringBuilder();
                Debug.LogError("检测到重复的Key，请手动调整：");
                foreach (var repeatKey in repeatKeys.Values.Where(x => x.isRepeat))
                {
                    sb.AppendLine($"\tKey: {repeatKey.key}:");
                    foreach (var cell in repeatKey.cellInfos)
                    {
                        sb.AppendLine($"\t\t{cell.sheetName} => {ExcelUtils.GetCellPositionText(cell.row, cell.column)}");
                    }
                }
                Debug.LogError(sb.ToString());

                // 修改背景颜色
                foreach (var repeatKey in repeatKeys.Values)
                {
                    if (repeatKey.isRepeat)
                    {
                        foreach (var cell in repeatKey.cellInfos.Where(x => !x.isFirst))
                        {
                            var worksheet = package.Workbook.Worksheets[cell.sheetName];
                            worksheet.Cells[cell.row, cell.column].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            worksheet.Cells[cell.row, cell.column].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);
                        }
                    }
                }
            }
        }



        class RepeatKeyInfo
        {
            public string key;
            public bool isRepeat;
            public List<CellInfo> cellInfos;
        }

        class CellInfo
        {
            public string sheetName;
            public int row;
            public int column;
            public bool isFirst;
        }
    }
}