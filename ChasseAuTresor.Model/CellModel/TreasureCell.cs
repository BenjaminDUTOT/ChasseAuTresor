using ChasseAuTresor.Model.AdventurerModel;
using ChasseAuTresor.Utils;

namespace ChasseAuTresor.CellModel
{
    public class TreasureCell : PlainCell
    {
        public override TypeCell cellType => TypeCell.Treasure;

        public int TreasureValue { get; set; }

        public override string GetDisplayType()
        {
            return Adventurer == null ? $"{(char)cellType}({TreasureValue})" : Adventurer.GetDisplayText();
        }

        public override void AddAdventurer(Adventurer adventurer)
        {
            this.Adventurer = adventurer;

            if (this.TreasureValue > 0)
            {
                this.TreasureValue--;
                adventurer.TreasureValue++;
            }
        }

        public override bool TryGetWriteText(out string result)
        {
            if(Adventurer == null)
            {
                result = $"T - {AxeHorizontal} - {AxeVertical} - {TreasureValue}";
            }
            else
            {
                result = $"T - {AxeHorizontal} - {AxeVertical} - {TreasureValue}{System.Environment.NewLine}{Adventurer.WriteAdventurer(Adventurer)}";
            }

            return true;
        }

        public static void AddTreasureCellInTable(ref AbstractCell[,] cells, string line)
        {
            if (line == null)
            {
                throw new ArgumentNullException("The treasure cannot be created because the line is null");
            }
            else  if(cells == null)
            {
                throw new ArgumentNullException("The treasure cannot be created because the cells is null");
            }

            string[] strings = line.Replace(" ", "").Split("-");

            if (strings.Length != 4 || strings.Any(s => s == string.Empty))
            {
                throw new ArgumentException("The number of parameter of the line corresponding to the Treasure is not egual to 4 ", line);
            }
            else if (Parser.ParseInteger(strings[2]) >= cells.GetLength(0) || Parser.ParseInteger(strings[1]) >= cells.GetLength(1))
            {
                throw new ArgumentException("The treasure cannot be created because the coordinate are no in the cells", line);
            }
            else
            {
                if (cells[Parser.ParseInteger(strings[2]), Parser.ParseInteger(strings[1])] != null)
                {
                    throw new ArgumentException("The treasure cannot be created because the cell is already created", line);
                }

                cells[Parser.ParseInteger(strings[2]), Parser.ParseInteger(strings[1])] = new TreasureCell()
                {
                    TreasureValue = Parser.ParseInteger(strings[3]),
                    AxeHorizontal = Parser.ParseInteger(strings[1]),
                    AxeVertical = Parser.ParseInteger(strings[2]),
                };
            }
        }
    }
}
