namespace BlImplementation;
using BlApi;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

internal class TaskInEngineerImplementation : ITaskInEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    /// <summary>
    /// read task in engineer
    /// </summary>
    /// <param name="id"></param>
    /// <returns>BO.TaskInEngineer</returns>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public BO.TaskInEngineer? Read(int id)
    {
        DO.Task? newDoTask = _dal.Task.Read(id);
        if (newDoTask == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does not exist");
        BO.TaskInEngineer taskInEngineer = new BO.TaskInEngineer
        {
            Id = id,
            Alias = newDoTask.Alias
        };
        return taskInEngineer;
    }

    /// <summary>
    /// read all tasks in engineer
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<BO.TaskInEngineer?> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        IEnumerable<DO.Task> allDoTasks = _dal.Task.ReadAll(filter)!;
        IEnumerable<BO.TaskInEngineer> tasksInEngineer = from task in allDoTasks
                                                         select Read(task.Id);
        return tasksInEngineer;
    }


}
