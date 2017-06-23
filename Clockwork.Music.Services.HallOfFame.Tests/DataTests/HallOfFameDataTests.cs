using System.Collections.Generic;
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

            var sut = new HallOfFameRepository();

            var actual = sut.GetAll();

            actual.ShouldBeEquivalentTo(expected);
        }
    }
}
