using System.Collections.Generic;
using System.Linq;
using Clockwork.Music.Services.HallOfFame.Core;
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
        private string _json;
        private Mock<IFileReader> _fileReaderMock;
        private Mock<IJsonParser> _jsonParserMock;
        private HallOfFameRepository _sut;

        [SetUp]
        public void SetUp()
        {
            _filename = _fixture.Create<string>();
            _json = _fixture.Create<string>();
            _fileReaderMock = new Mock<IFileReader>();
            _jsonParserMock = new Mock<IJsonParser>();
            _fileReaderMock.Setup(d => d.ReadAsString(_filename)).Returns(_json);
            _jsonParserMock.Setup(j => j.Parse<IList<Hall>>(_json))
                .Returns(_fixture.CreateMany<Hall>().ToList());
            _sut = new HallOfFameRepository(_fileReaderMock.Object, _jsonParserMock.Object, _filename);
        }

        private IList<Hall> _actual;

        private void calling_GetAll() => _actual = _sut.GetAll();

        private void it_should_return_items_of_type() => _actual.Should().AllBeOfType<Hall>();
    }
}