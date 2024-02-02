using System;
using System.Collections.Generic;

public abstract class Task
{
    public string Title { get; set; }
    public bool IsCompleted { get; set; }

    protected Task(string title)
    {
        Title = title;
        IsCompleted = false;
    }

    public abstract void MarkAsCompleted();

    public virtual string GetDescription()
    {
        return Title;
    }

    public override string ToString()
    {
        return $"{(IsCompleted ? "[X]" : "[ ]")} {GetDescription()}";
    }
}

public class BasicTask : Task
{
    public BasicTask(string title) : base(title)
    {
    }

    public override void MarkAsCompleted()
    {
        IsCompleted = true;
    }
}

public class AdvancedTask : Task
{
    public int Priority { get; set; }

    public AdvancedTask(string title, int priority) : base(title)
    {
        Priority = priority;
    }

    public override void MarkAsCompleted()
    {
        IsCompleted = true;
        Priority = 0;
    }

    public override string GetDescription()
    {
        return $"{base.GetDescription()} [Priority: {Priority}]";
    }
}

public class TaskList
{
    private readonly List<Task> tasks;

    public TaskList()
    {
        tasks = new List<Task>();
    }

    public void AddTask(Task task)
    {
        tasks.Add(task);
    }

    public void DeleteTask(int index)
    {
        if (index >= 0 && index < tasks.Count)
        {
            tasks.RemoveAt(index);
        }
        else
        {
            Console.WriteLine("Invalid task index.");
        }
    }

    public void MarkTaskAsCompleted(int index)
    {
        if (index >= 0 && index < tasks.Count)
        {
            tasks[index].MarkAsCompleted();
        }
        else
        {
            Console.WriteLine("Invalid task index.");
        }
    }

    public void DisplayTasks()
    {
        Console.WriteLine("Task List:");
        for (int i = 0; i < tasks.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {tasks[i]}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        TaskList taskList = new TaskList();

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("ToDo List\n");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. Delete Task");
            Console.WriteLine("3. Mark Task as Completed");
            Console.WriteLine("4. Show Tasks");
            Console.WriteLine("5. Exit\n");

            Console.Write("Select an action: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    AddTask(taskList);
                    break;
                case "2":
                    Console.Clear();
                    DeleteTask(taskList);
                    break;
                case "3":
                    Console.Clear();
                    MarkTaskAsCompleted(taskList);
                    break;
                case "4":
                    Console.Clear();
                    taskList.DisplayTasks();
                    break;
                case "5":
                    exit = true;
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid selection.");
                    break;
            }
        }
    }

    private static void AddTask(TaskList taskList)
    {
        Console.Write("Enter the title of the task: ");
        string title = Console.ReadLine();

        Console.Write("Enter the priority of the task (0 for basic tasks): ");
        if (int.TryParse(Console.ReadLine(), out int priority))
        {
            if (priority == 0)
            {
                taskList.AddTask(new BasicTask(title));
            }
            else
            {
                taskList.AddTask(new AdvancedTask(title, priority));
            }
        }
        else
        {
            Console.WriteLine("Invalid priority input. Task not added.");
        }
        Console.Clear ();
    }

    private static void DeleteTask(TaskList taskList)
    {
        Console.Write("Enter the index of the task to delete: ");
        if (int.TryParse(Console.ReadLine(), out int index))
        {
            taskList.DeleteTask(index - 1);
        }
        else
        {
            Console.WriteLine("Invalid index input.");
        }
        Console.Clear ();
    }

    private static void MarkTaskAsCompleted(TaskList taskList)
    {
        Console.Write("Enter the index of the task to mark as completed: ");
        if (int.TryParse(Console.ReadLine(), out int index))
        {
            taskList.MarkTaskAsCompleted(index - 1);
        }
        else
        {
            Console.WriteLine("Invalid index input.");
        }
        Console.Clear ();
    }
}