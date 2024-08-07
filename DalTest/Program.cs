﻿namespace DalTest;
using DalApi;
using DO;
using Dal;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Transactions;
using System.Xml.Linq;
/// <summary>
/// main program
/// </summary>
internal class Program
{
//   static readonly IDal s_dal = new DalList(); //stage 2
// static readonly IDal s_dal = new DalXml(); //stage 3

    static readonly IDal s_dal = Factory.Get; //stage 4

    private static void MainMenu()
    {
        do
        {
            Console.Write("Enter your choice:\n 1 for Engineer menu,\n 2 for Task menu,\n" +
                           " 3 for Depnendency menu,\n 0 to exit\n ");
            int choice;
            int.TryParse(Console.ReadLine(), out choice);
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
                           " 3 to read all engineers,\n 4 to update an engineer, \n 5 to delete an engineer, \n 0 to exit\n");
            int choice;
            int.TryParse(Console.ReadLine(), out choice);
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
            catch (DalAlreadyExistsException ex) { Console.WriteLine(ex.Message); }
            catch (DalDeletionImpossible ex) { Console.WriteLine(ex.Message); }
            catch (DalDoesNotExistException ex) { Console.WriteLine(ex.Message); }
            catch (Exception ex){ Console.WriteLine(ex.Message); }
        }while (true);
    }
    private static void CreateEngineer()
    {
        Console.Write("Enter Engineer's ID, Name, E-mail, Level(1-5) and pay per hour.\n");
        int Id;
        int.TryParse(Console.ReadLine(), out Id);
        string Name = Console.ReadLine()??"";
        string Email = Console.ReadLine()?? "";
        int Level;
        int.TryParse(Console.ReadLine(), out Level);
        double Cost;
        double.TryParse(Console.ReadLine(), out Cost);
        Engineer newEngineer = new Engineer(Id,Name,Email,(EngineerExperience)(Level-1), Cost);
        try
        {
            Console.WriteLine(s_dal!.Engineer.Create(newEngineer));
        }
        catch (DalAlreadyExistsException ex) { throw ex; }
        catch (DalDeletionImpossible ex) { throw ex; }
        catch (DalDoesNotExistException ex) { throw ex; }
        catch (Exception) { throw; }
    }
    private static void ReadEngineer()
    {
        Console.Write("Enter Engineer ID.");
        int Id; 
        int.TryParse(Console.ReadLine(), out Id);
        Engineer? foundEngineer = s_dal!.Engineer.Read(Id);
        Console.WriteLine(foundEngineer);
    }
    private static void ReadAllEngineers()
    {
        IEnumerable<Engineer?> engineers = s_dal!.Engineer.ReadAll();
        foreach (Engineer? engineer in engineers)
        {
            if (engineer?.Status == true) Console.WriteLine(engineer);
        }
    }
    private static void UpdateEngineer()
    {
        Console.Write("Enter Engineer ID.");
        int Id;
        int.TryParse(Console.ReadLine(), out Id);
        Engineer? foundEngineer = s_dal!.Engineer.Read(Id);
        Console.WriteLine(foundEngineer);
        Console.WriteLine("Enter values to update.");
        Console.WriteLine("Enter updated Engineer's Name.");
        string? name = Console.ReadLine();
        if (string.IsNullOrEmpty(name))
            name = foundEngineer!.Name;
        Console.WriteLine("Enter updated Engineer's E-mail.");
        string? email = Console.ReadLine();
        if (string.IsNullOrEmpty(email))
            email = foundEngineer!.Email;
        Console.WriteLine("Enter updated Engineer's Level(1-5).");
        int level;
        int.TryParse(Console.ReadLine(), out level);
        if(level == 0)
            level = (int)foundEngineer!.Level+1;
        Console.WriteLine("Enter updated Engineer's Cost.");
        double cost;
        double.TryParse(Console.ReadLine(), out cost);
        if (cost == 0)
            cost = foundEngineer!.Cost;
        Engineer newEngineer = new Engineer(Id,name, email, (EngineerExperience)(level-1), (double)cost);
        try
        {
            s_dal!.Engineer.Update(newEngineer);
        }
        catch (DalAlreadyExistsException ex) { throw ex; }
        catch (DalDeletionImpossible ex) { throw ex; }
        catch (DalDoesNotExistException ex) { throw ex; }
        catch (Exception ) { throw ; }

    }
    private static void DeleteEngineer()
    {
        Console.Write("Enter Engineer ID.");
        int Id;
        int.TryParse(Console.ReadLine(), out Id);
        try
        {
            s_dal!.Engineer.Delete(Id);
        }
        catch (DalAlreadyExistsException ex) { throw ex; }
        catch (DalDeletionImpossible ex) { throw ex; }
        catch (DalDoesNotExistException ex) { throw ex; }
        catch (Exception ) { throw ; }
    }
    private static void TaskMenu()
    {
        do
        {
            Console.Write("Enter your choice:\n 1 to create an task,\n 2 to read an task,\n" +
                           " 3 to read all tasks,\n 4 to update an task, \n 5 to delete an task, \n 0 to exit\n");
            int choice;
            int.TryParse(Console.ReadLine(), out choice);
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
            catch (DalAlreadyExistsException ex) { Console.WriteLine(ex.Message); }
            catch (DalDeletionImpossible ex) { Console.WriteLine(ex.Message); }
            catch (DalDoesNotExistException ex) { Console.WriteLine(ex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        } while (true);
    }
    private static void CreateTask()
    {
        Console.Write("Enter Task's Description, Alias, Milestone(0/1),\n" +
                      "Created At,Start Date,Scheduled Date\n" +
                      "Deliverables, Remarks, EngineerId, ComplexityLevel(1-5).");
        string Description = Console.ReadLine() ?? "";
        string Alias = Console.ReadLine() ?? "";
        bool Milestone;
        bool.TryParse(Console.ReadLine(), out Milestone);
        DateTime CreatedAt;
        DateTime.TryParse(Console.ReadLine(), out CreatedAt);
        DateTime Start;
        DateTime.TryParse(Console.ReadLine(), out Start);
        DateTime ScheduledDate;
        DateTime.TryParse(Console.ReadLine(), out ScheduledDate);
        string? Deliverables = Console.ReadLine();
        string? Remarks = Console.ReadLine();
        int EngineerId;
        int.TryParse(Console.ReadLine(), out EngineerId);
        int ComplexityLevel;
        int.TryParse(Console.ReadLine(), out ComplexityLevel);
        Task newTask = new Task(0, Description, Alias, Milestone,CreatedAt, Start,
                                ScheduledDate, null,null, null, Deliverables,
                                Remarks, EngineerId,(EngineerExperience)(ComplexityLevel - 1));
        try
        {
            Console.WriteLine(s_dal!.Task.Create(newTask));
        }
        catch (DalAlreadyExistsException ex) { throw ex; }
        catch (DalDeletionImpossible ex) { throw ex; }
        catch (DalDoesNotExistException ex) { throw ex; }
        catch (Exception ) { throw ; }
    }
    private static void ReadTask()
    {
        Console.Write("Enter Task ID.");
        int Id;
        int.TryParse(Console.ReadLine(), out Id);
        Task? foundTask = s_dal!.Task.Read(Id);
        Console.WriteLine(foundTask);
    }
    private static void ReadAllTasks()
    {
        IEnumerable<Task?> tasks = s_dal!.Task.ReadAll();
        foreach (Task? task in tasks)
        {
            Console.WriteLine(task);
        }
    }
    private static void UpdateTask()
    {
        Console.Write("Enter Task ID.");
        int Id;
        int.TryParse(Console.ReadLine(), out Id);
        Task? foundTask = s_dal!.Task.Read(Id);
        Console.WriteLine(foundTask);
        Console.WriteLine("Enter values to update,");
        Console.WriteLine("Enter updated Task's Description:");
        string? Description = Console.ReadLine();
        if (string.IsNullOrEmpty(Description))
            Description = foundTask!.Description;
        Console.WriteLine("Enter updated Task's Alias:");
        string? Alias = Console.ReadLine();
        if (string.IsNullOrEmpty(Alias))
            Alias = foundTask!.Alias;
        Console.WriteLine("Enter updated Task's Milestone:");
        bool Milestone;
        bool.TryParse(Console.ReadLine(), out Milestone);
        Console.WriteLine("Enter updated Task's Created At:");
        DateTime CreatedAt;
        DateTime.TryParse(Console.ReadLine(), out CreatedAt);
        if(CreatedAt == DateTime.MinValue)
            CreatedAt = foundTask!.CreatedAt;
        Console.WriteLine("Enter updated Task's Start date:");
        DateTime Start1;
        DateTime? Start;
        DateTime.TryParse(Console.ReadLine() , out Start1);
        if (Start1 == DateTime.MinValue)
            Start = (DateTime?)foundTask?.Start;
        else
            Start = Start1;
        Console.WriteLine("Enter updated Task's ScheduledDate:");
        DateTime ScheduledDate1;
        DateTime? ScheduledDate;
        DateTime.TryParse(Console.ReadLine() , out ScheduledDate1);
        if (ScheduledDate1 == DateTime.MinValue)
            ScheduledDate = (DateTime?)foundTask?.ScheduledDate;
        else
            ScheduledDate = ScheduledDate1;
        Console.WriteLine("Enter updated Task's ForecastDate:");
        DateTime ForecastDate1;
        DateTime? ForecastDate;
        DateTime.TryParse(Console.ReadLine() , out ForecastDate1);
        if (ForecastDate1 == DateTime.MinValue)
            ForecastDate = (DateTime?)foundTask?.ForecastDate;
        else
            ForecastDate = ForecastDate1;
        Console.WriteLine("Enter updated Task's Deadline:");
        DateTime? Deadline;
        DateTime Deadline1;
        DateTime.TryParse(Console.ReadLine(), out Deadline1);
        if (Deadline1 == DateTime.MinValue)
            Deadline = (DateTime?)foundTask?.Deadline;
        else
            Deadline = Deadline1;
        Console.WriteLine("Enter updated Task's Complete date:");
        DateTime Complete1;
        DateTime? Complete;
        DateTime.TryParse(Console.ReadLine(), out Complete1);
        if (Complete1 == DateTime.MinValue)
            Complete = (DateTime?)foundTask?.Complete;
        else
            Complete = Complete1;
        Console.WriteLine("Enter updated Task's Deliverables:");
        string? Deliverables = Console.ReadLine();
        if(string.IsNullOrEmpty(Deliverables))
            Deliverables = foundTask!.Deliverables;
        Console.WriteLine("Enter updated Task's Remarks:");
        string? Remarks = Console.ReadLine();
        if(string.IsNullOrEmpty(Remarks))
            Remarks = foundTask!.Remarks;
        Console.WriteLine("Enter updated Task's Engineer Id:");
        int EngineerId1;
        int? EngineerId;
        int.TryParse(Console.ReadLine() , out EngineerId1);
        if (EngineerId1 == 0)
            EngineerId =(int?)foundTask?.EngineerId;
        else
            EngineerId = EngineerId1;
        Console.WriteLine("Enter updated Task's Complexity Level (1-5):");
        int? ComplexityLevel;
        int ComplexityLevel1;
        int.TryParse(Console.ReadLine() , out ComplexityLevel1);
        if (ComplexityLevel1 == 0)
            ComplexityLevel = (int?)foundTask?.ComplexityLevel + 1;
        else
            ComplexityLevel = ComplexityLevel1;
        Task newTask = new Task(Id,Description,Alias,Milestone,CreatedAt,Start,ScheduledDate,
                                 ForecastDate,Deadline,Complete,Deliverables,Remarks,EngineerId,
                                 (EngineerExperience?)(ComplexityLevel-1));
        try
        {
            s_dal!.Task.Update(newTask);
        }
        catch (DalAlreadyExistsException ex) { throw ex; }
        catch (DalDeletionImpossible ex) { throw ex; }
        catch (DalDoesNotExistException ex) { throw ex; }
        catch (Exception) { throw; }
    }
    private static void DeleteTask()
    {
        Console.Write("Enter Task ID.");
        int Id;
        int.TryParse(Console.ReadLine(), out Id);
        try
        {
            s_dal!.Task.Delete(Id);
        }
        catch (DalAlreadyExistsException ex) { throw ex; }
        catch (DalDeletionImpossible ex) { throw ex; }
        catch (DalDoesNotExistException ex) { throw ex; }
        catch (Exception) { throw; }
    }
    private static void DependencyMenu()
    {
        do
        {
            Console.Write("Enter your choice:\n 1 to create an dependency,\n 2 to read an dependency,\n" +
                           " 3 to read all dependencys,\n 4 to update an dependency, \n 5 to delete an dependency, \n 0 to exit\n");
            int choice;
            int.TryParse(Console.ReadLine(), out choice);
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
            catch (DalAlreadyExistsException ex) { Console.WriteLine(ex.Message); }
            catch (DalDeletionImpossible ex) { Console.WriteLine(ex.Message); }
            catch (DalDoesNotExistException ex) { Console.WriteLine(ex.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        } while (true);
    }
    private static void CreateDependency()
    {
        Console.Write("Enter Dependency's DependentTask,DependentOnTask:\n");
        int DependentTask;
        int.TryParse(Console.ReadLine(), out DependentTask);
        int DependentOnTask;
        int.TryParse(Console.ReadLine()??null, out DependentOnTask);
        Dependency newDepndency = new Dependency(0,DependentTask, DependentOnTask);
        try
        {
            Console.WriteLine(s_dal!.Dependency.Create(newDepndency));
        }
        catch (DalAlreadyExistsException ex) { throw ex; }
        catch (DalDeletionImpossible ex) { throw ex; }
        catch (DalDoesNotExistException ex) { throw ex; }
        catch  { throw ; }
    }
    private static void ReadDependency()
    {
        Console.Write("Enter Dependency ID.");
        int Id;
        int.TryParse(Console.ReadLine(), out Id);
        Dependency? foundDepndency = s_dal!.Dependency.Read(Id);
        Console.WriteLine(foundDepndency);
    }
    private static void ReadAllDependencys()
    {
        IEnumerable<Dependency>? dependencies = s_dal!.Dependency.ReadAll()!;
        foreach (Dependency dependency in dependencies)
        {
            Console.WriteLine(dependency);
        }
    }
    private static void UpdateDependency()
    {
        Console.Write("Enter Dependency ID.");
        int Id;
        int.TryParse(Console.ReadLine(), out Id);
        Dependency? foundDepndency = s_dal!.Dependency.Read(Id);
        Console.WriteLine(foundDepndency);
        Console.WriteLine("Enter values to update.");
        Console.WriteLine("Enter updated DependentTask:");
        int DependentTask;
        int.TryParse(Console.ReadLine() , out DependentTask);
        if(DependentTask == 0) 
        {
            DependentTask = (int)foundDepndency?.DependentTask!;
        }
        Console.WriteLine("Enter updated DependentOnTask:");
        int DependentOnTask;
        int.TryParse(Console.ReadLine() , out DependentOnTask);
        if (DependentOnTask == 0)
        {
            DependentOnTask = (int)foundDepndency?.DependentOnTask!;
        }
        Dependency newDependency = new Dependency(Id, DependentTask,DependentOnTask);
        try
        {
            s_dal!.Dependency.Update(newDependency);
        }
        catch (DalAlreadyExistsException ex) { throw ex; }
        catch (DalDeletionImpossible ex) { throw ex; }
        catch (DalDoesNotExistException ex) { throw ex; }
        catch (Exception ) { throw; }

    }
    private static void DeleteDependency()
    {
        Console.Write("Enter Dependency ID.");
        int Id;
        int.TryParse(Console.ReadLine(), out Id);
        try
        {
            s_dal!.Dependency.Delete(Id);
        }
        catch (DalAlreadyExistsException ex) { throw ex; }
        catch (DalDeletionImpossible ex) { throw ex; }
        catch (DalDoesNotExistException ex) { throw ex; }
        catch (Exception ) { throw ; }
    }
    private static void Main(string[] args)
    {
        try
        {
            //Initialization.Do(s_dal); //stage 2
            Initialization.Do(); //stage 4
            MainMenu();
        }
        catch (DalAlreadyExistsException ex) { Console.WriteLine(ex.Message); }
        catch (DalDeletionImpossible ex) { Console.WriteLine(ex.Message); }
        catch (DalDoesNotExistException ex) { Console.WriteLine(ex.Message); }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }
}