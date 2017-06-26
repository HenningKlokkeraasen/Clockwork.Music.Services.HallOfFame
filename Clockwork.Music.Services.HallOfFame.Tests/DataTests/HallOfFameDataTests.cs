using System;
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
            var sut = new HallOfFameRepository(new FileReader(), new JsonParser(), BuildPath());
            
            var actual = sut.GetAll();

            Console.WriteLine("ID EST. NAME (URL)");
            foreach (var hall in actual)
                Console.WriteLine($"{hall.Id.ToString("00")} {hall.YearEstabllished.ToString("0000")} {hall.Name} ({hall.InfoUrl})");

            actual.Should().AllBeOfType<PublicContracts.HallOfFame>();
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
