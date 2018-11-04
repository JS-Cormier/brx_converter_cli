using System;
using System.Collections.Generic;
using System.IO;
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
    }
}
