using System;

namespace brx_converter_cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFolder = args[0];
            var outputFolder = args[1];
            Console.WriteLine("Input:" + inputFolder + "/ Output:" + outputFolder);
            Console.ReadLine();
        }
    }
}
