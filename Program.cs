using System;
using System.Drawing;
using VSort;

namespace vsort
{
    public class Program
    {
        static Preferences p = Preferences.LoadPreferences();
        static string highlight = "\u001b[33m-->";
        public static void Main(string[] args)
        {
            int option = 1;
            ConsoleKeyInfo key;
            bool exit = false;
            Console.CursorVisible = false;
            Console.WriteLine("Instruction: Use up and down keys to navigate and press the Enter key to select. If this is you're first time, make sure to configure the preferences before starting out to reduce confusion of the flow of the program.\n");
            do
            {
                Console.SetCursorPosition(0, 1);
                Console.WriteLine("\n");
                Console.WriteLine($"{(option == 1 ? highlight : "   ")}Visualize Sorting\u001b[0m");                
                Console.WriteLine($"{(option == 2 ? highlight : "   ")}Preferences\u001b[0m");
                Console.WriteLine($"{(option == 3 ? highlight : "   ")}Exit\u001b[0m");
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow: option = (option == 3 ? 1 : option + 1); break;
                    case ConsoleKey.UpArrow: option = (option == 1 ? 3 : option - 1); break;
                    case ConsoleKey.Enter:
                        switch (option)
                        {
                            case 1: Console.Clear(); VisualizeSorting(); Console.Clear(); break;
                            case 2: Console.Clear(); p.viewPreferences(); Console.Clear(); break;
                            case 3: Console.Clear(); exit = true; Console.WriteLine("Made by: Vee Emmanuel L. Añora"); return;
                        }
                        if (!exit)
                        {
                            Console.Clear();
                            Console.WriteLine("Instruction: Use up and down keys to navigate and press the Enter key to select. If this is you're first time, make sure to configure the preferences before starting out to reduce confusion of the flow of the program.\n");
                        }
                        break;
                }
            } while (!exit);
        }
        static void PracticeSorting()
        {
            Console.WriteLine("practice");
            Console.ReadKey();
        }
        static void VisualizeSorting()
        {
            SortList sl;
            SortingType st = SortingType.Selection_Sort;
            ConsoleKeyInfo key;
            int option = 1;
            bool exit = false;
            Console.WriteLine("Select Sorting Algorithm to Visualize\n");
            do
            {
                Console.SetCursorPosition(0, 1);
                Console.WriteLine($"{(option == 1 ? highlight : "   ")}Selection Sort\u001b[0m");
                Console.WriteLine($"{(option == 2 ? highlight : "   ")}Bubble Sort\u001b[0m");
                Console.WriteLine($"{(option == 3 ? highlight : "   ")}Insertion Sort\u001b[0m");
                Console.WriteLine($"{(option == 4 ? highlight : "   ")}Merge Sort\u001b[0m");
                Console.WriteLine($"{(option == 5 ? highlight : "   ")}Quick Sort\u001b[0m");
                Console.WriteLine($"{(option == 6 ? highlight : "   ")}Return\u001b[0m");
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow: option = (option == 6 ? 1 : option + 1); break;
                    case ConsoleKey.UpArrow: option = (option == 1 ? 6 : option - 1); break;
                    case ConsoleKey.Enter:
                        switch (option)
                        {
                            case 1: Console.Clear(); st = SortingType.Selection_Sort; break;
                            case 2: Console.Clear(); st = SortingType.Bubble_Sort; break;
                            case 3: Console.Clear(); st = SortingType.Insertion_Sort; break;
                            case 4: Console.Clear(); st = SortingType.Merge_Sort; break;
                            case 5: Console.Clear(); st = SortingType.Quick_Sort; break;
                            case 6: Console.Clear(); exit = true; return;
                        }
                        if (!exit)
                        {
                            sl = promptVisualize(st);
                            printSortList(sl);
                            Console.Write("\nList has been sorted\n--Press any key to continue--");
                            Console.ReadKey();
                            Console.Clear();
                            Console.WriteLine("Select Sorting Algorithm to Visualize\n");
                        }
                        break;
                }
            } while (!exit);
        }
        static SortList promptVisualize(SortingType sortingType)
        {
            Console.CursorVisible = true;
            int size = 0;
            do
            {
                Console.Write($"--Visualizing {sortingType}--\nNature of Sort: {(p.UseRandomInput ? "Random" : "Custom")}\nNumber of Input: ");
                try
                {
                    size = int.Parse(Console.ReadLine());
                    if (size <= 1)
                    { 
                        Console.WriteLine("Size cannot be less than or equal to 1.");
                        Console.WriteLine("--Press any key to continue--");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Clear();
                        break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("\n\nInput is not a valid integer. Please input once more!");
                    Console.WriteLine("--Press any key to continue--");
                    Console.ReadKey();
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("\n\nInput should not be null. Please input once more!");
                    Console.WriteLine("--Press any key to continue--");
                    Console.ReadKey();
                }
                Console.Clear();
            } while (size <= 1);
            List<int> data = new List<int>(size);
            if (p.UseRandomInput)
            {
                data = GenerateRandom(size);
            }
            else
            {
                bool validInput1 = false;
                do
                {
                    Console.Write("Input data separated by spaces: ");
                    try
                    {
                        string[] inputs = Console.ReadLine().Split(' ');
                        if (inputs.Length != size)
                            throw new FormatException("Number of inputs does not match the indicated size.");
                        foreach (string input in inputs)
                            data.Add(int.Parse(input));
                        validInput1 = true;
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine($"Input is not valid. {e.Message} Please input once more!");
                        Console.WriteLine("\n\n--Press any key to continue--");
                        Console.ReadKey();
                        data.Clear();
                    }
                    catch (ArgumentNullException)
                    {
                        Console.WriteLine("Input should not be null. Please input once more!");
                        Console.WriteLine("\n\n--Press any key to continue--");
                        Console.ReadKey();
                        data.Clear();
                    }
                    Console.Clear();
                } while (!validInput1);
            }
            bool ascending = false, validInput = false;
            do
            {
                Console.Write($"--Visualizing {sortingType}--\nNature of Sort: {(p.UseRandomInput ? "Random" : "Custom")}\nNumber of Input: {size}\nInitial List: ");
                foreach(var num in data)
                    Console.Write(num + " ");
                Console.Write("\nSorting Order (A/D): ");
                try
                {
                    string input = Console.ReadLine().ToUpper();
                    if (input == "A" || input == "D")
                    {
                        ascending = input == "A";
                    }
                    else
                        throw new FormatException();
                    validInput = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input is not valid. Please input once more!");
                    Console.WriteLine("\n\n--Press any key to continue--");
                    Console.ReadKey();
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("Input should not be null. Please input once more!");
                    Console.WriteLine("\n\n--Press any key to continue--");
                    Console.ReadKey();
                }
                Console.Clear();
            } while (!validInput);
            Console.Write($"--Visualizing {sortingType}--\nNature of Sort: {(p.UseRandomInput ? "Random" : "Custom")}\nNumber of Input: {size}\nInitial List: ");
            foreach (var num in data)
                Console.Write(num + " ");
            Console.Write($"\nSorting Order: {(ascending ? "Ascending" : "Descending")}");
            Console.CursorVisible = false;
            return new SortList(data, sortingType, ascending);
        }
        static void printSortList(SortList sortList)
        {
            if (p.ShowNumOfRecordHints)
                Console.WriteLine($"\nNumber of Actual Records: {sortList.Rounds.Count}");
            Console.WriteLine("\n");
            List<int> prev = null;
            foreach (var round in sortList.Rounds)
            {
                if (p.RemoveNonSwappingRounds && prev != null && round.SequenceEqual(prev))
                    continue;
                Console.WriteLine(string.Join(", ", round));
                prev = round;
                if (p.OneByOne)
                    Console.ReadKey();
            }
            Console.WriteLine();
        }
        static List<int> GenerateRandom(int size)
        {
            List<int> numbers = Enumerable.Range(0, size).ToList();
            Random rand = new Random();
            for (int i = numbers.Count - 1; i > 0; i--)
            {
                int j = rand.Next(0, i+1);
                int temp = numbers[j];
                numbers[j] = numbers[i];
                numbers[i] = temp;
            }
            return numbers;
        }
    }
}