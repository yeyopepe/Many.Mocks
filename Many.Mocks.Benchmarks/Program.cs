using BenchmarkDotNet.Running;

namespace Many.Mocks.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Mocks_Getters>();
        }
    }
}
