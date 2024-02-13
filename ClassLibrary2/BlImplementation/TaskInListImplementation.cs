namespace BlImplementation;
using BlApi;
using BO;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

internal class TaskInListImplementation : ITaskInList
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    private int setStatus(DO.Task task)
    {
        try
        {
            int status = 0;
            if (task.Complete is not null)
                status = 4;
            else if (task.ForecastDate < DateTime.Now)
                status = 3;
            else if (task.Start < DateTime.Now)
                status = 2;
            else if (task.ForecastDate is not null)
                status = 1;
            else status = 0;
            return status;
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with Id={task.Id} was not found", ex);
        }
    }
    public BO.TaskInList? Read(int id)
    {
        try
        {
            DO.Task? newDoTask = _dal.Task.Read(id);
            if (newDoTask == null)
                throw new BO.BlDoesNotExistException("Task does not exist");
            BO.TaskInList taskInList = new BO.TaskInList
            {
                Id = id,
                Description = newDoTask.Description,
                Alias = newDoTask.Alias,
                Status = (BO.Status)setStatus(newDoTask!),
                ComplexityLevel = (BO.EngineerExperience?)newDoTask.ComplexityLevel
            };
            return taskInList;
        }
        catch (Exception ex)
        {
            throw new BO.BlDoesNotExistException($"Task was not found", ex);
        }
    }


    public IEnumerable<BO.TaskInList?> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        IEnumerable<DO.Task> allDoTasks = _dal.Task.ReadAll(filter)!;
        IEnumerable<BO.TaskInList> tasksInList = from task in allDoTasks
                                                 select Read(task.Id);
        return tasksInList;
    }


}
