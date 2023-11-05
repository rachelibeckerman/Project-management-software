namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        int newId = DataSource.Config.NextDependencyId;
        Dependency copyItem = item with { Id = newId };
        DataSource.Dependencies!.Add(copyItem);
        return newId;
    }

    public void Delete(int id)
    {
        Dependency? foundValue = DataSource.Dependencies?.Find(dep => dep.Id == id);
        if (foundValue == null)
        {
            throw new Exception($"An Dependency with {id} id does not exist.");
        }
        DataSource.Dependencies!.Remove(foundValue);
    }

    public Dependency? Read(int id)
    {
        Dependency? foundValue = DataSource.Dependencies?.Find(dep => dep.Id == id);
        return foundValue != null ? foundValue : null;
    }

    public List<Dependency> ReadAll()
    {
       return new List<Dependency>(DataSource.Dependencies);
    }

    public void Update(Dependency item)
    {
        Dependency? foundValue = DataSource.Dependencies?.Find(dep => dep.Id == item.Id);
        if (foundValue == null)
        {
            throw new Exception($"An Dependency with {item.Id} id does not exist.");
        }
        DataSource.Dependencies!.Remove(foundValue!);
        DataSource.Dependencies!.Add(item);
    }
}
