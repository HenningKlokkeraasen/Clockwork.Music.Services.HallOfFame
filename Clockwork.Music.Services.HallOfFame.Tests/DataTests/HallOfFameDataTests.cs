using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace Clockwork.Music.Services.HallOfFame.Tests.DataTests
{
    [TestFixture]
    public class HallOfFameDataTests
    {
        [Test]
        public void GetAll_Should_Return_Correct_Data()
        {
            var expected = new List<HallOfFame>
            {
                new HallOfFame
                {
                    Id = 1,
                    Name = "Rock and Roll Hall of Fame",
                    InfoUrl = "https://en.wikipedia.org/wiki/Rock_and_Roll_Hall_of_Fame"
                }
            };
            var path = BuildPath();
            var sut = new SimpleDocumentDb();
            
            var actual = sut.Read<IList<HallOfFame>>(path);

            actual.ShouldBeEquivalentTo(expected);
        }

        // Assumes currentContextTestDirectory is \bin\debug
        private static string BuildPath()
        {
            var currentContextTestDirectory = TestContext.CurrentContext.TestDirectory;
            var directoryInfo = new DirectoryInfo(currentContextTestDirectory);
            var rootFolder = directoryInfo.Parent.Parent.Parent;
            var path = Path.Combine(rootFolder.FullName, "Data", "hallsoffame.json");
            return path;
        }
    }
}
