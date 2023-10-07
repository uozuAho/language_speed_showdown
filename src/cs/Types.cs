namespace cs;

public readonly record struct Point2D(double X, double Y)
{
    public double DistanceSquared(Point2D other)
    {
        var dx = X - other.X;
        var dy = Y - other.Y;
        return dx * dx + dy * dy;
    }

    public double Distance(Point2D other)
    {
        return Math.Sqrt(DistanceSquared(other));
    }
}