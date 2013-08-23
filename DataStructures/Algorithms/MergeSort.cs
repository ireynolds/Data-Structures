using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class MergeSort
    {
        public static void Run()
        {
            int size = 10000;

            Random r = new Random();
            int[] lst = new int[size];
            for (int i = 0; i < size; i++)
                lst[i] = r.Next();
            Sort(lst, 0, lst.Length);

            for (int i = 1; i < lst.Length; i++)
                if (lst[i - 1] > lst[i])
                    throw new Exception();
        }

        // Inclusive on Low, exclusive on High.
        static void Sort(int[] List, int Low, int High)
        {
            if (High - Low > 1)
            {
                int Middle = (High + Low) / 2;
                Sort(List, Low, Middle);
                Sort(List, Middle, High);

                Merge(List, Low, Middle, High);
            }
        }

        static void Merge(int[] List, int Low, int Middle, int High)
        {
            int[] arr = new int[High - Low];
            int i = Low;
            int j = Middle;
            int next = 0;
            while (i < Middle && j < High)
            {
                if (List[i] <= List[j])
                    arr[next++] = List[i++];
                else
                    arr[next++] = List[j++];
            }

            while (i < Middle)
                arr[next++] = List[i++];
            while (j < High)
                arr[next++] = List[j++];

            i = Low;
            j = 0;
            while (j < arr.Length)
                List[i++] = arr[j++];
        }
    }
}
