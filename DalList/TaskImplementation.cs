namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
/// <summary>
/// Task Implementation (CRUD)
/// </summary>
public class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        int newId = DataSource.Config.NextTaskId;
        Task copyItem = item with { Id = newId };
        DataSource.Tasks!.Add(copyItem);
        return newId;
    }

    public void Delete(int id)
    {
        throw new Exception($"A Task can't be deleted.");
    }

    public Task? Read(int id)
    {
        Task? foundValue = DataSource.Tasks?.Find(task => task.Id == id);
        return foundValue != null ? foundValue : null;
    }

    public List<Task> ReadAll()
    {
       return new List<Task>(DataSource.Tasks);
    }

    public void Update(Task item)
    {
        Task? foundValue = DataSource.Tasks?.Find(task => task.Id == item.Id);
        if (foundValue == null)
        {
            throw new Exception($"A Task with {item.Id} id does not exist.");
        }
        DataSource.Tasks!.Remove(foundValue!);
        DataSource.Tasks!.Add(item);
    }
}
