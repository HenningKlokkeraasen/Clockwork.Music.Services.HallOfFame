using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using LightBDD.Framework;
using LightBDD.Framework.Scenarios.Extended;
using LightBDD.NUnit3;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Clockwork.Music.Services.HallOfFame.Tests.FeatureTests
{
    [FeatureDescription(
        @"In order to get data
        I want to use a repository")]
    public partial class Repo_feature
    {
        [Scenario]
        public void All() => Runner.RunScenario(
            when => calling_GetAll(),
            then => it_should_return_items_of_type());
    }

    public partial class Repo_feature : FeatureFixture
    {
        private readonly Fixture _fixture = new Fixture();
        private string _filename;
        private Mock<ISimpleDocumentDb> _docDbMock;
        private HallOfFameRepository _sut;

        [SetUp]
        public void SetUp()
        {
            _filename = "hallsoffame.json";
            _docDbMock = new Mock<ISimpleDocumentDb>();
            _docDbMock.Setup(d => d.Read<IList<HallOfFame>>(_filename))
                .Returns(_fixture.CreateMany<HallOfFame>().ToList());
            _sut = new HallOfFameRepository(_docDbMock.Object);
        }

        private IList<HallOfFame> _actual;

        private void calling_GetAll() => _actual = _sut.GetAll();

        private void it_should_return_items_of_type() => _actual.Should().AllBeOfType<HallOfFame>();
    }
}