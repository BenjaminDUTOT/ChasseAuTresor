using ChasseAuTresor.Utils;

namespace ChasseAuTresor.CellModel
{
    public class MountainCell : AbstractCell
    {
        public override TypeCell cellType => TypeCell.Mountain;

        public override string GetDisplayType()
        {
            return $"{(char)cellType}";
        }

        public override bool TryGetWriteText(out string result)
        {
            result = $"M - {AxeHorizontal} - {AxeVertical}";
            return true;
        }

        public static void AddMountainCellInTable(ref AbstractCell[,] cells, string line)
        {
            if (line == null)
            {
                throw new ArgumentNullException("The treasure cannot be created because the line is null");
            }
            else if (cells == null)
            {
                throw new ArgumentNullException("The treasure cannot be created because the cells is null");
            }

            string[] strings = line.Replace(" ", "").Split("-");

            if (strings.Length != 3 || strings.Any(data => data == string.Empty))
            {
                throw new ArgumentException("The number of parameter of the line corresponding to the Mountain is not egual to 3 ", line);
            }
            else if (Parser.ParseInteger(strings[2]) >= cells.GetLength(0) || Parser.ParseInteger(strings[1]) >= cells.GetLength(1))
            {
                throw new ArgumentException("The treasure cannot be created because the coordinate are no in the cells", line);
            }
            else
            {
                if (cells[Parser.ParseInteger(strings[2]), Parser.ParseInteger(strings[1])] != null)
                {
                    throw new ArgumentException("The mountain cannot be created because the cell is already created ", line);
                }
                cells[Parser.ParseInteger(strings[2]), Parser.ParseInteger(strings[1])] = new MountainCell()
                {
                    AxeHorizontal = Parser.ParseInteger(strings[1]),
                    AxeVertical = Parser.ParseInteger(strings[2]),
                };
            }
        }
    }


}
