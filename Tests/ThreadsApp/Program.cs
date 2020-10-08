using System;
using System.Threading;

namespace ThreadsApp
{
    class Program
    {
        static int[] MyArray;
        static ulong[] result;
        static ulong finally_result1 = 1;
        static ulong finally_result2 = 1;

        static void Thread1()
        {
            ulong temp = 1;
            for (int i = 0; i < MyArray.Length; i++)
            {
                temp *= Convert.ToUInt64(MyArray[i]);
            }
            result[0] = Convert.ToUInt64((temp == 0) ? 1 : temp);
            finally_result1 *= result[0]; 
            Console.WriteLine($"Первый поток работу закончил! Результат вычисления - {finally_result1}");
        }
        static void Thread2()
        {
            ulong temp = 0;
            for (int i = 0; i < MyArray.Length; i++)
            {
                temp += Convert.ToUInt64(MyArray[i]);
            }
            result[1] = Convert.ToUInt64((temp == 0) ? 1 : temp);
            finally_result2 = result[1];
            Console.WriteLine($"Второй поток работу закончил! Результат сложения - {finally_result2}");
        }
        static void Main(string[] args)
        {
            result = new ulong[2];

            var time = DateTime.Now;
            Console.WriteLine("Введите число, факториал и сумму которого нужно посчитать");
            int num = Convert.ToInt32(Console.ReadLine());
            MyArray = new int[num];
            //Заполняем массив, длина которого = числу с клавиатуры
            for (int i = 1; i <= num; i++) { MyArray[i - 1] = i; }
            //параметры потоков
            Thread thrd_1 = new Thread(new ThreadStart(Thread1));
            Thread thrd_2 = new Thread(new ThreadStart(Thread2));
            thrd_1.Start();
            thrd_2.Start();
            //Ждём вычисления                         
            while (thrd_1.IsAlive || thrd_2.IsAlive) { Thread.Sleep(1000); }
            Console.WriteLine("Основной поток работу закончил!");
            Console.WriteLine($"Время выполнения {DateTime.Now - time}");
            
            Console.ReadLine();
        }
               
    }
}
