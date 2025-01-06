namespace LocalizationTool
{
    internal class Cell
    {
        public string Value { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public Cell(int row, int column, string value)
        {
            Row = row;
            Column = column;
            Value = value;
        }
    }
}
