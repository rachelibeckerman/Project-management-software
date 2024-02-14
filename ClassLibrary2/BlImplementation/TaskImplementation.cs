namespace BlImplementation;
using BlApi;
using BO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Xml.Linq;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    /// <summary>
    /// create task entity
    /// </summary>
    /// <param name="task"></param>
    /// <returns>new task id</returns>
    /// <exception cref="BO.BlNullPropertyException"></exception>
    /// <exception cref="BO.BlInvalidInputException"></exception>
    /// <exception cref="BO.BlAlreadyExistException"></exception>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public int Create(BO.Task task)
    {
        //validtion tests
        if (task.Description is null || task.Description == "" || task.Alias is null || task.Alias == "")
            throw new BO.BlNullPropertyException("description or alias was not valid");
        if (task.Start < task.CreatedAt || task.ScheduledDate < task.Start || task.ForecastDate > task.Deadline || task.Complete < task.ScheduledDate)
            throw new BO.BlInvalidInputException("not valid dates");

        DO.Task doTask = ConvertBoTaskToDoTask(task);

        try
        {
            //check if task has an engineer
            if (task.Engineer is not null && task.Engineer.Id != 0)
            {
                //check if wanted engineer exists
                if (_dal.Engineer.Read(task.Engineer.Id) == null)
                {
                    throw new BO.BlDoesNotExistException("Engineer was not found");
                }
                //check if a diffrent task with the same engineer already exists
                IEnumerable<DO.Task> allTasks = _dal.Task.ReadAll()!;
                DO.Task? newDoTask = (from newTask in allTasks
                                      where newTask.EngineerId == task.Engineer.Id
                                      select newTask).FirstOrDefault();
                if (newDoTask != null && newDoTask.Id != task.Id)
                {
                    throw new BO.BlAlreadyExistException("Engineer is busy");
                }
            }
            _dal.Task.Create(doTask);//create DO.Task
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistException($"Task with ID={task.Id} already exists", ex);
        }
        return doTask.Id;
    }

    /// <summary>
    ///update status according to task progress 
    /// </summary>
    /// <param name="task"></param>
    /// <returns>status</returns>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
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
            throw new BO.BlDoesNotExistException($"Task was not found", ex);
        }
    }
    /// <summary>
    /// delete task
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="BO.BlDeletionImpossibleException"></exception>
    public void Delete(int id)
    {
        try
        {
            _dal.Task.Delete(id);
        }
        catch (DO.DalDeletionImpossible ex)
        {
            throw new BO.BlDeletionImpossibleException("A Task can't be deleted.", ex);
        }
    }
    /// <summary>
    /// Convert Do.Task To Bo.Task
    /// </summary>
    /// <param name="task"></param>
    /// <returns>BO.Task from DO.Task</returns>
    private BO.Task ConvertDoTaskToBoTask(DO.Task task)
    {
        BO.Task newBoTask = new BO.Task
        {
            Id = task.Id,
            Description = task.Description,
            Alias = task.Alias,
            Milestone = null,
            CreatedAt = task.CreatedAt,
            Status = (BO.Status)setStatus(task),
            Dependencies = new List<BO.TaskInList>() { },
            Start = task.Start,
            ScheduledDate = task.ScheduledDate,
            ForecastDate = task.ForecastDate,
            Deadline = task.Deadline,
            Complete = task.Complete,
            Deliverables = task.Deliverables,
            Remarks = task.Remarks,
            Engineer = new BO.EngineerInTask() { Id = 0, Name = "" },
            ComplexityLevel = (BO.EngineerExperience?)task.ComplexityLevel,
        };
        int engineerId = task.EngineerId ?? 0;
        if (engineerId > 0)
        {
            DO.Engineer? engineer = _dal.Engineer.Read(engineerId);
            if (engineer != null)
            {
                BO.EngineerInTask engineerInTask = new BO.EngineerInTask { Id = engineer.Id, Name = engineer.Name };
                newBoTask.Engineer = engineerInTask;
            }
        }
        return newBoTask;
    }

    /// <summary>
    /// read task by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>BO.Task</returns>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public BO.Task? Read(int id)
    {
        try
        {
            DO.Task dalTask = _dal.Task.Read(id) ?? throw new BO.BlNullPropertyException($"Task was not found");
            BO.Task blTask = ConvertDoTaskToBoTask(dalTask);
            return blTask;
        }
        catch (Exception ex)
        {
            throw new BO.BlDoesNotExistException($"Task was not found", ex);
        }
    }

    /// <summary>
    /// read all tasks, passible by filter
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<BO.Task> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        //read all DO.Tasks  
        IEnumerable<DO.Task> allDoTasks = _dal.Task.ReadAll(filter)!;
        //convet each one to BO.Task by BO.Read function
        IEnumerable<BO.Task> tasks = from task in allDoTasks
                                     select Read(task.Id);
        return tasks;
    }

    /// <summary>
    /// Convert Bo.Task To Do.Task
    /// </summary>
    /// <param name="task"></param>
    /// <returns>DO.Task from BO.Task</returns>
    private DO.Task ConvertBoTaskToDoTask(BO.Task task)
    {
        DO.Task newDoTask = new DO.Task(
            task.Id,
            task.Description,
            task.Alias,
            task.Milestone is null ? false : true,
            task.CreatedAt,
            task.Start,
            task.ScheduledDate,
            task.ForecastDate,
            task.Deadline,
            task.Complete,
            task.Deliverables!,
            task.Remarks,
            task.Engineer?.Id,
            (DO.EngineerExperience?)task.ComplexityLevel
          );
        return newDoTask;
    }

    /// <summary>
    /// update task properties
    /// </summary>
    /// <param name="task"></param>
    /// <exception cref="BO.BlNullPropertyException"></exception>
    /// <exception cref="BO.BlInvalidInputException"></exception>
    /// <exception cref="BO.BlAlreadyExistException"></exception>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public void Update(BO.Task task)
    {
        //validation tests
        if (task.Description is null || task.Description == "" || task.Alias is null || task.Alias == "")
            throw new BO.BlNullPropertyException("description or alias was not valid");
        if (task.Start < task.CreatedAt || task.ScheduledDate < task.Start || task.ForecastDate > task.Deadline || task.Complete < task.ScheduledDate)
            throw new BO.BlInvalidInputException("not valid dates");


        try
        {
            //check if user entered an engineer to task
            if (task.Engineer is not null && task.Engineer.Id != 0)
            {
                //check if wanted engineer exists
                if (_dal.Engineer.Read(task.Engineer.Id) == null)
                {
                    throw new BO.BlDoesNotExistException("Engineer was not found");
                }
                //check if a diffrent task with the same engineer already exists
                IEnumerable<DO.Task> allTasks = _dal.Task.ReadAll()!;
                DO.Task? doTask = (from newTask in allTasks
                                      where newTask.EngineerId == task.Engineer.Id
                                      select newTask).FirstOrDefault();
                if (doTask != null && doTask.Id != task.Id)
                {
                    throw new BO.BlAlreadyExistException("Engineer is busy");
                }  
            }
            DO.Task newDoTask = ConvertBoTaskToDoTask(task);
            _dal.Task.Update(newDoTask);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with Id={task.Id} was not found", ex);
        }
        catch (BO.BlDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException("Engineer was not found", ex);
        }
    }
}
