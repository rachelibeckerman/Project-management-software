namespace Dal;
using DalApi;
using DO;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Dependency Implementation (CRUD)
/// </summary>
internal class DependencyImplementation : IDependency
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
        Dependency? foundValue = DataSource.Dependencies?.Where(dep => dep.Id == id).First();
        if (foundValue == null)
        {
            throw new DalDoesNotExistException($"An Dependency with {id} id does not exist.");
        }
        DataSource.Dependencies!.RemoveAll(dep => dep.Id == id);
    }

    public Dependency? Read(int id)
    {
        Dependency? foundValue = DataSource.Dependencies?.Where( dep => dep.Id == id).First();
        return foundValue != null ? foundValue : null;
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        Dependency? foundValue = DataSource.Dependencies?.Where(filter).First();
        return foundValue != null ? foundValue : null;
    }

    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter = null) //stage 2
    {
        if (filter != null)
            return from dep in DataSource.Dependencies
                   where filter(dep)
                   select dep;
        return from dep in DataSource.Dependencies
               select dep;
    }

    public void Update(Dependency item)
    {
        Dependency? foundValue = DataSource.Dependencies?.Where(dep => dep.Id == item.Id).First();
        if (foundValue == null)
        {
            throw new DalDoesNotExistException($"An Dependency with {item.Id} id does not exist.");
        }
        DataSource.Dependencies!.RemoveAll(dep => dep.Id == item.Id);
        DataSource.Dependencies!.Add(item);
    }
}
