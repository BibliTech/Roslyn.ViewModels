using System;
using System.Collections.Generic;
using System.Text;

namespace McMaster.Extensions.CommandLineUtils
{
    internal static class Extensions
    {

        public static void ExecuteOptional(this CommandOption option, Action<CommandOption> action)
        {
            if (option.HasValue())
            {
                action(option);
            }
        }

    }
}

namespace BibliTech.Roslyn.ViewModels.Terminal
{
    
}
