namespace DalTest;
using DalApi;
using DO;
using DalList;
using Dal;
using System.Diagnostics;

internal class Program
{
    private static IDependency? s_dalDependency = new DependencyImplementation(); //stage 1
    private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
    private static ITask? s_dalTask = new TaskImplementation(); //stage 1

    private static void MainMenu()///לקרוא לפונקציה מאיפשהוא!!!
    {
        do
        {
            Console.Write("Enter your choice:\n 1 for Engineer menu,\n 2 for Task menu,\n 3 for Depnendency menu,\n0 to exit ");
            int? choice = Console.Read();
            switch (choice)
            {
                case 0:
                    return; 
                case 1:
                    EngineerMenu();
                    break;
                case 2:
                    TaskMenu();
                    break;
                case 3:
                    DependencyMenu();
                    break;

            }
        } while (true);
    }
     private static void EngineerMenu()
    {
        do
        {
            Console.Write("Enter your choice:\n 1 to create an engineer,\n 2 to read an engineer,\n" +
                           " 3 to read all engineers,\n  4 to update an engineer, \n  5 to delete an engineer, \n0 to exit");
            int? choice = Console.Read();
            try
            {
                switch (choice)
                {
                    case 0:
                        return;
                    case 1:
                        CreateEngineer();
                        break;
                    case 2:
                        ReadEngineer();
                        break;
                    case 3:
                        ReadAllEngineers();
                        break;
                    case 4:
                        UpdateEngineer();
                        break;
                    case 5:
                        DeleteEngineer();
                        break;
                }
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.Message);
            }
        }while (true);
    }
    private static void CreateEngineer()
    {

    }
    private static void ReadEngineer()
    {

    }
    private static void ReadAllEngineers()
    {

    }
    private static void UpdateEngineer()
    {

    }
    private static void DeleteEngineer()
    {

    }
    private static void TaskMenu()
    {
        do
        {
            Console.Write("Enter your choice:\n 1 to create an task,\n 2 to read an task,\n" +
                           " 3 to read all tasks,\n  4 to update an task, \n  5 to delete an task, \n0 to exit");
            int? choice = Console.Read();
            try
            {
                switch (choice)
                {
                    case 0:
                        return;
                    case 1:
                        CreateTask();
                        break;
                    case 2:
                        ReadTask();
                        break;
                    case 3:
                        ReadAllTasks();
                        break;
                    case 4:
                        UpdateTask();
                        break;
                    case 5:
                        DeleteTask();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        } while (true);
    }
    private static void CreateTask()
    {

    }
    private static void ReadTask()
    {

    }
    private static void ReadAllTasks()
    {

    }
    private static void UpdateTask()
    {

    }
    private static void DeleteTask()
    {

    }
    private static void DependencyMenu()
    {
        do
        {
            Console.Write("Enter your choice:\n 1 to create an dependency,\n 2 to read an dependency,\n" +
                           " 3 to read all dependencys,\n  4 to update an dependency, \n  5 to delete an dependency, \n0 to exit");
            int? choice = Console.Read();
            try
            {
                switch (choice)
                {
                    case 0:
                        return;
                    case 1:
                        CreateDependency();
                        break;
                    case 2:
                        ReadDependency();
                        break;
                    case 3:
                        ReadAllDependencys();
                        break;
                    case 4:
                        UpdateDependency();
                        break;
                    case 5:
                        DeleteDependency();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        } while (true);
    }
    private static void CreateDependency()
    {

    }
    private static void ReadDependency()
    {

    }
    private static void ReadAllDependencys()
    {

    }
    private static void UpdateDependency()
    {

    }
    private static void DeleteDependency()
    {

    }
    private static void Main(string[] args)
    {
        try
        {
            Initialization.Do(s_dalDependency, s_dalEngineer, s_dalTask);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}