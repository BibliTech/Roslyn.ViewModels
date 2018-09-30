using BibliTech.Roslyn.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace BibliTech.Roslyn.ViewModels.Test
{

    public class EntityParserTest
    {

        [Fact]
        public void TestEntityFile()
        {
            var filePath = Utils.GetFirstEntityFile();

            var parser = new EntityParser(filePath);
            var result = parser.ParseToString();

            Utils.WriteResultInDebug(result);

            Assert.NotNull(result);
        }

        [Fact]
        public void TestDbContextFile()
        {
            var filePath = Utils.GetDbContextType();

            var parser = new EntityParser(filePath);
            var result = parser.ParseToString();

            Utils.WriteResultInDebug(result);

            Assert.True(string.IsNullOrWhiteSpace(result));
        }

    }

}
