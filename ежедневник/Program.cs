using System;
using System.Collections.Generic;

class DailyPlanner
{
    static Dictionary<DateTime, List<Note>> dailyNotes = new Dictionary<DateTime, List<Note>>();
    static DateTime currentDate = DateTime.Today;
    static int menuChoice = 1;

    static void Main(string[] args)
    {
        Console.CursorVisible = false;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Любимый ежедневник: ");
            for (int i = 1; i <= 4; i++)
            {
                if (i == menuChoice)
                {
                    Console.WriteLine($"=> {GetMenuText(i)}");
                }
                else
                {
                    Console.WriteLine($"   {GetMenuText(i)}");
                }
            }

            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow)
                {
                    if (menuChoice > 1)
                    {
                        menuChoice--;
                    }
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    if (menuChoice < 4)
                    {
                        menuChoice++;
                    }
                }
                else if (key == ConsoleKey.Enter)
                {
                    ExecuteMenuAction();
                }
            }

            System.Threading.Thread.Sleep(100);
        }
    }

    static void ExecuteMenuAction()
    {
        switch (menuChoice)
        {
            case 1:
                AddNote();
                break;
            case 2:
                ShowNoteList();
                break;
            case 3:
                ShowDate();
                break;
            case 4:
                Environment.Exit(0);
                break;
        }
    }

    static void AddNote()
    {
        Console.Write("Введите дату (гггг-мм-дд): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime selectedDate))
        {
            Console.Write("Введите название заметки: ");
            string title = Console.ReadLine();
            Console.Write("Введите описание: ");
            string description = Console.ReadLine();

            if (!dailyNotes.ContainsKey(selectedDate))
            {
                dailyNotes[selectedDate] = new List<Note>();
            }

            dailyNotes[selectedDate].Add(new Note
            {
                Title = title,
                Description = description
            });
        }
        else
        {
            Console.WriteLine("Неверный формат даты.");
        }
    }

    static void ShowNoteList()
    {
        if (dailyNotes.ContainsKey(currentDate))
        {
            Console.Clear();
            Console.WriteLine($"Заметки на {currentDate.ToShortDateString()}:");
            List<Note> notes = dailyNotes[currentDate];
            for (int i = 0; i < notes.Count; i++)
            {
                Console.WriteLine($"Заметка {i + 1}: {notes[i].Title}");
            }

            Console.WriteLine("Введите номер заметки для подробной информации (или '0' для возврата):");

            if (int.TryParse(Console.ReadLine(), out int selectedNoteIndex))
            {
                if (selectedNoteIndex >= 1 && selectedNoteIndex <= notes.Count)
                {
                    ShowNoteDetails(currentDate, selectedNoteIndex - 1);
                }
            }
        }
        else
        {
            Console.WriteLine("Заметок на эту дату нет.");
        }
    }

    static void ShowNoteDetails(DateTime date, int noteIndex)
    {
        if (dailyNotes.ContainsKey(date) && noteIndex >= 0 && noteIndex < dailyNotes[date].Count)
        {
            Console.Clear();
            var selectedNote = dailyNotes[date][noteIndex];
            Console.WriteLine($"Детали заметки на {date.ToShortDateString()} (Заметка {noteIndex + 1}):");
            Console.WriteLine("Название: " + selectedNote.Title);
            Console.WriteLine("Описание: " + selectedNote.Description);
            Console.WriteLine("Нажмите Enter, чтобы вернуться.");
            Console.ReadLine();
        }
    }

    static void ShowDate()
    {
        Console.Clear();
        Console.WriteLine($"Текущая дата: {currentDate.ToShortDateString()}");
        Console.WriteLine("Нажмите Enter, чтобы вернуться.");
        Console.ReadLine();
    }

    static string GetMenuText(int option)
    {
        switch (option)
        {
            case 1:
                return "Добавить заметку";
            case 2:
                return "Просмотреть заметки";
            case 3:
                return "Просмотреть дату";
            case 4:
                return "Выйти";
            default:
                return "";
        }
    }

    class Note
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
