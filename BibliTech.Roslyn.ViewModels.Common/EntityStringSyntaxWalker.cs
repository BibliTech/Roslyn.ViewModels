using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BibliTech.Roslyn.ViewModels.Common
{

    public class EntityStringSyntaxWalker : CSharpSyntaxWalker
    {

        public int CurrentIndent { get; set; }

        StringBuilder result;
        ScriptOptions options;
        string indent;
        public EntityStringSyntaxWalker()
        {
            this.result = new StringBuilder();
            this.options = ScriptOptions.Instance;
            this.indent = new string(' ', ScriptOptions.SpacesPerIndent);
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            if (!this.ShouldIgnore(node))
            {
                this.WriteViewModel(node);
            }

            base.VisitClassDeclaration(node);
        }

        private void WriteViewModel(ClassDeclarationSyntax node)
        {
            var entityName = node.Identifier.ValueText;
            var viewModelName = string.Format(this.options.ClassNameFormat, entityName);

            this.WriteIndent();
            this.result.AppendLine(string.Format(
                "public {2}class {0}",
                viewModelName,
                entityName,
                this.options.NoPartial ? "" : "partial "));

            this.WriteIndent();
            this.result.AppendLine("{");

            this.CurrentIndent++;

            foreach (var member in node.Members)
            {
                if (member is PropertyDeclarationSyntax propertyMember)
                {
                    this.WriteProperty(propertyMember);
                }
            }

            this.CurrentIndent--;

            this.WriteIndent();
            this.result.AppendLine("}");


            this.result.AppendLine();
            this.result.AppendLine();
        }

        private void WriteProperty(PropertyDeclarationSyntax node)
        {
            if (this.ShouldWriteProperty(node))
            {
                var propertyTypeName = node.Type.ToString();
                var propertyName = node.Identifier;

                this.WriteIndent();
                this.result.AppendLine(string.Format("public {0} {1} {{ get; set; }}",
                    propertyTypeName, propertyName));
            }
        }

        private bool ShouldWriteProperty(PropertyDeclarationSyntax node)
        {
            var propertyType = node.Type;

            return propertyType is PredefinedTypeSyntax;
        }

        private bool ShouldIgnore(ClassDeclarationSyntax node)
        {
            if (node.BaseList?.Types != null)
            {
                foreach (var baseType in node.BaseList.Types)
                {
                    var name = baseType.Type.TryGetInferredMemberName();

                    if (name.Equals("dbcontext", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void WriteIndent()
        {
            for (int i = 0; i < this.CurrentIndent; i++)
            {
                this.result.Append(this.indent);
            }
        }

        public override string ToString()
        {
            return this.result.ToString();
        }

    }

}
