using BibliTech.Roslyn.ViewModels.Common;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.IO;

namespace BibliTech.Roslyn.ViewModels.Terminal
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var commandLineApp = new CommandLineApplication();

            commandLineApp.HelpOption("-? | -h | --help");



            commandLineApp.Execute(args);
        }

    }
}
