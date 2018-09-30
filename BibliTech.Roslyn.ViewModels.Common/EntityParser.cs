using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BibliTech.Roslyn.ViewModels.Common
{

    public class EntityParser
    {

        public int InitialIndent { get; set; } = 0;

        string filePath;
        public EntityParser(string filePath)
        {
            this.filePath = filePath;
        }

        public string ParseToString()
        {
            var input = File.ReadAllText(this.filePath);
            var tree = CSharpSyntaxTree.ParseText(input);
            var root = tree.GetRoot() as CompilationUnitSyntax;

            var walker = new EntitySyntaxWalker()
            {
                CurrentIndent = this.InitialIndent,
            };
            walker.Visit(root);

            return walker.ToString();
        }

    }

}
