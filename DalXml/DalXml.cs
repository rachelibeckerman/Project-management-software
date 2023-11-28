using DalApi;
namespace Dal;
/// <summary>
/// Defining the basic functions (CRUD). 
/// </summary>
//stage 3
sealed public class DalXml : IDal
{
    public IDependency Dependency => new DependencyImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();
    
}
