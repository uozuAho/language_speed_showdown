namespace cs;

public static class Utils
{
    public static double CycleDistance(Point2D[] points, int[] route)
    {
        var distance = 0.0;
        for (var i = 0; i < route.Length - 1; i++)
        {
            var a = route[i];
            var b = route[i + 1];
            distance += Math.Sqrt(points[a].DistanceSquared(points[b]));
        }
        distance += Math.Sqrt(points[route[^1]].DistanceSquared(points[route[0]]));
        return distance;
    }

    public static double CycleDistanceSquared(Point2D[] points, int[] route)
    {
        var distance = 0.0;
        for (var i = 0; i < route.Length - 1; i++)
        {
            var a = route[i];
            var b = route[i + 1];
            distance += points[a].DistanceSquared(points[b]);
        }
        distance += points[route[^1]].DistanceSquared(points[route[0]]);
        return distance;
    }

    public static int[] ReverseRange(int[] array, int start, int end)
    {
        while (start < end)
        {
            (array[start], array[end]) = (array[end], array[start]);
            start++;
            end--;
        }

        return array;
    }

    public static Point2D[] GenerateRandomPoints(int numPoints)
    {
        var points = new Point2D[numPoints];
        var rand = new Random();
        for (var i = 0; i < numPoints; i++)
        {
            points[i] = new Point2D(rand.NextDouble() * 100, rand.NextDouble() * 100);
        }
        return points;
    }
}
