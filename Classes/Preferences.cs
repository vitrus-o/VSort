﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace VSort.Classes
{
    public class Preferences
    {
        public bool ImmediateFeedback { get; set; }
        public bool RemoveNonSwappingRounds { get; set; }
        public bool OneByOne { get; set; }
        private static readonly string PreferencesFilePath = "preferences.json";

        public Preferences()
        {
            ImmediateFeedback = true;
            RemoveNonSwappingRounds = true;
            OneByOne = true;
        }
        public void viewPreferences()
        {
            Console.WriteLine($"--Preferences--\n\nImmediate Feedback: {(ImmediateFeedback ? "\u001b[32mTrue\u001b[0m" : "\u001b[31mFalse\u001b[0m")} - Shows if you were correct or not immediately, otherwise it will be shown at the end.\nRemove Non-Swapping Records (Visualization Only): {(RemoveNonSwappingRounds ? "\u001b[32mTrue\u001b[0m" : "\u001b[31mFalse\u001b[0m")} - Set to true if you want to skip non-swapping records from the visualization list. (\u001b[31mNot Recommended\u001b[0m)\nDisplay Rounds one-by-one (Visualization Only): {(OneByOne ? "\u001b[32mTrue\u001b[0m" : "\u001b[31mFalse\u001b[0m")} - Display each round after each key presses. This is good for practicing.\n\n");
            ConsoleKeyInfo key;
            int option = 1;
            bool isSelected = false;
            (int left, int top) = Console.GetCursorPosition();
            string highlight = "\u001b[32m-->";
            while (!isSelected)
            {
                Console.SetCursorPosition(left, top);
                Console.WriteLine($"{(option == 1 ? highlight : "   ")}Edit Preferences\u001b[0m");
                Console.WriteLine($"{(option == 2 ? highlight : "   ")}Return\u001b[0m");
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow: option = option == 2 ? 1 : option + 1; break;
                    case ConsoleKey.UpArrow: option = option == 1 ? 2 : option - 1; break;
                    case ConsoleKey.Enter:
                        isSelected = true;
                        switch (option)
                        {
                            case 1: Console.Clear(); setPreference(); viewPreferences(); break;
                            case 2: return;
                        }
                        break;
                }
            }
        }
        public void setPreference()
        {
            Console.CursorVisible = true;
            Console.Write("--Setting Preference--\nInput \"True\" or \"False\" to set the values\n\nImmediate Feedback: ");
            ImmediateFeedback = Console.ReadLine().ToUpper() == "TRUE" ? true : false;
            Console.Write("Remove Non-Swapping Records: ");
            RemoveNonSwappingRounds = Console.ReadLine().ToUpper() == "TRUE" ? true : false;
            Console.Write("Display Rounds One-by-One: ");
            OneByOne = Console.ReadLine().ToUpper() == "TRUE" ? true : false;
            Console.CursorVisible = false;
            Console.Clear();
            SavePreferences();
        }
        public void SavePreferences()
        {
            string json = JsonSerializer.Serialize(this);
            File.WriteAllText(PreferencesFilePath, json);
        }
        public static Preferences LoadPreferences()
        {
            if (File.Exists(PreferencesFilePath))
            {
                string json = File.ReadAllText(PreferencesFilePath);
                return JsonSerializer.Deserialize<Preferences>(json);
            }
            return new Preferences();
        }
    }
}