using BenchmarkDotNet.Running;
using NBench;
using StressTest.benchmark;

namespace StressTest;

public class StressTest
{
    static int Main(String[] args)
    {
        var summary = BenchmarkRunner.Run<ComponentBenchmark>();
        
        Console.WriteLine(summary);
        
        return 0;
    }
}