namespace Shape.Lib;

public static class MathHelper
{
    public static bool AreEquivalent(double a, double b)
    {
        return Math.Abs(a - b) <= 0.001;
    }

    public static bool IsEquivalent(this double a, double b)
    {
        return AreEquivalent(a, b);
    }
}
