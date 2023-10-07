using System.Diagnostics;

namespace cs;

public static class TwoOpt
{
    public static int[] Exhaustive(Point2D[] points, int[] route, TimeSpan timeLimit)
    {
        var sw = Stopwatch.StartNew();
        var best = new int[route.Length];
        var routeCost = Utils.CycleDistanceSquared(points, route);
        var bestCost = double.PositiveInfinity;
        var improved = true;
        var numChecks = 0;
        var lastPrint = TimeSpan.Zero;
        var timeLimitReached = false;

        while (improved && !timeLimitReached)
        {
            improved = false;
            for (var i = 1; i < points.Length - 2; i++)
            {
                if (timeLimitReached) break;
                for (var j = i + 2; j < points.Length + 1; j++)
                {
                    var jj = j % points.Length;
                    if (Math.Abs(jj - i) < 2) continue;
                    var jprev = jj == 0 ? points.Length - 1 : jj - 1;
                    var (a, b, c, d) = (route[i-1], route[i], route[jprev], route[jj]);
                    if (jj == 0) jj = points.Length;
                    var costRemoved =
                          points[a].DistanceSquared(points[b])
                        + points[c].DistanceSquared(points[d]);
                    var costAdded =
                          points[a].DistanceSquared(points[c])
                        + points[b].DistanceSquared(points[d]);
                    var newCost = routeCost - costRemoved + costAdded;
                    if (newCost < bestCost)
                    {
                        Array.Copy(route, best, route.Length);
                        Utils.ReverseRange(best, i, j - 1);
                        bestCost = newCost;
                        improved = true;
                    }
                    numChecks++;
                    if (sw.Elapsed - lastPrint > TimeSpan.FromSeconds(1))
                    {
                        Console.Error.WriteLine($"best: {bestCost}     Checks: {numChecks}");
                        lastPrint = sw.Elapsed;
                        numChecks = 0;
                    }
                    if (sw.Elapsed > timeLimit)
                    {
                        timeLimitReached = true;
                        break;
                    }
                }
            }
            route = best;
            routeCost = bestCost;
        }
        return best;
    }
}
