using BenchmarkDotNet.Attributes;
using Many.Mocks.Benchmarks.TestClasses;

namespace Many.Mocks.Benchmarks
{
    [SimpleJob(warmupCount:1, targetCount:10)]
    [RankColumn, MeanColumn]
    public class Mocks_Getters
    {
        [Benchmark]
        public Bag GetMocksFromConstructors_NoCtors()
        {
            return typeof(ImplIClass1).GetMocksFromConstructors();
        }
        [Benchmark]
        public Bag GetMocksFromConstructors_01()
        {
            return typeof(ImplIClass2).GetMocksFromConstructors();
        }
        [Benchmark]
        public Bag GetMocksFromConstructors_02()
        {
            return typeof(ImplIClass3).GetMocksFromConstructors();
        }
    }
}
