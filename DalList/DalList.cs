using DalApi;

namespace Dal;

sealed internal class DalList : IDal
{

    public static IDal Instance { get; } = new Lazy<DalList>(()=>new DalList(),true).Value;
   
    public IDependency Dependency => new DependencyImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();
    private DalList() { }
}
