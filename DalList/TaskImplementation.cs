namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
/// <summary>
/// Task Implementation (CRUD)
/// </summary>
internal class TaskImplementation : ITask
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
        throw new DalDeletionImpossible($"A Task can't be deleted.");
    }

    public Task? Read(int id)
    {
        Task? foundValue = DataSource.Tasks?.Where(task => task.Id == id).FirstOrDefault();
        return foundValue != null ? foundValue : null;
    }

    public Task? Read(Func<Task, bool> filter)
    {
        Task? foundValue = DataSource.Tasks?.Where(filter).FirstOrDefault();
        return foundValue != null ? foundValue : null;
    }

    public IEnumerable<Task?> ReadAll(Func<Task?, bool>? filter = null) //stage 2
    {
        if (filter == null)
            return DataSource.Tasks.Select(item => item);
        else
            return DataSource.Tasks.Where(filter);

    }

    public void Update(Task item)
    {
        Task? foundValue = DataSource.Tasks?.Where(task => task.Id == item.Id).FirstOrDefault();
        if (foundValue == null)
        {
            throw new DalDoesNotExistException($"A Task with {item.Id} id does not exist.");
        }
        DataSource.Tasks!.RemoveAll(task => task.Id == item.Id);
        DataSource.Tasks!.Add(item);
    }
}
