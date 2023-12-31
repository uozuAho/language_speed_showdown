﻿using BenchmarkDotNet.Running;
using cs;

var runBenchmark = args[0] == "benchmark";

if (runBenchmark)
{
    BenchmarkRunner.Run<TwoOptBenchmark>();
    return;
}
else
{
    var dataPath = args[0];
    var points = FileReader.Read(dataPath);
    var route = Enumerable.Range(0, points.Length).ToArray();
    TwoOpt.Exhaustive(points, route, TimeSpan.FromSeconds(4));
}

internal static class FileReader
{
    public static Point2D[] Read(string path)
    {
        var lineNum = 0;
        var points = Array.Empty<Point2D>();

        foreach (var line in File.ReadLines(path))
        {
            if (++lineNum == 1)
            {
                var numPoints = int.Parse(line);
                points = new Point2D[numPoints];
                continue;
            }

            var asdf = line.Split(' ');
            var x = int.Parse(asdf[0]);
            var y = int.Parse(asdf[1]);
            points[lineNum - 2] = new Point2D(x, y);
        }

        return points;
    }
}

