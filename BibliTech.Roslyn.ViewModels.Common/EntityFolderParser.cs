using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BibliTech.Roslyn.ViewModels.Common
{

    public class EntityFolderParser
    {

        string folderPath;
        ScriptOptions options;
        public EntityFolderParser(string folderPath)
        {
            this.folderPath = folderPath;
            this.options = ScriptOptions.Instance;
        }

        public string ParseToString()
        {
            var result = new StringBuilder();
            var currentIndent = 0;

            var shouldWriteNamespace = !string.IsNullOrEmpty(this.options.Namespace);
            if (shouldWriteNamespace)
            {
                result.AppendLine(string.Format("namespace {0}", this.options.Namespace));
                result.AppendLine("{");
                result.AppendLine();

                currentIndent++;
            }

            var files = Directory.GetFiles(this.folderPath, "*.cs");
            foreach (var file in files)
            {
                var parser = new EntityParser(file)
                {
                    InitialIndent = currentIndent,
                };
                result.AppendLine(parser.ParseToString());
            }

            if (shouldWriteNamespace)
            {
                result.AppendLine();
                result.AppendLine("}");
            }

            return result.ToString();
        }

    }

}
