using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POPLab2
{
    internal class ArrayGenerator
    {
        public static int[] Generate(int size)
        {
            Random rand = new Random();
            int[] arr = new int[size];

            for (int i = 0; i < size; i++)
                arr[i] = rand.Next(0, 100000);

            // 🔸 Додаємо один від’ємний елемент у випадкове місце
            int randomIndex = rand.Next(0, size);
            arr[randomIndex] = -rand.Next(1, 500);

            return arr;
        }
    }
}
