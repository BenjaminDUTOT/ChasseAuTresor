using ChasseAuTresor.Model.AdventurerModel;
using ChasseAuTresor.Common;
using ChasseAuTresor.Utils;
using System.Text;

namespace ChasseAuTresor.CellModel
{
    public abstract class AbstractCell : Coordinate
    {
        public abstract TypeCell cellType { get; }

        public abstract string GetDisplayType();

        public virtual bool IsReachable()
        {
            return false;
        }

        public virtual void AddAdventurer(Adventurer adventurer)
        {
            
        }

        public virtual void RemoveAdventurer()
        {

        }

        public virtual bool TryGetWriteText(out string result)
        {
            result = string.Empty;

            return false;
        }

        public static AbstractCell[,] CreateAbstractCellTableFromLine(string line)
        {
            if (line != null)
            {
                string[] strings = line.Replace(" ", "").Split("-");

                if (strings.Length == 3 && !strings.Any(s => s == string.Empty))
                {
                    return new AbstractCell[Parser.ParseInteger(strings[2]), Parser.ParseInteger(strings[1])];
                }
                else
                {
                    throw new ArgumentException("The number of parameter of the line corresponding to the map is not egual to 3 ", line);
                }
            }
            else
            {
                throw new ArgumentNullException("The line is null, so map cannot be created");
            }
        }

        public static void AddCellInTable(ref AbstractCell[,] cells, ref List<Adventurer> adventurers, IEnumerable<string> lines)
        {
            if(cells == null)
            {
                throw new ArgumentNullException(nameof(cells));
            }
            else if (adventurers is null)
            {
                throw new ArgumentNullException(nameof(adventurers));
            }
            else if (lines is null)
            {
                throw new ArgumentNullException(nameof(lines));
            }

            foreach (string line in lines)
            {
                switch (line.First())
                {
                    case 'C':
                        continue;
                    case '#':
                        continue;
                    case 'M':
                        MountainCell.AddMountainCellInTable(ref cells, line);
                        break;
                    case 'T':
                        TreasureCell.AddTreasureCellInTable(ref cells, line);
                        break;
                    case 'A':
                        Adventurer.AddAdventurerInTable(ref cells, line, ref adventurers);
                        break;
                    default:
                        continue;
                }
            }

            CompleteTableWithPlainCell(ref cells);
        }

        public static void CompleteTableWithPlainCell(ref AbstractCell[,] cells)
        {
            if(cells != null)
            {
                for (int i = 0; i < cells.GetLength(0); i++)
                {
                    for (int j = 0; j < cells.GetLength(1); j++)
                    {
                        if (cells[i, j] == null)
                        {
                            cells[i, j] = new PlainCell()
                            {
                                AxeHorizontal = i,
                                AxeVertical = j,
                            };
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentNullException("The cells is not define while attempt to complete it");
            }
        }

        public static string WriteMapInFile(ref AbstractCell[,] cells)
        {
            if (cells != null)
            {
                return $"C - {cells.GetLength(1)} - {cells.GetLength(0)}";
            }
            else
            {
                throw new ArgumentNullException("You must initialize the Array Cell correctly to write it");
            }
        }

        public static void DisplayArrayCellInConsole(AbstractCell[,] cells)
        {
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    Console.Write(cells[i, j].GetDisplayType() + "\t");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public static string WriteCellsInFile(ref AbstractCell[,] cells)
        {
            if(cells != null)
            {
                StringBuilder stringBuilder = new StringBuilder();

                for (int i = 0; i < cells.GetLength(0); i++)
                {
                    for (int j = 0; j < cells.GetLength(1); j++)
                    {
                        string result;

                        if (cells[i, j].TryGetWriteText(out result))
                        {
                            stringBuilder.AppendLine(result);
                        }
                    }
                }

                return stringBuilder.ToString();
            }
            else
            {
                throw new ArgumentNullException("The cells is not define while attempt to display it");
            }
        }
    }
}
