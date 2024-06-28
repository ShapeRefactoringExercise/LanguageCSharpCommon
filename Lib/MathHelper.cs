using System.ComponentModel;

namespace Shape.Lib;

public static class MathHelper
{
    public static bool AreEquivalent(double a, double b)
    {
        return Math.Abs(a - b) <= 0.001;
    }

    public static bool IsEquivalentTo(this double a, double b)
    {
        return AreEquivalent(a, b);
    }

    public static bool IsEquivalentTo(this double? a, double? b)
    {
        if (!a.HasValue && !b.HasValue)
        {
            return true;
        }

        if (!a.HasValue || !b.HasValue)
        {
            return false;
        }

        return AreEquivalent(a.Value, b.Value);
    }
}
