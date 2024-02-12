namespace BlImplementation;
using BlApi;
using BO;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public int Create(BO.Task task)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
    //private BO.Task ConvertFromDOtoBO(DO.Task task)
    //{
    //    ///Creating a BO object
    //    BO.Task boTask = new BO.Task
    //    {
    //        Id = task.Id,
    //        Description = task.Description,
    //        Alias = task.Alias,
    //        Milestone = null,
    //        CreatedAt = task.CreatedAt,
    //        Status = (BO.Status)setStatus(task.Id),
    //        Start = task.Start,
    //        Dependencies = new List<BO.TaskInList>() { },
    //        ForecastDate = task.ForecastDate,
    //        Deadline = task.Deadline,
    //        Complete = task.Complete,
    //        Deliverables = task.Deliverables,
    //        Remarks = task.Remarks,
    //        EngineerId = task.EngineerId,
           
    //        Schedual = DateTime.Now,//?????
    //        ComplexityLevel = (BO.Level)task.ComplexityLevel,
            
    //    };

    //    /*\
   
    //public BO.Status? Status { get; set; }
    //public List<TaskInList>? Dependencies { get; set; }
    //public BO.MilestoneInTask? Milestone {get; set;}
    //public TimeSpan RequiredEffortTime { get; set; }
    //public DateTime? Start { get; set; }
    //public DateTime? ScheduledDate { get; set; }
    //public DateTime? ForecastDate { get; set; }
    //public DateTime? Deadline { get; set; }
    //public DateTime? Complete { get; set; }
    //public string? Deliverables { get; set; }
    //public string? Remarks { get; set; }
    //public BO.EngineerInTask? Engineer { get; set; }
    //public BO.EngineerExperience? Copmlexity { get; set; }
    ////public override string ToString() => Tools.ToStringProperty(this);*/
    ////    return boTask;
    //}
    public BO.Task? Read(int id)
    {
        try
        {
            DO.Task? dalTask = _dal.Task.Read(id);
            BO.Task blTask = ConvertFromDOtoBO(dalTask);
            return blTask;
        }
        catch (DO.DalDoesNotExistException ex)
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
        if (task.CreatedAt > DateTime.Now || task.Deadline < task.ForecastDate || task.RequiredEffortTime.Days < 0)
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
