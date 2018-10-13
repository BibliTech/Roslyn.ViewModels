using System;
using System.Collections.Generic;
using System.Text;

namespace McMaster.Extensions.CommandLineUtils
{
    internal static class Extensions
    {

        public static void OptionalOption(this CommandLineApplication commandLineApp,
            string template, string description, CommandOptionType optionType, Action<CommandOption> configuration)
        {
            commandLineApp.Option(
                template,
                description,
                optionType,
                (option) =>
                {
                    if (option.HasValue())
                    {
                        configuration(option);
                    }
                });
        }

    }
}

namespace BibliTech.Roslyn.ViewModels.Terminal
{
    
}
