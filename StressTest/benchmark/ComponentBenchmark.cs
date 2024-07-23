using BenchmarkDotNet.Attributes;
using Integration.Utils;

namespace StressTest.benchmark;

[SimpleJob(launchCount: 1, warmupCount: 10, iterationCount: 25)]
[MinColumn, MaxColumn, MeanColumn, MedianColumn]
public class ComponentBenchmark : TestClass
{
    
    public ComponentBenchmark()
    {
        Login();
    }
    
    [Benchmark]
    public void GetComponents()
    {
        var response = GetResponse("/api/person");
        response.EnsureSuccessStatusCode();
        
        Thread.Sleep(50);
    }
}