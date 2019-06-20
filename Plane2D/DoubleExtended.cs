using System;

namespace Plane2D
{
    public static class DoubleExtended
    {
        //private const double epsilon = 0.0000001;
        public static double Epsilon { get; set; } = 0.0000001;
        public static bool Equal(this double d1, double d2) => Math.Abs(d1 - d2) < Epsilon;
        public static bool IsZero(this double d1) => Equal(d1, 0.0);
        public static bool More(this double d1, double d2) => d1 - d2 >= Epsilon;
        public static bool MoreOrEqual(this double d1, double d2) => d1 - d2 > -Epsilon;
        public static bool Less(this double d1, double d2) => d2 - d1 >= Epsilon;
        public static bool LessOrEqual(this double d1, double d2) => d2 - d1 > -Epsilon;
    }
}
