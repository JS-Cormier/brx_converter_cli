using brx_converter_cli.Controllers;
using System;
using System.IO;

namespace brx_converter_cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFolder = "";
            var outputFolder = "";
            bool foldersAreValid = false;
            while(!foldersAreValid)
            {
                Console.Write("Enter the input folder: ");
                inputFolder = Console.ReadLine();
                Console.Write("Enter the output folder: ");
                outputFolder = Console.ReadLine();
                try
                {
                    foldersAreValid = ValidateFolders(inputFolder, outputFolder);
                }
                catch(Exception e)
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(e.Message + "\n");
                    Console.ResetColor();
                }
            }
            var mainController = new MainController(inputFolder, outputFolder);
            Console.WriteLine("\nFound " + mainController.FilesToConvert.Count + " files to convert in " + inputFolder);

            //Confirmation
            string conf = "";
            while (!(string.Equals("y", conf, StringComparison.OrdinalIgnoreCase) || string.Equals("n", conf, StringComparison.OrdinalIgnoreCase)))
            {
                Console.Write("Proceed with conversion ([y]es or [n]o)? ");
                conf = Console.ReadLine();
            }
            if(string.Equals("y", conf, StringComparison.OrdinalIgnoreCase))
            {
                mainController.ConvertAll();
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\n\nConversion completed. MP3 files located in : " + outputFolder);
                Console.ResetColor();
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\nConversion aborted");
                Console.ResetColor();
            }
            Console.Write("Press any key to exit...");
            Console.Read();
        }

        private static bool ValidateFolders(string inputFolder, string outputFolder)
        {
            if (!Directory.Exists(inputFolder)) throw new Exception("Input folder does not exist");
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch
            {
                throw new Exception("Invalid output folder");
            }

            return true;
        }
    }
}
