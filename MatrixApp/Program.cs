using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace MatrixApp;

using System;
using System.Diagnostics;


class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: MatrixApp <threads_num> <size> [benchmark]");
            return;
        }
        //dotnet build
        //dotent run -- <threads_num> <size> [benchmark]

        int threads = int.Parse(args[0]);
        int size = int.Parse(args[1]);

        if (args.Length >= 3 && args[2].ToLower() == "benchmark")
        {
            RunBenchmark();
        }
        else
        {
            RunSingleTest(threads, size);
        }
    }

    static void RunSingleTest(int threads, int size)
    {
        var A = Matrix.GenerateRandom(size, size);
        var B = Matrix.GenerateRandom(size, size);

        Console.WriteLine($"Multiplying {size}x{size} with {threads} threads...");

        var watch = Stopwatch.StartNew();
        var result = Matrix.MultiplyParallel(A, B, threads);
        watch.Stop();

        Console.WriteLine($"Time: {watch.ElapsedMilliseconds} ms");

        // A.Print("A");
        // B.Print("B");
        // result.Print("Result");
    }

    static void RunBenchmark()
    {
        int[] matrixSizes = { 100, 200, 400, 800 };
        int[] threadCounts = { 1, 2, 4, 8, 16, 32 };
        int attempts = 5;
        string filePath = "benchmark_results.csv";

        using (var writer = new StreamWriter(filePath))
        {
            writer.WriteLine("MatrixSize;Threads;AverageTimeMsParallel;AverageTimeMsThread");

            foreach (int size in matrixSizes)
            {
                var A = Matrix.GenerateRandom(size, size);
                var B = Matrix.GenerateRandom(size, size);

                foreach (int threads in threadCounts)
                {
                    // with parallel 
                    long parallelTime = 0;
                    for (int i = 0; i < attempts; i++)
                    {
                        var watch = Stopwatch.StartNew();
                        var result = Matrix.MultiplyParallel(A, B, threads);
                        watch.Stop();
                        parallelTime += watch.ElapsedMilliseconds;
                    }
                    //average  
                    long avgParallelTime = parallelTime / attempts;

                    // with threads 
                    long threadTime = 0;
                    for (int i = 0; i < attempts; i++)
                    {
                        var watch = Stopwatch.StartNew();
                        var result = Matrix.MultiplyThreaded(A, B, threads);
                        watch.Stop();
                        threadTime += watch.ElapsedMilliseconds;
                    }
                    //average
                    long avgThreadTime = threadTime / attempts;

                    writer.WriteLine($"{size}x{size};{threads};{avgParallelTime};{avgThreadTime}");
                    Console.WriteLine($"{size}x{size}\t{threads} threads\tParallel: {avgParallelTime} ms\tThread: {avgThreadTime} ms");
                }
            }
        }

        Console.WriteLine($"\nSaved to file: {Path.GetFullPath(filePath)}");
    }
}