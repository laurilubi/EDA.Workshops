using Xunit;

namespace TDD
{
    public class LocationTests
    {
        [Theory]
        [InlineData(Location.B, -5)]
        [InlineData(Location.Factory, 0)]
        [InlineData(Location.Port, 1)]
        [InlineData(Location.A, 5)]
        [InlineData(null, 2)]
        public void FromPosTest(Location? expected, int pos)
        {
            Assert.Equal(expected, LocationExtension.FromPos(pos));
        }
    }
}