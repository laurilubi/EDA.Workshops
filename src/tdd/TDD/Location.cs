using System;

namespace TDD
{
    public enum Location
    {
        B = -5,
        Factory = 0,
        Port = 1,
        A = 5,
    }

    public static class LocationExtension
    {
        public static Location? FromPos(int pos)
        {
            if (Enum.IsDefined(typeof(Location), pos) == false) return null;
            return (Location) pos;
        }
    }
}