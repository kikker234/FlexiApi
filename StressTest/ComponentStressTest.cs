using Auth;
using FlexiApi.Controllers;
using Integration.Utils;
using Microsoft.AspNetCore.Http;
using NBench;

namespace StressTest;

public class ComponentStressTest : TestClass
{
    private Counter counter;
    private HttpClient client;
    
    [PerfSetup]
    public void Setup(BenchmarkContext context)
    {
        counter = context.GetCounter("GetPersonComponentCounter");
        client = GetClient();
        
        Login();
    }

    [PerfBenchmark(Description = "Test to ensure that a minimal throughput test can be rapidly executed.",
        NumberOfIterations = 1, RunMode = RunMode.Throughput, RunTimeMilliseconds = 1000, TestMode = TestMode.Test)]
    [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
    [CounterThroughputAssertion("GetPersonComponentCounter", MustBe.GreaterThan, 50)]
    public void Benchmark()
    {
        HttpResponseMessage msg = GetResponse("/api/person");
        
        counter.Increment();
    }
}