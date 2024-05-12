﻿using System;
using System.Collections.Generic;

class TaskItem
{
    public string Title;
    public string Description ;
}

class Program
{
    static List<TaskItem> tasks = new List<TaskItem>();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\nTask List Menu:");
            Console.WriteLine("1. Create a Task");
            Console.WriteLine("2. Read Tasks");
            Console.WriteLine("3. Update a Task");
            Console.WriteLine("4. Delete a Task");
            Console.WriteLine("5. Exit");

            int choice;
            Console.Write("Enter your choice: ");
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Please enter a valid number between 1 to 5 .");
                continue;
            }

            switch (choice)
            {
                case 1:
                    CreateTask();
                    break;
                case 2:
                    ReadTasks();
                    break;
                case 3:
                    UpdateTask();
                    break;
                case 4:
                    DeleteTask();
                    break;
                case 5:
                    Console.WriteLine("Getting Out..");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please enter a valid choice.");
                    break;
            }
        }
    }

    static void CreateTask()
    {
        Console.Write("Enter task title: ");
        string title = Console.ReadLine();
        Console.Write("Enter task description: ");
        string description = Console.ReadLine();

        tasks.Add(new TaskItem { Title = title, Description = description });
        Console.WriteLine("Task has been created.");
    }

    static void ReadTasks()
    {
        Console.WriteLine("\nTask List:");
        if (tasks.Count == 0)
        {
            Console.WriteLine("There isn't any task.");
        }
        else
        {
            foreach (var task in tasks)
            {
                Console.WriteLine($"Title: {task.Title}");
                Console.WriteLine($"Description: {task.Description}");
                Console.WriteLine();
            }
        }
    }

    static void UpdateTask()
    {
        Console.Write("Enter the title of the task to update: ");
        string title = Console.ReadLine();

        TaskItem task = tasks.Find(t => t.Title.Equals(title));
        if (task != null)
        {
            Console.Write("Enter new title of task: ");
            string newTitle = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newTitle))
            {
                task.Title = newTitle;
            }

            Console.Write("Enter new description of the task: ");
            string newDescription = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newDescription))
            {
                task.Description = newDescription;
            }

            Console.WriteLine("Task has been updated.");
        }
        else
        {
            Console.WriteLine("Task not found.");
        }
    }

    static void DeleteTask()
    {
        Console.Write("Enter the title of the task to delete: ");
        string title = Console.ReadLine();

        TaskItem task = tasks.Find(t => t.Title.Equals(title));
        if (task != null)
        {
            tasks.Remove(task);
            Console.WriteLine("Task has been deleted from the list.");
        }
        else
        {
            Console.WriteLine("Task not found.");
        }
    }
}
