namespace BlImplementation;
using BlApi;
using BO;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Xml.Linq;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public int Create(BO.Task task)
    {
        if (task.Id <= 0 || task.Description is null || task.Description == "")
            throw new BO.BlNullPropertyException("Id or description was not valid");
        if (task.CreatedAt > DateTime.Now || task.Deadline < task.ForecastDate)
            throw new BO.BlInvalidInputException("not valid dates");

        DO.Task doTask = ConvertBoTaskToDoTask(task);
        try
        {
            _dal.Task.Create(doTask);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistException($"Task with ID={task.Id} already exists", ex);
        }
        return doTask.Id;
    }
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
            Engineer = null,
            Copmlexity = (BO.EngineerExperience?)task.ComplexityLevel,
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
    public BO.Task? Read(int id)
    {
        try
        {
            DO.Task dalTask = _dal.Task.Read(id) ?? throw new BO.BlNullPropertyException($"Task with Id={id} was not found");
            BO.Task blTask = ConvertDoTaskToBoTask(dalTask);
            return blTask;
        }
        catch (Exception ex)
        {
            throw new BO.BlDoesNotExistException($"Task with Id={id} was not found", ex);
        }
    }

    public IEnumerable<System.Threading.Tasks.Task> ReadAll()
    {
        throw new NotImplementedException();
    }
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
            (DO.EngineerExperience?)task.Copmlexity
          );
        return newDoTask;
    }
    public void Update(BO.Task task)
    {
        if (task.Id <= 0 || task.Description is null || task.Description == "")
            throw new BO.BlNullPropertyException("Id or description was not valid");
        if (task.CreatedAt > DateTime.Now || task.Deadline < task.ForecastDate)
            throw new BO.BlInvalidInputException("not valid dates");
        try
        {
            DO.Task newDoTask = ConvertBoTaskToDoTask(task);
            _dal.Task.Update(newDoTask);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with Id={task.Id} was not found", ex);
        }
    }
}
