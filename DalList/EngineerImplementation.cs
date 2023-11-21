namespace Dal;
using DalApi;
using DO;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Engineer Implementation (CRUD)
/// </summary>
internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        Engineer? foundValue = DataSource.Engineers?.Where(eng => eng.Id == item.Id).FirstOrDefault();
        if (foundValue != null)
        {
            throw new DalAlreadyExistsException($"An Engineer with {item.Id} ID already exists.");
        }
        DataSource.Engineers!.Add(item);
        return item.Id;
    }

    public void Delete(int id)
    {
        Engineer? foundValue =DataSource.Engineers?.Where(eng => eng.Id == id).FirstOrDefault();
        if (foundValue == null)
        {
            throw new DalDoesNotExistException($"An Engineer with {id} ID does not exist.");
        }
        Engineer copyItem = foundValue with { Status = false };
        DataSource.Engineers!.RemoveAll(eng => eng.Id == id);
        DataSource.Engineers!.Add(copyItem);
    }

    public Engineer? Read(int id)
    {
        Engineer? foundValue = DataSource.Engineers?.Where(eng => eng.Id == id).FirstOrDefault();
        return foundValue != null ? foundValue : null;
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        Engineer? foundValue = DataSource.Engineers?.Where(filter).FirstOrDefault();
        return foundValue != null ? foundValue : null;
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer?, bool>? filter = null) //stage 2
    {
        if (filter == null)
            return DataSource.Engineers.Select(item => item);
        else
            return DataSource.Engineers.Where(filter);

    }

    public void Update(Engineer item)
    {
        Engineer? foundValue = DataSource.Engineers?.Where(eng => eng.Id == item.Id).FirstOrDefault();
        if (foundValue == null)
        {
            throw new DalDoesNotExistException($"An Engineer with {item.Id} ID does not exist.");
        }
        DataSource.Engineers!.RemoveAll(eng => eng.Id == item.Id);
        DataSource.Engineers!.Add(item);
    }
}
