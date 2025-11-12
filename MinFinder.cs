using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POPLab2
{
    internal class MinFinder
    {
        private readonly int[] arr;
        private readonly Range range;
        private readonly Action<int, int> reportResult;

        public MinFinder(int[] arr, Range range, Action<int, int> reportResult)
        {
            this.arr = arr;
            this.range = range;
            this.reportResult = reportResult;
        }

        public void Run()
        {
            int localMin = arr[range.Start];
            int localIndex = range.Start;

            for (int i = range.Start + 1; i < range.End; i++)
            {
                if (arr[i] < localMin)
                {
                    localMin = arr[i];
                    localIndex = i;
                }
            }

            // 🔹 Передаємо знайдений мінімум у головну програму
            reportResult(localMin, localIndex);
        }
    }
}
