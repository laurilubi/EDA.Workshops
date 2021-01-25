using System;
using System.Linq;
using Xunit;

namespace TDD
{
    public class AppTests
    {
        [Theory]
        [InlineData(5, "B")]
        [InlineData(5, "A")]
        [InlineData(5, "A", "B")]
        [InlineData(7, "A", "B", "B")]
        [InlineData(29, "A", "A", "B", "A", "B", "B", "A", "B")]
        [InlineData(29, "A", "A", "A", "A", "B", "B", "B", "B")]
        [InlineData(49, "B", "B", "B", "B", "A", "A", "A", "A")]
        public void IntegrationTest(int expectedHour, params string[] packages)
        {
            var typedPackages = packages
                .Select(_ => new Package {Destination = Enum.Parse<DestinationType>(_)})
                .ToList();
            var app = new App(typedPackages);

            while (app.State.IsDone == false)
                app.HandleHour();

            Assert.Equal(expectedHour, app.State.Hour);
        }
    }
}