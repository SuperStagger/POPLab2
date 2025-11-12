using System;
using System.Threading;

namespace POPLab2
{
    class Program
    {
        static int globalMin = int.MaxValue;
        static int globalIndex = -1;

        static readonly object locker = new object();
        static readonly object lockerForCount = new object();
        static int finishedThreads = 0;

        static void Main(string[] args)
        {
            Console.Write("Введiть розмiр масиву: ");
            int size = int.Parse(Console.ReadLine() ?? "1000000");

            Console.Write("Введiть кiлькiсть потокiв: ");
            int threadCount = int.Parse(Console.ReadLine() ?? "4");

            int[] arr = ArrayGenerator.Generate(size);
            int partSize = size / threadCount;
            Thread[] threads = new Thread[threadCount];

            for (int i = 0; i < threadCount; i++)
            {
                int start = i * partSize;
                int end = (i == threadCount - 1) ? size : start + partSize;

                MinFinder finder = new MinFinder(arr, new Range(start, end), UpdateGlobalMin, ThreadFinished);
                threads[i] = new Thread(finder.Run);
                threads[i].Start();
            }

            lock (lockerForCount)
            {
                while (finishedThreads < threadCount)
                {
                    Monitor.Wait(lockerForCount);
                }
            }

            Console.WriteLine($"\nМiнiмальний елемент: {globalMin}");
            Console.WriteLine($"Iндекс мiнiмального елемента: {globalIndex}");
        }

        static void UpdateGlobalMin(int value, int index)
        {
            lock (locker)
            {
                if (value < globalMin)
                {
                    globalMin = value;
                    globalIndex = index;
                }
            }
        }

        static void ThreadFinished()
        {
            lock (lockerForCount)
            {
                finishedThreads++;
                Monitor.Pulse(lockerForCount);
            }
        }
    }
}