using ChasseAuTresor.Model.AdventurerModel;

namespace ChasseAuTresor.Utils
{
    public class Parser
    {
        public static int ParseInteger(string integerToParse)
        {
            int result;

            if (int.TryParse(integerToParse, out result) == true)
            {
                return result;
            }
            else
            {
                throw new ArgumentException($"Une erreur c'est produite lors de la convertion en entier : {integerToParse}");
                
            }
        }

        public static Orientation ParseOrientation(string orientationToParse)
        {
            Orientation result;

            if (!string.IsNullOrEmpty(orientationToParse) && Orientation.TryParse(orientationToParse.ToUpper(), out result))
            {
                return result;
            }
            else
            {
                throw new ArgumentException($"Une erreur c'est produite lors de la convertion en Orientation : {orientationToParse}");
            }
        }
    }
}
