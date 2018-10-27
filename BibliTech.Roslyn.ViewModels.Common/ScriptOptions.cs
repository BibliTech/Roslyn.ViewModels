using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BibliTech.Roslyn.ViewModels.Common
{

    public class ScriptOptions
    {
        public const int SpacesPerIndent = 4;

        public static readonly ScriptOptions Instance = new ScriptOptions();

        public string ClassNameFormat { get; set; } = "{0}EntityViewModel";
        public string Namespace { get; set; } = "ViewModels";
        public bool NoPartial { get; set; } = false;

        public string ClassAttribute { get; set; } = null;
        public string Bases { get; set; } = null;

        public string InputFolder { get; set; } = null;
        public string OutputFile { get; set; } = null;

        private ScriptOptions() { }
        
    }

}
