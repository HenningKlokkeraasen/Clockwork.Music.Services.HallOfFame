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
    [FeatureDescription("")]
    public partial class Get_HOF_feature
    {
        [Scenario]
        public void Item_does_not_exist() => Runner.RunScenario(
            given => a_list_of_items(),
            and => the_item_is_not_in_the_list(),
            when => calling_Get(),
            then => it_returns_null());

        [Scenario]
        public void Item_exists() => Runner.RunScenario(
            given => a_list_of_items(),
            and => the_item_is_in_the_list(),
            when => calling_Get(),
            then => it_returns_the_item());
    }

    public partial class Get_HOF_feature : FeatureFixture
    {
        private readonly Fixture _fixture = new Fixture();
        private Mock<ICache<IList<PublicContracts.HallOfFame>>> _cacheMock;
        private Mock<IRepository<PublicContracts.HallOfFame>> _repoMock;
        private HallOfFameService _sut;
        private IList<PublicContracts.HallOfFame> _list;
        private int _id;
        private PublicContracts.HallOfFame _expected;
        private PublicContracts.HallOfFame _actual;

        [SetUp]
        public void SetUp()
        {
            _cacheMock = new Mock<ICache<IList<PublicContracts.HallOfFame>>>();
            _repoMock = new Mock<IRepository<PublicContracts.HallOfFame>>();
            _sut = new HallOfFameService(_repoMock.Object, _cacheMock.Object, It.IsAny<string>());
            _list = _fixture.CreateMany<PublicContracts.HallOfFame>().ToList();
            _id = _fixture.Create<int>();
            _expected = new PublicContracts.HallOfFame {Id = _id};
        }

        private void a_list_of_items() => _cacheMock.Setup(c => c.Get(It.IsAny<string>())).Returns(_list);

        private void the_item_is_in_the_list() => _list.Add(_expected);

        private void calling_Get() => _actual = _sut.Get(_id);

        private void it_returns_the_item() => _actual.ShouldBeEquivalentTo(_expected);

        private void the_item_is_not_in_the_list() {}

        private void it_returns_null() => _actual.Should().BeNull();
    }
}
