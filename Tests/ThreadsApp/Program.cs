using System;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadsApp
{
    class Program
    {
        static Random rand = new Random();

        static int[,] matrix_1, matrix_2, matrix_3;
        static DateTime time;

        static int N = 100; //размерность матриц

        static void Main(string[] args)
        {
            matrix_1 = new int[N, N];
            matrix_2 = new int[N, N];
            matrix_3 = new int[N, N];

            time = DateTime.Now;

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {

                    matrix_1[i, j] = rand.Next(0, 9);
                    matrix_2[i, j] = rand.Next(0, 9);

                }
            }

            Parallel.For(0, N, (i, state) =>
            {
                Parallel.For(0, N, (j, state2) =>
                {
                   matrix_3[i, j] = matrix_1[i, j] * matrix_2[i, j];
                });
            });

           Console.WriteLine("Посчитано за {0} секунд", (DateTime.Now - time).TotalSeconds);
            Console.ReadKey();

        }
    }
}
