using ChasseAuTresor.Model.AdventurerModel;
using ChasseAuTresor.CellModel;
using System.Text;

namespace ChasseAuTresor.GameDataModel
{
    public class GameData
    {
        public AbstractCell[,] cells;

        public List<Adventurer> adventurers;

        public GameData()
        {
            adventurers = new List<Adventurer>();
        }

        public void ReadGameDataFromLines(List<string> lines)
        {
            if(lines != null && lines.Any(l => l.StartsWith("C")))
            {
                cells = AbstractCell.CreateAbstractCellTableFromLine(lines.Single(l => l.StartsWith("C")));

                AbstractCell.AddCellInTable(ref cells, ref adventurers, lines);
            }
            else
            {
                throw new ArgumentException("The file is not in correct format, missing the Map line.");
            }
        }

        public string WriteToFile()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("# {C comme Carte} - {Nb. de case en largeur} - {Nb. de case en hauteur}");
            stringBuilder.AppendLine(AbstractCell.WriteMapInFile(ref cells));

            stringBuilder.AppendLine("# {M comme Montagne} - {Axe horizontal} - {Axe vertical}");
            stringBuilder.AppendLine("# {T comme Trésor} - {Axe horizontal} - {Axe vertical} - {Nb. de trésors restants}");
            stringBuilder.AppendLine("# {A comme Aventurier} - {Nom de l’aventurier} - {Axe horizontal} - {Axe vertical} - {Orientation} - {Nb.trésors ramassés}");
            stringBuilder.AppendLine(AbstractCell.WriteCellsInFile(ref cells));

            return stringBuilder.ToString();
        }

        public void PlaySimulation()
        {
            int i = 0;
            int  maxNumberTurn = adventurers != null && adventurers.Count() > 0 ? adventurers.Max(a => a.MovementSequency.Length) : 0;

            while (i < maxNumberTurn)
            {
                foreach (Adventurer adventurer in adventurers)
                {
                    if (adventurer.MovementSequency.Length > i)
                    {
                        char nextAction = adventurer.MovementSequency.ElementAt(i);

                        Adventurer.RealiseAction(nextAction, adventurer, ref cells);
                    }
                }

                // AbstractCell.DisplayArrayCell(cells);
                i++;
            }
        }
    }
}
