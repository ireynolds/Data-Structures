using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class QuickSort
    {
        public static void Run()
        {
            int size = 10;
            Random r = new Random();
            int[] lst = new int[size];
            for (int i = 0; i < size; i++)
                lst[i] = r.Next();

            Sort(lst, 0, lst.Length);

            for (int i = 1; i < lst.Length; i++)
                if (lst[i - 1] > lst[i])
                    throw new Exception();
        }

        // Inclusive Low, exclusive High
        public static void Sort(int[] List, int Low, int High)
        {
            if (Math.Abs(Low - High) > 1)
            {
                int pivotIndex = GetPivotIndex(List, Low, High);
                pivotIndex = Partition(List, Low, High, pivotIndex);
                Sort(List, Low, pivotIndex);
                Sort(List, pivotIndex + 1, High);
            }
        }

        public static int Partition(int[] List, int Low, int High, int PivotIndex)
        {
            int left = Low;
            int right = High;
            int pivotVal = List[PivotIndex];

            Swap(List, PivotIndex, High);
            right--;

            while (right - left > 1)
            {
                int lVal = List[left];
                int hVal = List[right];
                if (lVal > pivotVal && hVal > pivotVal)
                {
                    right--;
                    Swap(List, left, right--);
                }
                else if (lVal > pivotVal && hVal <= pivotVal)
                {
                    Swap(List, left++, right--);
                }
                else if (lVal <= pivotVal && hVal > pivotVal)
                {
                    left++;
                    right--;
                }
                else if (lVal <= pivotVal && hVal <= pivotVal)
                {
                    Swap(List, ++left, right);
                    left++;
                }
                else
                    throw new Exception("Forgot a case ...");
            }

            Swap(List, right, High);
            return right;
        }

        public static void Swap(int[] List, int Index1, int Index2)
        {
            int temp = List[Index1];
            List[Index1] = List[Index2];
            List[Index2] = temp;
        }

        public static int GetPivotIndex(int[] List, int Low, int High)
        {
            int[] nums = new int[3];
            nums[0] = List[Low];
            nums[1] = List[High];
            nums[2] = List[Low + (High - Low) / 2];
            Array.Sort(nums);
            return nums[1];
        }
    }
}
