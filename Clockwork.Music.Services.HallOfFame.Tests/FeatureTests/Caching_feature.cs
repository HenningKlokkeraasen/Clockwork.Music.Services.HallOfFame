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
        @"In order to avoid reading from repository every time
        I want to use a cache")]
    public partial class Caching_feature
    {
        [Scenario]
        public void Cache_is_empty_and_repo_has_data() => Runner.RunScenario(
            given => an_empty_cache(),
            and => a_nonempty_repo(),
            when => calling_GetAll(),
            then => it_gets_from_cache(),
            and => it_gets_from_repo(),
            and => it_stores_in_cache(),
            and => it_returns_result()
        );

        [Scenario]
        public void Cache_is_empty_and_repo_is_empty() => Runner.RunScenario(
            given => an_empty_cache(),
            and => an_empty_repo(),
            when => calling_GetAll(),
            then => it_gets_from_cache(),
            and => it_gets_from_repo(),
            and => it_does_not_store_in_cache(),
            and => it_returns_empty()
        );

        [Scenario]
        public void Cache_is_not_empty() => Runner.RunScenario(
            given => a_nonempty_cache(),
            when => calling_GetAll(),
            then => it_does_not_get_from_repo(),
            and => it_does_not_store_in_cache(),
            and => it_returns_result());
    }

    public partial class Caching_feature : FeatureFixture
    {
        private Fixture _fixture = new Fixture();
        private string _cacheKey;
        private Mock<ICache<HallOfFame>> _cacheMock;
        private Mock<IRepository<HallOfFame>> _repoMock;
        private HallOfFameService _sut;
        private IList<HallOfFame> _expected;
        private IList<HallOfFame> _actual;

        [SetUp]
        public void SetUp()
        {
            _cacheKey = _fixture.Create<string>();
            _cacheMock = new Mock<ICache<HallOfFame>>();
            _repoMock = new Mock<IRepository<HallOfFame>>();
            _sut = new HallOfFameService(_repoMock.Object, _cacheMock.Object, _cacheKey);
            _expected = _fixture.CreateMany<HallOfFame>().ToList();
        }

        private void an_empty_cache() => _cacheMock.Setup(c => c.Get(_cacheKey)).Returns(new List<HallOfFame>());

        private void a_nonempty_cache() => _cacheMock.Setup(c => c.Get(_cacheKey)).Returns(_expected);

        private void a_nonempty_repo() => _repoMock.Setup(r => r.GetAll()).Returns(_expected);

        private void an_empty_repo() => _repoMock.Setup(r => r.GetAll()).Returns(new List<HallOfFame>());

        private void calling_GetAll() => _actual = _sut.GetAll();

        private void it_gets_from_cache() => _cacheMock.Verify(c => c.Get(_cacheKey), Times.Once);

        private void it_gets_from_repo() => _repoMock.Verify(r => r.GetAll(), Times.Once);

        private void it_does_not_get_from_repo() => _repoMock.Verify(r => r.GetAll(), Times.Never);

        private void it_stores_in_cache() => _cacheMock.Verify(c => c.Store(_expected, _cacheKey), Times.Once);

        private void it_does_not_store_in_cache() => _cacheMock.Verify(c => c.Store(It.IsAny<IList<HallOfFame>>(), _cacheKey), Times.Never);

        private void it_returns_result() => _actual.ShouldBeEquivalentTo(_expected);

        private void it_returns_empty() => _actual.Should().BeEmpty();
    }
}
