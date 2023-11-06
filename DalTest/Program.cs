namespace DalTest;
using DalApi;
using DO;
using DalList;
using Dal;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Transactions;
using System.Xml.Linq;

internal class Program
{
    private static IDependency? s_dalDependency = new DependencyImplementation(); //stage 1
    private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
    private static ITask? s_dalTask = new TaskImplementation(); //stage 1

    private static void MainMenu()
    {
        do
        {
            Console.Write("Enter your choice:\n 1 for Engineer menu,\n 2 for Task menu,\n" +
                           " 3 for Depnendency menu,\n0 to exit ");
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
        Console.Write("Enter Engineer's ID, Name, E-mail, Level(1-5) and pay per hour.");
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
            Console.WriteLine(s_dalEngineer!.Create(newEngineer));
        } catch (Exception ex) { throw ex; }
    }
    private static void ReadEngineer()
    {
        Console.Write("Enter Engineer ID.");
        int Id; 
        int.TryParse(Console.ReadLine(), out Id);
        Engineer? foundEngineer = s_dalEngineer!.Read(Id);
        Console.WriteLine(foundEngineer);
    }
    private static void ReadAllEngineers()
    {
        List<Engineer>? engineers = s_dalEngineer!.ReadAll();
        engineers.ForEach(engineer => { Console.WriteLine(engineer); });
    }
    private static void UpdateEngineer()
    {
        Console.Write("Enter Engineer ID.");
        int Id;
        int.TryParse(Console.ReadLine(), out Id);
        Engineer? foundEngineer = s_dalEngineer!.Read(Id);
        Console.WriteLine(foundEngineer);
        Console.WriteLine("Enter values to update.");
        Console.WriteLine("Enter updated Engineer's Name.");
        string? name = Console.ReadLine();
        if (name == null)
            name = foundEngineer!.Name;
        Console.WriteLine("Enter updated Engineer's E-mail.");
        string? email = Console.ReadLine();
        if (email == null)
            email = foundEngineer!.Email;
        Console.WriteLine("Enter updated Engineer's Level(1-5).");
        int level;
        int.TryParse(Console.ReadLine()?? "-1", out level);
        if (level == -1)
            level = (int)foundEngineer!.Level;
        Console.WriteLine("Enter updated Engineer's Cost.");
        double cost;
        double.TryParse(Console.ReadLine() ?? "-1", out cost);
        if (cost == -1)
            cost = foundEngineer!.Cost;
        Engineer newEngineer = new Engineer(Id,name, email, (EngineerExperience)(level-1), (double)cost);
        try
        {
            s_dalEngineer!.Update(newEngineer);
        }catch (Exception ex) { throw ex; }
        
    }
    private static void DeleteEngineer()
    {
        Console.Write("Enter Engineer ID.");
        int Id;
        int.TryParse(Console.ReadLine(), out Id);
        try
        {
            s_dalEngineer!.Delete(Id);
        }catch(Exception ex) { throw ex; }
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
        Console.Write("Enter Task's Description, Alias, Milestone(0/1),\n" +
                     " Created At,Start Date,Scheduled Date\n" +
                     "Deliverables, Remarks, EngineerId, ComplexityLevel(1-5).");
        string Description = Console.ReadLine() ?? "";
        string Alias = Console.ReadLine() ?? "";
        bool Milestone;
        bool.TryParse(Console.ReadLine(), out Milestone);
        DateTime CreatedAt;
        DateTime.TryParse(Console.ReadLine(), out CreatedAt);
        DateTime Start;
        DateTime.TryParse(Console.ReadLine()??$"{DateTime.Today.AddDays(30)}", out Start);
        DateTime ScheduledDate;
        DateTime.TryParse(Console.ReadLine() ?? $"{DateTime.Today.AddDays(70)}", out ScheduledDate);
        string? Deliverables = Console.ReadLine();
        string? Remarks = Console.ReadLine();
        int EngineerId;
        int.TryParse(Console.ReadLine()??null, out EngineerId);
        int ComplexityLevel;
        int.TryParse(Console.ReadLine()??null, out ComplexityLevel);
        Task newTask = new Task(0, Description, Alias, Milestone,CreatedAt, Start,
                                ScheduledDate, null,null, null, Deliverables,
                                Remarks, EngineerId,(EngineerExperience)(ComplexityLevel - 1));
        try
        {
            Console.WriteLine(s_dalTask!.Create(newTask));
        }
        catch (Exception ex) { throw ex; }
    }
    private static void ReadTask()
    {
        Console.Write("Enter Task ID.");
        int Id;
        int.TryParse(Console.ReadLine(), out Id);
        Task? foundTask = s_dalTask!.Read(Id);
        Console.WriteLine(foundTask);
    }
    private static void ReadAllTasks()
    {
        List<Task>? tasks = s_dalTask!.ReadAll();
        tasks.ForEach(task => { Console.WriteLine(task); });
    }
    private static void UpdateTask()
    {
        Console.Write("Enter Task ID.");
        int Id;
        int.TryParse(Console.ReadLine(), out Id);
        Task? foundTask = s_dalTask!.Read(Id);
        Console.WriteLine(foundTask);
        Console.WriteLine("Enter values to update,");
        Console.WriteLine("Enter updated Task's Description:");
        string? Description = Console.ReadLine();
        if (Description == null)
            Description = foundTask!.Description;
        Console.WriteLine("Enter updated Task's Alias:");
        string? Alias = Console.ReadLine();
        if (Alias == null)
            Alias = foundTask!.Alias;
        Console.WriteLine("Enter updated Task's Milestone:");
        bool Milestone;
        bool.TryParse(Console.ReadLine()??$"{foundTask!.Milestone}", out Milestone);
        Console.WriteLine("Enter updated Task's Created At:");
        DateTime CreatedAt;
        DateTime.TryParse(Console.ReadLine()?? $"{foundTask!.CreatedAt}", out CreatedAt);
        Console.WriteLine("Enter updated Task's Start date:");
        DateTime Start;
        DateTime.TryParse(Console.ReadLine() ?? $"{foundTask.Start}", out Start);
        Console.WriteLine("Enter updated Task's ScheduledDate:");
        DateTime ScheduledDate;
        DateTime.TryParse(Console.ReadLine() ?? $"{foundTask.ScheduledDate}", out ScheduledDate);
        Console.WriteLine("Enter updated Task's ForecastDate:");
        DateTime ForecastDate;
        DateTime.TryParse(Console.ReadLine() ?? $"{foundTask.ForecastDate}", out ForecastDate);
        Console.WriteLine("Enter updated Task's Deadline:");
        DateTime Deadline;
        DateTime.TryParse(Console.ReadLine() ?? $"{foundTask.Deadline}", out Deadline);
        Console.WriteLine("Enter updated Task's Complete date:");
        DateTime Complete;
        DateTime.TryParse(Console.ReadLine() ?? $"{foundTask.Complete}", out Complete);
        Console.WriteLine("Enter updated Task's Deliverables:");
        string? Deliverables = Console.ReadLine();
        if( Deliverables == null)
            Deliverables = foundTask!.Deliverables;
        Console.WriteLine("Enter updated Task's Remarks:");
        string? Remarks = Console.ReadLine();
        if( Remarks == null)
            Remarks = foundTask!.Remarks;
        Console.WriteLine("Enter updated Task's Engineer Id:");
        int EngineerId;
        int.TryParse(Console.ReadLine() ??$"{foundTask.EngineerId}", out EngineerId);
        Console.WriteLine("Enter updated Task's Complexity Level (1-5):");
        int ComplexityLevel;
        int.TryParse(Console.ReadLine() ?? $"{(int)foundTask.ComplexityLevel}", out ComplexityLevel);
        Task newTask = new Task(Id,Description,Alias,Milestone,CreatedAt,Start,ScheduledDate,
                                 ForecastDate,Deadline,Complete,Deliverables,Remarks,EngineerId,
                                 (EngineerExperience)(ComplexityLevel-1));
        try
        {
            s_dalTask!.Update(newTask);
        }catch (Exception ex) { throw ex; }
    }
    private static void DeleteTask()
    {
        Console.Write("Enter Task ID.");
        int Id;
        int.TryParse(Console.ReadLine(), out Id);
        try
        {
            s_dalTask!.Delete(Id);
        }
        catch (Exception ex) { throw ex; }
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
        Console.Write("Enter Dependency's DependentTask,DependentOnTask.");
        int DependentTask;
        int.TryParse(Console.ReadLine(), out DependentTask);
        int DependentOnTask;
        int.TryParse(Console.ReadLine()??null, out DependentOnTask);
        Dependency newDepndency = new Dependency(0,DependentTask, DependentOnTask);
        try
        {
            Console.WriteLine(s_dalDependency!.Create(newDepndency));
        }
        catch (Exception ex) { throw ex; }
    }
    private static void ReadDependency()
    {
        Console.Write("Enter Dependency ID.");
        int Id;
        int.TryParse(Console.ReadLine(), out Id);
        Dependency? foundDepndency = s_dalDependency!.Read(Id);
        Console.WriteLine(foundDepndency);
    }
    private static void ReadAllDependencys()
    {
        List<Dependency>? dependencies = s_dalDependency!.ReadAll();
        dependencies.ForEach(dependency => { Console.WriteLine(dependency); });
    }
    private static void UpdateDependency()
    {
        Console.Write("Enter Dependency ID.");
        int Id;
        int.TryParse(Console.ReadLine(), out Id);
        Dependency? foundDepndency = s_dalDependency!.Read(Id);
        Console.WriteLine(foundDepndency);
        Console.WriteLine("Enter values to update.");
        Console.WriteLine("Enter updated DependentTask:");
        int DependentTask;
        int.TryParse(Console.ReadLine() ?? $"{foundDepndency.DependentTask}", out DependentTask);
        Console.WriteLine("Enter updated DependentOnTask:");
        int DependentOnTask;
        int.TryParse(Console.ReadLine() ?? $"{foundDepndency.DependentOnTask}", out DependentOnTask);
        Dependency newDependency = new Dependency(Id, DependentTask,DependentOnTask);
        try
        {
            s_dalDependency!.Update(newDependency);
        }
        catch (Exception ex) { throw ex; }

    }
    private static void DeleteDependency()
    {
        Console.Write("Enter Dependency ID.");
        int Id;
        int.TryParse(Console.ReadLine(), out Id);
        try
        {
            s_dalDependency!.Delete(Id);
        }
        catch (Exception ex) { throw ex; }
    }
    private static void Main(string[] args)
    {
        try
        {
            Initialization.Do(s_dalDependency, s_dalEngineer, s_dalTask);
            MainMenu();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}