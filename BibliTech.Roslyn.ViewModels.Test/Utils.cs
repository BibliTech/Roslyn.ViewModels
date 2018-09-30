using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace BibliTech.Roslyn.ViewModels.Test
{

    internal static class Utils
    {

        public static readonly string DataFolder = @"Data";
        public static readonly string DataClassesFolder = DataFolder + @"\Classes";

        public static string GetFirstEntityFile()
        {
            return Path.Combine(DataClassesFolder, "Account.cs");
        }

        public static string GetDbContextType()
        {
            return Path.Combine(DataClassesFolder, "BibliIdContext.cs");
        }

        const string DebugFileName = @"D:\Temp\test.txt";
        [Conditional("DEBUG")]
        public static void WriteResultInDebug(object result)
        {
            File.WriteAllText(DebugFileName, result?.ToString());
        }

        [Conditional("DEBUG")]
        public static void WriteJsonResultInDebug(object result)
        {
            File.WriteAllText(DebugFileName, JsonConvert.SerializeObject(result ?? new { }));
        }

    }

}
