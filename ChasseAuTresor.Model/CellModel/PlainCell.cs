using ChasseAuTresor.Model.AdventurerModel;

namespace ChasseAuTresor.CellModel
{
    public class PlainCell : AbstractCell
    {
        public override TypeCell cellType => TypeCell.Plain;

        public Adventurer Adventurer { get; set; }

        public override string GetDisplayType()
        {
            return Adventurer == null ? $"{(char)cellType}" : Adventurer.GetDisplayText();
        }

        public override bool IsReachable()
        {
            return Adventurer == null;
        }

        public override void AddAdventurer(Adventurer adventurer)
        {
            this.Adventurer = adventurer;
        }

        public override void RemoveAdventurer()
        {
            this.Adventurer = null;
        }

        public override bool TryGetWriteText(out string result)
        {
            bool success = false;
            if (Adventurer == null)
            {
                result = null;
            }
            else
            {
                result = Adventurer.WriteAdventurer(Adventurer);
                success = true;
            }

            return success;
        }
    }
}
