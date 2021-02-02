using System;
using System.Linq;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace TDD
{
    public class AppTests
    {
        private readonly ITestOutputHelper testOutputHelper;

        public AppTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

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
                .Select((destCode, index) => new Package(index + 1, Location.Factory, Enum.Parse<Location>(destCode)))
                .ToList();
            var app = new App(typedPackages);

            while (app.State.IsDone == false)
                app.HandleHour();

            app.History.ForEach(ev =>
                testOutputHelper.WriteLine(JsonConvert.SerializeObject(ev,
                    new Newtonsoft.Json.Converters.StringEnumConverter())));

            Assert.Equal(expectedHour, app.State.Hour);
        }
    }
}