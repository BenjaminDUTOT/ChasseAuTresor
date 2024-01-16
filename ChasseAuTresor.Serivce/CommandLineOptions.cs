using CommandLine;

namespace ChasseAuTresor
{
    public class CommandLineOptions
    {
        [Option(shortName: 'i', longName: "input", Required = false, HelpText = "Input parameter file data")]
        public string InputPath { get; set; }

        [Option(shortName: 'o', longName: "output", Required = false, HelpText = "Output parameter file data")]
        public string OutPutPath { get; set; }
    }
}
