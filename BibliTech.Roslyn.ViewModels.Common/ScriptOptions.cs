using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BibliTech.Roslyn.ViewModels.Common
{

    public class ScriptOptions
    {

        public static readonly ScriptOptions Instance = new ScriptOptions();

        public HashSet<string> IgnoredBaseClasses { get; set; }
        public string ViewModelClassName { get; set; }
        public string OutputNamespace { get; set; }
        public int SpacesPerIndent { get; set; }
        public string ViewModelDeclarationFormat { get; set; }
        public bool AllowPredefinedType { get; set; }
        public HashSet<string> AllowedTypes { get; set; }
        public string PropertyDeclaration { get; set; }
        public string FilePattern { get; set; }
        public string NamespaceDeclarationFormat { get; set; }

        private ScriptOptions()
        {
            var optionsFileContent = File.ReadAllText("options.json");
            JsonConvert.PopulateObject(optionsFileContent, this);
        }

    }

}
