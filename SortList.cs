using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VSort
{
    public class SortList
    {
        private List<int> data;
        private List<List<int>> rounds;
        private bool ascending;
        private SortingType sortingType;
        public SortList(List<int> data, SortingType sortingType, bool ascending)
        {
            this.data = data;
            this.ascending = ascending;
            this.sortingType = sortingType;
            this.rounds = new List<List<int>>();
            this.GenerateRounds();
        }
        public SortList(List<int> data, SortingType sortingType)
        {
            this.data = data;
            this.ascending = true;
            this.sortingType = sortingType;
            this.rounds = new List<List<int>>();
            this.GenerateRounds();
        }
        public List<List<int>> Rounds
        {
            get => new List<List<int>>(this.rounds);
        }
        public void GenerateRounds()
        {
            int[] array = this.data.ToArray();
            switch (this.sortingType)
            {
                case SortingType.Selection_Sort: SelectionSort(array); break;
                case SortingType.Bubble_Sort: BubbleSort(array); break;
                case SortingType.Insertion_Sort: InsertionSort(array); break;
                case SortingType.Merge_Sort: MergeSort(array,0,array.Length-1); break;
                case SortingType.Quick_Sort: QuickSort(array); break;
            }
        }
        private void CaptureRound(int[] array)
        {
            this.rounds.Add(array.ToList());
        }
        private void Swap(int[] array, int i, int idxSmall)
        {
            int temp = array[i];
            array[i] = array[idxSmall];
            array[idxSmall] = temp;
        }
        //Sir Regis implementation of Sorting Algorithms
        private void SelectionSort(int[] array)
        {
            int idxSmall;
            for (int i = 0; i < array.Length - 1; i++)
            {
                idxSmall = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (this.ascending ? array[j] < array[idxSmall] : array[j] > array[idxSmall])
                        idxSmall = j;
                }
                Swap(array,i,idxSmall);
                CaptureRound(array);
            }
        }
        private void BubbleSort(int[] array)
        {
            int i = 0;
            bool swp = true;
            while (swp)
            {
                swp = false;
                for (int j = array.Length - 1; j > i; j--)
                {
                    if (ascending ? array[j] < array[j - 1] : array[j] > array[j-1])
                    {
                        Swap(array, j, j - 1);
                        swp = true;
                    }
                }
                CaptureRound(array);
                i++;
            }
        }
        private void InsertionSort(int[] array)
        {
            int key, j;
            for (int i = 0; i < array.Length; i++)
            {
                key = array[i];
                j = i - 1;
                while (j >= 0 && (ascending ? key < array[j] : key > array[j]))
                {
                    array[j+1]=array[j];
                    j--;
                }
                array[j+1] = key;
                CaptureRound(array);
            }
        }
        private void MergeSort(int[] array,int left,int right)
        {
            if (left < right)
            {
                int mid =  (left + right)/ 2;
                MergeSort(array,left,mid);
                MergeSort(array, mid + 1, right);
                Merge(array, left, mid, right);
                CaptureRound(array);
            }
        }
        private void Merge(int[] array, int left, int mid, int right)
        {
            int n1 = mid - left + 1;
            int n2 = right - mid;
            int[]lArray = new int[n1];
            int[]rArray = new int[n2];
            Array.Copy(array, left, lArray, 0, n1);
            Array.Copy(array,mid+1, rArray, 0, n2);
            int i = 0, j = 0, k = left;
            while (i < n1 && j < n2)
            {
                if (ascending ? lArray[i] <= rArray[j] : lArray[i] >= rArray[j])
                    array[k++] = lArray[i++];
                else
                    array[k++] = rArray[j++];;
            }
            while (i < n1)
                array[k++] = lArray[i++];
            while (j < n2)
                array[k++] = rArray[j++];
        }
        private void QuickSort(int[] array)
        {
            QuickSort(array, 0, array.Length - 1);
        }
        private void QuickSort(int[] array, int start, int end)
        {
            int i, j, pivot;
            if (start < end)
            {
                pivot = array[start];
                i= start+1;
                j = end;
                while (true)
                {
                    if (ascending)
                    {
                        while (i <= end && array[i] < pivot)
                            i++;
                        while (j >= start && array[j] > pivot)
                            j--;
                    }
                    else 
                    {
                        while (i <= end && array[i] > pivot)
                            i++;
                        while (j >= start && array[j] < pivot)
                            j--;
                    }
                    if (i >= j)
                        break;
                    Swap(array, i, j);
                    i++;
                    j--;
                }
                Swap(array, start, j);
                CaptureRound(array);
                QuickSort(array, start, j-1);
                QuickSort(array,j+1,end);
            }
        }
    }
}