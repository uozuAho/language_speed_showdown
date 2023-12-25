using BenchmarkDotNet.Attributes;

namespace cs;

[DisassemblyDiagnoser]
[RyuJitX64Job]
public class TwoOptBenchmark
{
    [Benchmark]
    public void TwoOptBench()
    {
        var points = Utils.GenerateRandomPoints(1000);
        var route = Enumerable.Range(0, points.Length).ToArray();
        var best = TwoOpt.Exhaustive(points, route, TimeSpan.FromSeconds(1));
    }
}
