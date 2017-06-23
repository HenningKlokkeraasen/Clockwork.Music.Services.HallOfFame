using System.Collections.Generic;
using FluentAssertions;
using LightBDD.Framework;
using LightBDD.Framework.Scenarios.Extended;
using LightBDD.NUnit3;
using NUnit.Framework;

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
        private HallOfFameRepository _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new HallOfFameRepository();
        }

        private IList<HallOfFame> _actual;

        private void calling_GetAll() => _actual = _sut.GetAll();

        private void it_should_return_items_of_type() => _actual.Should().AllBeOfType<HallOfFame>();
    }
}