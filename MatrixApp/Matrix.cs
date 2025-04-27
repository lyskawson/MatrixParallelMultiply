namespace MatrixApp;

public class Matrix
{
    public int Rows { get; }
    public int Cols { get; }
    public double[,] Data { get; } // 2D array to store matrix data

    private static readonly Random Rand = new Random();

    public Matrix(int rows, int cols)
    {
        Rows = rows;
        Cols = cols;
        Data = new double[rows, cols];
    }

    public static Matrix GenerateRandom(int rows, int cols)
    {
        var matrix = new Matrix(rows, cols);
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                matrix.Data[i, j] = Rand.NextDouble() * 10;
        return matrix;
    }

    public static Matrix MultiplyParallel(Matrix A, Matrix B, int maxThreads)
    {
        if (A.Cols != B.Rows)
            throw new InvalidOperationException("Matrix dimensions do not match.");

        Matrix result = new Matrix(A.Rows, B.Cols);
        ParallelOptions options = new ParallelOptions { MaxDegreeOfParallelism = maxThreads };

        Parallel.For(0, A.Rows, options, i =>
        {
            for (int j = 0; j < B.Cols; j++)
            {
                double sum = 0;
                for (int k = 0; k < A.Cols; k++)
                {
                    sum += A.Data[i, k] * B.Data[k, j];
                }
                result.Data[i, j] = sum;
            }
        });

        return result;
    }
    
    public static Matrix MultiplyThreaded(Matrix A, Matrix B, int threadCount)
    {
        if (A.Cols != B.Rows)
            throw new InvalidOperationException("Matrix dimensions do not match.");

        Matrix result = new Matrix(A.Rows, B.Cols);
        Thread[] threads = new Thread[threadCount];
        int rowsPerThread = A.Rows / threadCount; // rows for each thread 
        int remainder = A.Rows % threadCount; // remaining rows for last thread

        for (int t = 0; t < threadCount; t++)
        {
            int start = t * rowsPerThread; // starting row for each thread
            int end;
            if (t == threadCount - 1) 
                end = start + rowsPerThread + remainder; // last thread with remainder
            else
                end = start + rowsPerThread; //end row for each thread
            

            threads[t] = new Thread(() =>
            {
                for (int i = start; i < end; i++)
                {
                    for (int j = 0; j < B.Cols; j++)
                    {
                        double sum = 0;
                        for (int k = 0; k < A.Cols; k++)
                        {
                            sum += A.Data[i, k] * B.Data[k, j];
                        }
                        result.Data[i, j] = sum;
                    }
                }
            });

            threads[t].Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }

        return result;
    }
    
    

    public void Print(string name)
    {
        Console.WriteLine($"\nMatrix {name}:");
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
                Console.Write($"{Data[i, j]:0.00} "); // format to 2 decimal places
            Console.WriteLine();
        }
    }
}