using CommandLine;
using ChasseAuTresor.GameDataModel;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ChasseAuTresor.Test")]
namespace ChasseAuTresor
{
    internal class Program
    {
        static string parameterPath = @"../../../data/DefaultParameter/parameter_Default.txt";
        static string resultPath = @"../../../data/result.txt";

        internal static void Main(string[] args)
        {
            ParserResult<CommandLineOptions> parsedArgument = Parser.Default.ParseArguments<CommandLineOptions>(args);

            //Read data from file
            List<string> lines = ReadFile(parsedArgument.Value.InputPath ?? parameterPath);

            // Create Simulation
            GameData gameData = new GameData();
            gameData.ReadGameDataFromLines(lines);

            // Display the map in the console
            // AbstractCell.DisplayArrayCell(gameData.cells);

            // Play Simulation
            gameData.PlaySimulation();

            // Write file with Simulation Result
            WriteFile(parsedArgument.Value.OutPutPath ?? resultPath, gameData.WriteToFile());
        }

        public static List<string> ReadFile(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllLines(path).ToList();
            }
            else
            {
                throw new FileNotFoundException(path);
            }
        }

        public static void WriteFile(string path, string content)
        {
            File.WriteAllText(path, content);
        }
    }
}
