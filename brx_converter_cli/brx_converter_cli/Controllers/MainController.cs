using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace brx_converter_cli.Controllers
{
    class MainController
    {
        private string _inputFolder;
        private string _outputFolder;
        private List<string> _filesToConvert;

        public List<string> FilesToConvert
        {
            private set => _filesToConvert = value;
            get => _filesToConvert;
        }

        public MainController(string inputFolder, string outputFolder)
        {
            _inputFolder = inputFolder;
            _outputFolder = outputFolder;
            FilesToConvert = new List<string>();
            populateFilesList(inputFolder);
        }

        private void populateFilesList(string folder)
        {
            string[] subfolders = Directory.GetDirectories(folder);
            foreach(string sub in subfolders)
            {
                populateFilesList(sub);
            }

            foreach(string file in Directory.GetFiles(folder))
            {
                if(file.EndsWith(".BR4") || file.EndsWith(".BR5"))
                {
                    FilesToConvert.Add(file);
                }
            }
        }

        public void ConvertAll()
        {
            foreach(string file in FilesToConvert)
            {
                ClearLine();
                Console.Write("Converting " + file);
                byte[] bytes = System.IO.File.ReadAllBytes(file);
                byte[] reversed = InvertBytesArray(bytes);
                string outputPath = _outputFolder + file.Replace(_inputFolder, "");
                outputPath = outputPath.Substring(0, outputPath.Length - 4) + ".mp3";
                string outputPathFolder = Path.GetDirectoryName(outputPath);
                if(!Directory.Exists(outputPathFolder))
                {
                    Directory.CreateDirectory(outputPathFolder);
                }
                System.IO.File.WriteAllBytes(outputPath, reversed);
            }
        }

        private byte[] InvertBytesArray(byte[] bytes)
        {
            List<byte> reversed = new List<byte>();
            foreach(byte @byte in bytes)
            {
                byte reversedByte = @byte;
                reversedByte ^= byte.MaxValue;
                reversed.Add(reversedByte);
            }
            return reversed.ToArray();
        }

        private void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
    }
}
