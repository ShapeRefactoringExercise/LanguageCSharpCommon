using System.ComponentModel;

namespace Shape.Lib;

public static class MathHelper
{
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

        return Math.Abs(a.Value - b.Value) <= 0.001;
    }
}
