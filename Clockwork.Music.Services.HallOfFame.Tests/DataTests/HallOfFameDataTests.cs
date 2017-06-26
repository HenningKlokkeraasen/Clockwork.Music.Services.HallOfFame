using System;
using System.Collections.Generic;
using System.IO;
using Clockwork.Music.Services.HallOfFame.Core;
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
            var expected = new List<Hall>
            {
                new Hall
                {
                    Id = 1,
                    Name = "Rock and Roll Hall of Fame",
                    InfoUrl = "https://en.wikipedia.org/wiki/Rock_and_Roll_Hall_of_Fame"
                }
            };
            var sut = new HallOfFameRepository(new FileReader(), new JsonParser(), BuildPath());
            
            var actual = sut.GetAll();

            foreach (var hall in actual)
                Console.WriteLine($"{hall.Id} {hall.Name} {hall.InfoUrl}");

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
