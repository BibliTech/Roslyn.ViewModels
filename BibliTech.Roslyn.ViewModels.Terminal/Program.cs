using BibliTech.Roslyn.ViewModels.Common;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BibliTech.Roslyn.ViewModels.Terminal
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var app = new CommandLineApplication();

            app.HelpOption("-? | -h | --help");

            var argInput = app.Argument("Input Folder", "Input folder.").IsRequired();
            var argOutput = app.Argument("Output File", "Output file.").IsRequired();

            var optClassNameFormat = app.Option(
                "-c|--ClassName <format>",
                "String format for View Model class name. Default: {0}EntityViewModel",
                CommandOptionType.SingleValue);

            var optNamespace = app.Option(
                "-ns|--Namespace <name>",
                "Namespace for the output file. Default: ViewModels",
                CommandOptionType.SingleValue);

            var optNoPartial = app.Option(
                "-np|--NoPartial",
                "Do not add the partial directive to the class declaration. Default: False",
                CommandOptionType.NoValue);

            var optClassAttribute = app.Option(
                "-attr|--ClassAttribute <string>",
                "Append Class attribute string before every Class declaration",
                CommandOptionType.SingleValue);

            var optBases = app.Option(
                "-bases|--Bases <string>",
                "Append inheritance members in every Class declaration",
                CommandOptionType.SingleValue);

            var optForce = app.Option(
                "-f|--Force",
                "Allow overwrite output file if it already exists",
                CommandOptionType.NoValue);

            var optUsings = app.Option(
                "-u|--Using",
                "Declare using namespaces. System is included by default.",
                CommandOptionType.MultipleValue);

            app.OnExecute(() =>
            {
                var scriptOptions = ScriptOptions.Instance;

                optClassNameFormat.ExecuteOptional(o => scriptOptions.ClassNameFormat = o.Value());
                optNamespace.ExecuteOptional(o => scriptOptions.Namespace = o.Value());
                optNoPartial.ExecuteOptional(o => scriptOptions.NoPartial = true);
                optClassAttribute.ExecuteOptional(o => scriptOptions.ClassAttribute = o.Value());
                optBases.ExecuteOptional(o => scriptOptions.Bases = o.Value());
                optForce.ExecuteOptional(o => scriptOptions.Force = true);
                optUsings.ExecuteOptional(o => scriptOptions.UsingDirectives.AddRange(o.Values));
                
                if (File.Exists(argOutput.Value) && !scriptOptions.Force)
                {
                    Console.WriteLine("Output file already exist. Please use -f or --Force to overwrite.");
                    return;
                }

                var parser = new EntityFolderParser(argInput.Value);
                var result = parser.ParseToString();
                File.WriteAllText(argOutput.Value, result, Encoding.UTF8);
            });

            app.OnValidationError(validation =>
            {
                Console.WriteLine(validation.ErrorMessage);
            });

            app.Execute(args);
        }

    }
}
