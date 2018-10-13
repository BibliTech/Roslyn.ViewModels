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
                "Namespace for the output file. Default: False",
                CommandOptionType.NoValue);

            app.OnExecute(() =>
            {
                var scriptOptions = ScriptOptions.Instance;

                optClassNameFormat.ExecuteOptional(o => scriptOptions.ClassNameFormat = o.Value());
                optNamespace.ExecuteOptional(o => scriptOptions.Namespace = o.Value());
                optNoPartial.ExecuteOptional(o => scriptOptions.NoPartial = true);

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
