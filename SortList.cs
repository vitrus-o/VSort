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
                case SortingType.Merge_Sort: MergeSort(array); break;
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
            CaptureRound(array);
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
                i++;
            }
        }
        private void InsertionSort(int[] array)
        {
            int key, j;
            for (int i = 1; i < array.Length; i++)
            {
                key = array[i];
                j = i - 1;
                bool captured = false;
                while (j >= 0 && (ascending ? key < array[j] : key > array[j]))
                {
                    array[j+1]=array[j];
                    j--;
                    captured = true;
                }
                array[j+1] = key;
                if (captured)
                    CaptureRound(array);
            }
        }
        private void MergeSort(int[] array)
        {
            if (array.Length > 1)
            {
                int mid = array.Length / 2;
                int[] left = new int[mid];
                int[] right = new int[array.Length - mid];
                Array.Copy(array, 0, left, 0, mid);
                Array.Copy(array,mid,right,0, array.Length - mid);
                MergeSort(left);
                MergeSort(right);
                Merge(array, left, right);
            }
        }
        private void Merge(int[] array, int[] left, int[] right)
        {
            int i = 0, j = 0, k = 0;
            int leftStart = k, leftEnd = left.Length-1,rightStart = left.Length, rightEnd = array.Length-1;
            while (i < left.Length && j < right.Length)
            {
                if(ascending ? left[i] <= right[j] : left[i] >= right[j])
                    array[k++] = left[i++];
                else
                    array[k++] = right[j++];
                CaptureRound(array);
            }
            while(i < left.Length)
            {
                array[k++] = left[i++];
                CaptureRound(array);
            }
            while (j < right.Length)
            {
                array[k++] = right[j++];
                CaptureRound(array);
            }
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
                QuickSort(array, start, j-1);
                QuickSort(array,j+1,end);
            }
        }
    }
}