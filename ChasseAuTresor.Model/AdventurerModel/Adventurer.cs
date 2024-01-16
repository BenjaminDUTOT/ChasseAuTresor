using ChasseAuTresor.CellModel;
using ChasseAuTresor.Common;
using ChasseAuTresor.Utils;

namespace ChasseAuTresor.Model.AdventurerModel
{
    public class Adventurer : Coordinate
    {
        public string Name { get; set; }

        public string MovementSequency { get; set; }

        public Orientation Orientation { get; set; }

        public int TreasureValue { get; set; }

        public string GetDisplayText()
        {
            return $"A({Name})";
        }

        public static Adventurer BuildAdventurer(string[] datas)
        {
            if (datas == null)
            {
                throw new ArgumentNullException($"Argument not vald to create adventurer");
            }
            else if (datas.Length != 6)
            {
                throw new ArgumentException($"{datas} not vald to create adventurer");
            }

            return new Adventurer()
            {
                Name = datas[1],
                AxeHorizontal = Parser.ParseInteger(datas[2]),
                AxeVertical = Parser.ParseInteger(datas[3]),
                Orientation = Parser.ParseOrientation(datas[4]),
                MovementSequency = datas[5],
            };
        }

        public static string WriteAdventurer(Adventurer adventurer)
        {
            if (adventurer == null)
            {
                throw new ArgumentNullException($"Cannot write null");
            }

            return $"A - {adventurer.Name} - {adventurer.AxeHorizontal} - {adventurer.AxeVertical} - {adventurer.Orientation.ToString()} - {adventurer.TreasureValue}";
        }

        public static void RealiseAction(char nextAction, Adventurer adventurer, ref AbstractCell[,] cells)
        {
            if(adventurer == null)
            {
                throw new ArgumentNullException("Cannot realise action because adventurer is null", nameof(adventurer));
            }
            else if(cells == null)
            {
                throw new ArgumentNullException("Cannot realise action because cells list is null", nameof(cells));
            }

            switch (char.ToUpper(nextAction))
            {
                case 'A':
                    MovementAction(adventurer, ref cells);
                    break;
                case 'D':
                    TurnRight(adventurer);
                    break;
                case 'G':
                    TurnLeft(adventurer);
                    break;
                default:
                    throw new ArgumentException($"Une erreur c'est produite lors de la réalisation d'une action. L'action : {nextAction} de l'aventurier : {adventurer.Name} n'est pas reconnu");
            }
        }

        private static void MovementAction(Adventurer adventurer, ref AbstractCell[,] cells)
        {
            Coordinate nextPosition = GetNextPosition(adventurer);

            if (CanMove(nextPosition, ref cells))
            {
                MakeMovement(nextPosition, adventurer, ref cells);
            }
        }

        private static void MakeMovement(Coordinate nextPosition, Adventurer adventurer, ref AbstractCell[,] cells)
        {
            cells[adventurer.AxeVertical, adventurer.AxeHorizontal].RemoveAdventurer();
            cells[nextPosition.AxeVertical, nextPosition.AxeHorizontal].AddAdventurer(adventurer);

            adventurer.AxeHorizontal = nextPosition.AxeHorizontal;
            adventurer.AxeVertical = nextPosition.AxeVertical;
        }

        private static bool CanMove(Coordinate nextPosition, ref AbstractCell[,] cells)
        {
            return IsInTheMap(nextPosition, ref cells) &&
                cells[nextPosition.AxeVertical, nextPosition.AxeHorizontal].IsReachable();
        }

        private static bool IsInTheMap(Coordinate nextPosition, ref AbstractCell[,] cells)
        {
            return nextPosition.AxeHorizontal >= 0 &&
                nextPosition.AxeVertical >= 0 &&
                nextPosition.AxeHorizontal < cells.GetLength(1) &&
                nextPosition.AxeVertical < cells.GetLength(0);
        }

        private static Coordinate GetNextPosition(Adventurer adventurer)
        {
            Coordinate nextPosition = new Coordinate()
            {
                AxeHorizontal = adventurer.AxeHorizontal,
                AxeVertical = adventurer.AxeVertical
            };

            switch (adventurer.Orientation)
            {
                case Orientation.N:
                    nextPosition.AxeVertical -= 1;
                    break;
                case Orientation.E:
                    nextPosition.AxeHorizontal += 1;
                    break;
                case Orientation.S:
                    nextPosition.AxeVertical += 1;
                    break;
                case Orientation.O:
                    nextPosition.AxeHorizontal -= 1;
                    break;
                default:
                    throw new Exception($"Une orientation inconnu est presente");
            }

            return nextPosition;
        }

        private static void TurnRight(Adventurer adventurer)
        {
            switch (adventurer.Orientation)
            {
                case Orientation.N:
                    adventurer.Orientation = Orientation.E;
                    break;
                case Orientation.E:
                    adventurer.Orientation = Orientation.S;
                    break;
                case Orientation.S:
                    adventurer.Orientation = Orientation.O;
                    break;
                case Orientation.O:
                    adventurer.Orientation = Orientation.N;
                    break;
                default:
                    throw new Exception($"Une orientation inconnu est presente sur l'aventurier");
            }
        }

        private static void TurnLeft(Adventurer adventurer)
        {
            switch (adventurer.Orientation)
            {
                case Orientation.N:
                    adventurer.Orientation = Orientation.O;
                    break;
                case Orientation.E:
                    adventurer.Orientation = Orientation.N;
                    break;
                case Orientation.S:
                    adventurer.Orientation = Orientation.E;
                    break;
                case Orientation.O:
                    adventurer.Orientation = Orientation.S;
                    break;
                default:
                    throw new Exception($"Une orientation inconnu est presente sur l'aventurier");
            }
        }

        public static void AddAdventurerInTable(ref AbstractCell[,] cells, string line, ref List<Adventurer> adventurers)
        {
            if (line == null)
            {
                throw new ArgumentNullException("The adventurer cannot be created because the line is null");
            }
            else if (cells == null)
            {
                throw new ArgumentNullException("The adventurer cannot be created because the cells is null");
            }
            else if (adventurers == null)
            {
                throw new ArgumentNullException("The adventurer cannot be created because the adventurer is null");
            }

            string[] strings = line.Replace(" ", "").Split("-");

            if (strings.Length != 6 || strings.Any(s => s == string.Empty))
            {
                throw new ArgumentException("The number of parameter of the line corresponding to the Adventurer is not egual to 6 ", line);
            }
            else if (Parser.ParseInteger(strings[3]) >= cells.GetLength(0) || Parser.ParseInteger(strings[2]) >= cells.GetLength(1))
            {
                throw new ArgumentException("The adventurer cannot be created because the coordinate are no in the cells", line);
            }
            else
            {
                if (cells[Parser.ParseInteger(strings[3]), Parser.ParseInteger(strings[2])] != null && !cells[Parser.ParseInteger(strings[3]), Parser.ParseInteger(strings[2])].IsReachable())
                {
                    throw new ArgumentException("The Adventurer cannot be added because the cell was not available ", line);
                }
                else if (cells[Parser.ParseInteger(strings[3]), Parser.ParseInteger(strings[2])] != null)
                {
                    Adventurer adventurer = Adventurer.BuildAdventurer(strings);
                    (cells[Parser.ParseInteger(strings[3]), Parser.ParseInteger(strings[2])] as TreasureCell).Adventurer = adventurer;
                    adventurers.Add(adventurer);
                }
                else
                {
                    Adventurer adventurer = Adventurer.BuildAdventurer(strings);
                    cells[Parser.ParseInteger(strings[3]), Parser.ParseInteger(strings[2])] = new PlainCell()
                    {
                        Adventurer = adventurer,
                        AxeHorizontal = Parser.ParseInteger(strings[2]),
                        AxeVertical = Parser.ParseInteger(strings[3]),
                    };
                    adventurers.Add(adventurer);
                }
            }
        }
    }
}
