using brx_converter_cli.Controllers;
using System;

namespace brx_converter_cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFolder = args[0];
            var outputFolder = args[1];
            var mainController = new MainController(inputFolder, outputFolder);
            Console.WriteLine("Found " + mainController.FilesToConvert.Count + " files to convert in " + inputFolder);
            mainController.ConvertAll();
            Console.WriteLine("");
            Console.WriteLine("Conversion completed. MP3 files located in : " + outputFolder + "\\");
            Console.Write("Press any key to exit...");
            Console.Read();
        }
    }
}
