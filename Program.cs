using System;
using System.Drawing;
using System.Text;
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
                Console.WriteLine($"{(option == 2 ? highlight : "   ")}Practice Sorting\u001b[0m");                
                Console.WriteLine($"{(option == 3 ? highlight : "   ")}Preferences\u001b[0m");
                Console.WriteLine($"{(option == 4 ? highlight : "   ")}Exit\u001b[0m");
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow: option = (option == 4 ? 1 : option + 1); break;
                    case ConsoleKey.UpArrow: option = (option == 1 ? 4 : option - 1); break;
                    case ConsoleKey.Enter:
                        switch (option)
                        {
                            case 1: Console.Clear(); select("Visualiz"); Console.Clear(); break;
                            case 2: Console.Clear(); select("Practic"); Console.Clear(); break;
                            case 3: Console.Clear(); p.viewPreferences(); Console.Clear(); break;
                            case 4: Console.Clear(); exit = true; Console.WriteLine("Made by: Vee Emmanuel L. Añora"); return;
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
        static void select(string action)
        {
            SortList sl;
            SortingType st = SortingType.Selection_Sort;
            ConsoleKeyInfo key;
            int option = 1;
            bool exit = false;
            Console.WriteLine($"Select Sorting Algorithm to {action}e\n");
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
                            sl = prompt(action,st);
                            if (action == "Practic")
                                practiceSortList(sl);
                            else
                                printSortList(sl);
                            Console.WriteLine("--Press any key to continue--");
                            Console.ReadKey();
                            Console.Clear();
                            Console.WriteLine($"Select Sorting Algorithm to {action}e\n");
                        }
                        break;
                }
            } while (!exit);
        }
        static void practiceSortList(SortList sortList)
        {
            int score = 0;
            Console.WriteLine("\n\nEnter the expected list for each round of sorting: ");
            Console.CursorVisible = true;
            for (int i = 0; i < sortList.Rounds.Count; i++)
            {
                string[] inputs = Console.ReadLine().Split(' ');
                List<int> expectedRound = inputs.Select(int.Parse).ToList();
                if (p.ImmediateFeedback)
                {
                    if (expectedRound.SequenceEqual(sortList.Rounds[i]))
                    {
                        Console.Write(string.Join(" ", sortList.Rounds[i]));
                        Console.WriteLine(" - Correct!");
                        score++;
                    }
                    else
                        Console.WriteLine("Incorrect!");
                }
                if (expectedRound.SequenceEqual(sortList.Rounds[i]))
                    score++;
            }
            Console.WriteLine($"\n\nPractice ended.\nTotal Score: {score}/{sortList.Rounds.Count} ({(double)score / sortList.Rounds.Count * 100.0:F0}%)\n\n");
            Console.CursorVisible = false;
        }
        static SortList prompt(string action,SortingType sortingType)
        {
            Console.CursorVisible = true;
            int size = 0;
            bool nature = false,selected = false;
            do
            {
                Console.Write($"--{action}ing {sortingType}--\nNature of Sort (Random - \"R\"/ Manual - \"M\"): ");
                try
                {
                    string input = Console.ReadLine().ToUpper();
                    if (input == "R" || input == "M")
                    {
                        nature = input == "R";
                    }
                    else
                        throw new FormatException();
                    selected = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("\n\nInput is not a valid boolean value. Please input once more!");
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("\n\nInput should not be null. Please input once more!");
                }
                finally
                {
                    if (selected == false)
                    { //checkpoint for exceptions if not exception pass through else finally block works
                        Console.WriteLine("--Press any key to continue--");
                        Console.ReadKey();
                    }
                }
                Console.Clear();
            } while (!selected);
            selected = false;
            do
            {
                Console.Write($"--{action}ing {sortingType}--\nNature of Sort: {(nature ? "Random" : "Custom")}\nNumber of Input: ");
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
                        selected = true;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("\n\nInput is not a valid integer. Please input once more!");
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("\n\nInput should not be null. Please input once more!");
                }
                finally
                {
                    if (selected == false)
                    {
                        Console.WriteLine("--Press any key to continue--");
                        Console.ReadKey();
                    }
                }
                Console.Clear();
            } while (!selected);
            selected = false;
            List<int> data = new List<int>(size);
            if (nature)
            {
                data = GenerateRandom(size);
            }
            else
            {
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
                        selected = true;
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine($"Input is not valid. {e.Message} Please input once more!");
                    }
                    catch (ArgumentNullException)
                    {
                        Console.WriteLine("Input should not be null. Please input once more!");
                    }
                    finally
                    {
                        if (selected == false)
                        {
                            Console.WriteLine("\n\n--Press any key to continue--");
                            Console.ReadKey();
                            data.Clear();
                        }
                    }
                    Console.Clear();
                } while (!selected);
            }
            bool ascending = false;
            selected = false;
            do
            {
                Console.Write($"--{action}ing {sortingType}--\nNature of Sort: {(nature ? "Random" : "Custom")}\nNumber of Input: {size}\nInitial List: ");
                foreach(var num in data)
                    Console.Write($"{num}, ");
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
                    selected = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input is not valid. Please input once more!");
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("Input should not be null. Please input once more!");
                }
                finally
                {
                    if (selected == false)
                    {
                        Console.WriteLine("\n\n--Press any key to continue--");
                        Console.ReadKey();
                    }
                }
                Console.Clear();
            } while (!selected);
            Console.Write($"--{action}ing {sortingType}--\nNature of Sort: {(nature ? "Random" : "Custom")}\nNumber of Input: {size}\nInitial List: ");
            foreach(var num in data)
                Console.Write($"{num}, ");
            Console.Write($"\nSorting Order: {(ascending ? "Ascending" : "Descending")}");
            Console.CursorVisible = false;
            return new SortList(data, sortingType, ascending);
        }
        static void printSortList(SortList sortList)
        {
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
            Console.WriteLine("\nList has been sorted");
        }
        static List<int> GenerateRandom(int size, int minValue = 1, int maxValue = 100)
        {
            List<int> numbers = new List<int>();
            Random rand = new Random();
            for (int i = 0; i < size; i++)
            {
                numbers.Add(rand.Next(minValue,maxValue + 1));
            }
            return numbers;
        }
    }
}