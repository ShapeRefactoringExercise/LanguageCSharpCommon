using System.ComponentModel;

namespace Shape.Lib;

public static class MathHelper
{
    public static bool IsEquivalentTo(this double? a, double? b)
    {
        return Math.Abs(a.GetValueOrDefault() - b.GetValueOrDefault()) <= 0.001;
    }
}
