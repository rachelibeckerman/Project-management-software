namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;

internal class TaskImplementetion : ITask
{
    public int Create(Task item)
    {
        List<Task> tasksList = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        int newId = Config.NextTaskId;
        Task copyItem = item with { Id = newId };
        tasksList.Add(copyItem);
        XMLTools.SaveListToXMLSerializer<Task>(tasksList, "tasks");
        return newId;
    }

    public void Delete(int id)
    {
        throw new DalDeletionImpossible($"A Task can't be deleted.");
    }

    public Task? Read(int id)
    {
        List<Task> tasksList = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        Task? foundValue = tasksList.Where(task => task.Id == id).First();
        return foundValue != null ? foundValue : null;
    }

    public Task? Read(Func<Task, bool> filter)
    {
        List<Task> tasksList = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        Task? foundValue = tasksList.Where(filter).First();
        return foundValue != null ? foundValue : null;
    }

    public IEnumerable<Task?> ReadAll(Func<Task?, bool>? filter = null) //stage 2
    {
        List<Task> tasksList = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        if (filter == null)
            return tasksList.Select(item => item);
        else
            return tasksList.Where(filter);

    }

    public void Update(Task item)
    {
        List<Task> tasksList = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        Task? foundValue = tasksList.Where(task => task.Id == item.Id).First();
        if (foundValue == null)
        {
            throw new DalDoesNotExistException($"A Task with {item.Id} id does not exist.");
        }
        tasksList.RemoveAll(task => task.Id == item.Id);
        tasksList.Add(item);
        XMLTools.SaveListToXMLSerializer<Task>(tasksList, "tasks");
    }
}
