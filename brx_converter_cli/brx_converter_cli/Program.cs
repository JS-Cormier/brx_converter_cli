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
            foreach(string file in mainController.FilesToConvert)
            {
                Console.WriteLine(file);
            }
            Console.WriteLine("Found " + mainController.FilesToConvert.Count + " files to convert in " + inputFolder);
            Console.ReadLine();
        }
    }
}
