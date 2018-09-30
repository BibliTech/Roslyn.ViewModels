using BibliTech.Roslyn.ViewModels.Common;
using System;
using System.IO;

namespace BibliTech.Roslyn.ViewModels.Terminal
{
    public class Program
    {

        public static void Main(string[] args)
        {
            string inputFolder;
            string outputFile;

            if (args.Length == 0)
            {
                Console.Write("Enter folder path: ");
                inputFolder = Console.ReadLine();
            }
            else
            {
                inputFolder = args[0];
            }

            if (args.Length < 2)
            {
                Console.Write("Enter output file path: ");
                outputFile = Console.ReadLine();
            }
            else
            {
                outputFile = args[1];
            }

            var parser = new EntityFolderParser(inputFolder);
            var output = parser.ParseToString();
            File.WriteAllText(outputFile, output);
        }

    }
}
