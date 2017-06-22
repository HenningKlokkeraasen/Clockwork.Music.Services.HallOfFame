using System.Collections.Generic;
using FluentAssertions;
using LightBDD.Framework;
using LightBDD.Framework.Scenarios.Extended;
using LightBDD.NUnit3;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Clockwork.Music.Services.HallOfFame.Tests
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

        [Scenario]
        public void One() => Runner.RunScenario(
            when => calling_Get(),
            then => it_should_return_item_of_type());
    }

    public partial class Repo_feature : FeatureFixture
    {
        private Fixture _fixture;
        private int _id;
        private HallOfFameRepository _sut;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
            _id = _fixture.Create<int>();
            _sut = new HallOfFameRepository();
        }
    }

    public partial class Repo_feature
    {
        private IList<HallOfFame> _actual;

        private void calling_GetAll() => _actual = _sut.GetAll();

        private void it_should_return_items_of_type() => _actual.Should().AllBeOfType<HallOfFame>();
    }

    public partial class Repo_feature
    {
        private HallOfFame _actualItem;

        private void calling_Get() => _actualItem = _sut.Get(_id);

        private void it_should_return_item_of_type() => _actualItem.Should().BeOfType<HallOfFame>();
    }
}