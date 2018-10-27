using BibliTech.Roslyn.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BibliTech.Roslyn.ViewModels.Test
{

    public class EntityFolderParserTest
    {

        [Fact]
        public void ParseToStringTest()
        {
            ScriptOptions.Instance.NoPartial = true;
            ScriptOptions.Instance.ClassAttribute = "EntityViewModel";
            ScriptOptions.Instance.Bases = "IEntityViewModel";

            var folderPath = Utils.DataClassesFolder;
            var parser = new EntityFolderParser(folderPath);
            var result = parser.ParseToString();

            Utils.WriteResultInDebug(result);
            Assert.False(string.IsNullOrEmpty(result));
        }

    }

}
