using System;
using System.Collections.Generic;
using System.IO;

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
            PopulateFilesList(inputFolder);
        }

        private void PopulateFilesList(string folder)
        {
            string[] subfolders = Directory.GetDirectories(folder);
            foreach(string sub in subfolders)
            {
                PopulateFilesList(sub);
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
                UpdateProgressBar(FilesToConvert.IndexOf(file) + 1);
                Console.Write(" - Converting " + Path.GetRelativePath(_inputFolder, file));
                byte[] bytes = System.IO.File.ReadAllBytes(file);
                byte[] reversed = InvertBytesArray(bytes);
                string outputPath = _outputFolder + "\\" + Path.GetRelativePath(_inputFolder, file);
                outputPath = Path.ChangeExtension(outputPath, ".mp3");
                string outputPathFolder = Path.GetDirectoryName(outputPath);
                if(!Directory.Exists(outputPathFolder))
                {
                    Directory.CreateDirectory(outputPathFolder);
                }
                System.IO.File.WriteAllBytes(outputPath, reversed);
            }
        }

        private void UpdateProgressBar(int progress)
        {
            float perc = ((float)progress / FilesToConvert.Count) * 100;
            double tenPercents = Math.Truncate(perc / 10);
            string progressBar = "||";
            for(int i = 0; i < tenPercents; i++)
            {
                progressBar += (char)0x25A0;
            }
            for(double i = tenPercents; i < 10; i++)
            {
                progressBar += "-";
            }
            progressBar += "|| (" + progress + "/" + FilesToConvert.Count + ")";
            Console.Write(progressBar);
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
