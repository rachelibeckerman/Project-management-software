using DalApi;
using System.Diagnostics;

namespace Dal;
/// <summary>
/// Defining the basic functions (CRUD). 
/// </summary>
//stage 3
sealed internal class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
  
    public IDependency Dependency => new DependencyImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();
    private DalXml() { }

}
