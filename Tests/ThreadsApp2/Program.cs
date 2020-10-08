using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Text.Unicode;
using System.Threading;

namespace ThreadsApp2
{
    class Program
    {
        static ConcurrentQueue<string[]> fields = new ConcurrentQueue<string[]>();
        static void Main(string[] args)
        {
            Thread ThreadParse = new Thread(new ThreadStart(CsvFileParser));
            Thread ThreadWrite = new Thread(new ThreadStart(CsvToTxt));

            ThreadParse.Start();
            while (ThreadParse.IsAlive) { Thread.Sleep(100); }
            ThreadWrite.Start(); 

            Console.WriteLine("Основной поток работу закончил\n");
            Console.ReadKey();
        }

        static void CsvFileParser()
        {
            Console.WriteLine("Поток 1 стартовал\n");
            var utf8 = Encoding.UTF8;
            string path = @"Test.csv";
            using (TextFieldParser parser = new TextFieldParser(path, utf8))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";", ",");
                while (!parser.EndOfData)
                {
                    fields.Enqueue(parser.ReadFields());
                }
            }
        }

        static void CsvToTxt()
        {
            Console.WriteLine("Поток 2 стартовал\n");
            string path = @"Test.txt";
            using (StreamWriter SW = new StreamWriter(path))
            {
                Console.OutputEncoding = Encoding.UTF8;
                foreach (string[] lines in fields)
                {
                    foreach (string field in lines)
                    {
                        SW.Write($"{field}\t ");
                    }
                    SW.WriteLine();
                }
                Console.WriteLine("Второй поток работу закончил, проверьте файл Test.txt!\n");
            }
        }
    }
}
