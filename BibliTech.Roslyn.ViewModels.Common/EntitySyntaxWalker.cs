using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BibliTech.Roslyn.ViewModels.Common
{

    public class EntitySyntaxWalker : CSharpSyntaxWalker
    {

        public int CurrentIndent { get; set; }

        StringBuilder result;
        ScriptOptions options;
        string indent;
        public EntitySyntaxWalker()
        {
            this.result = new StringBuilder();
            this.options = ScriptOptions.Instance;
            this.indent = new string(' ', this.options.SpacesPerIndent);
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
            var viewModelName = string.Format(this.options.ViewModelClassName, entityName);

            this.WriteIndent();
            this.result.AppendLine(string.Format(this.options.ViewModelDeclarationFormat, viewModelName));

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
                this.result.AppendLine(string.Format(options.PropertyDeclaration,
                    propertyTypeName, propertyName));
            }
        }

        private bool ShouldWriteProperty(PropertyDeclarationSyntax node)
        {
            var propertyType = node.Type;

            if (this.options.AllowPredefinedType && propertyType is PredefinedTypeSyntax)
            {
                return true;
            }

            var name = propertyType.ToString();
            if (this.options.AllowedTypes != null && this.options.AllowedTypes.Contains(name))
            {
                return true;
            }

            return false;
        }

        private bool ShouldIgnore(ClassDeclarationSyntax node)
        {
            if (node.BaseList?.Types != null)
            {
                foreach (var baseType in node.BaseList.Types)
                {
                    var name = baseType.Type.TryGetInferredMemberName();

                    if (this.options.IgnoredBaseClasses.Contains(name))
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
