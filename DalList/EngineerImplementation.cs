namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {

        Engineer? foundValue = DataSource.Engineers?.Find(eng => eng.Id == item.Id);
        if (foundValue != null)
        {
            throw new Exception($"An Engineer with {item.Id} id already exists.");
        }
        DataSource.Engineers!.Add(item);
        return item.Id;
    }

    public void Delete(int id)
    {
        Engineer? foundValue = DataSource.Engineers?.Find(eng => eng.Id == id);
        if (foundValue == null)
        {
            throw new Exception($"An Engineer with {id} id does not exist.");
        }
        Engineer copyItem = foundValue with { Status = false };
        DataSource.Engineers!.Remove(copyItem);
        DataSource.Engineers!.Add(copyItem);
    }

    public Engineer? Read(int id)
    {
        Engineer? foundValue = DataSource.Engineers?.Find(eng => eng.Id == id);
        return foundValue != null ? foundValue : null;
    }

    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
    }

    public void Update(Engineer item)
    {
        Engineer? foundValue = DataSource.Engineers?.Find(eng => eng.Id == item.Id);
        if (foundValue == null)
        {
            throw new Exception($"An Engineer with {item.Id} id does not exist.");
        }
        DataSource.Engineers!.Remove(foundValue!);
        DataSource.Engineers!.Add(item);
    }
}
