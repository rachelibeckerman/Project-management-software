namespace BlImplementation;
using BlApi;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    /// <summary>
    /// create BO.Engineer entity 
    /// </summary>
    /// <param name="engineer"></param>
    /// <returns> new engineer id</returns>
    /// <exception cref="BO.BlNullPropertyException"></exception>
    /// <exception cref="BO.BlInvalidInputException"></exception>
    /// <exception cref="BO.BlAlreadyExistException"></exception>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public int Create(BO.Engineer engineer)
    {
        //validation tests
        if (engineer.Name is null || engineer.Name == "" || engineer.Email is null || engineer.Email == "")
            throw new BO.BlNullPropertyException("missing name or email");
        if (engineer.Id <= 0 || engineer.Cost <= 0)
            throw new BO.BlInvalidInputException("negative id or cost");

        //create localy DO.engineer object
        DO.Engineer newDoEngineer = new DO.Engineer(engineer.Id, engineer.Name!, engineer.Email!, (DO.EngineerExperience)engineer.Level, engineer.Cost);

        try
        {
            //check if the user enter a task for engineer
            if (engineer.Task != null && engineer.Task.Id != 0)
            {
                //read all tasks 
                IEnumerable<DO.Task> allTasks = _dal.Task.ReadAll()!;
                //check if any task has the same engineer 
                DO.Task? newDoTask = (from task in allTasks
                                      where task.EngineerId == engineer.Id
                                      select task).FirstOrDefault();
                //if exist a diffrent task with the same engineer, updates its engineer to null
                if (newDoTask != null && newDoTask.Id != engineer.Task!.Id)
                {
                    _dal.Task.Update(newDoTask! with { EngineerId = null });
                }
                _dal.Task.Update(_dal.Task.Read(engineer.Task.Id)! with { EngineerId = engineer.Id });//update task with current engineer
            }
            _dal.Engineer.Create(newDoEngineer);//create a new DO.engineer
        }
        catch (DO.DalAlreadyExistsException ex) { throw new BO.BlAlreadyExistException($"Engineer with ID={engineer.Id} already exists", ex); }
        catch (DO.DalDoesNotExistException ex) { throw new BO.BlDoesNotExistException($"Task with ID={engineer.Id} was not found", ex); }
        return engineer.Id;
    }

    /// <summary>
    /// delete Engineer entity
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="BO.BlDeletionImpossibleException"></exception>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public void delete(int id)
    {
        try
        {
            //check if engineer has a task
            bool EngineerHasTask = false;
            IEnumerable<DO.Task> allTasks = _dal.Task.ReadAll()!;
            allTasks.FirstOrDefault(task => task.EngineerId == id ? EngineerHasTask = true : EngineerHasTask);

            //if engineer does not have a task , delete DO.Engineer
            if (!EngineerHasTask)
                _dal.Engineer.Delete(id);
            else
                throw new BO.BlDeletionImpossibleException($"Engineer with ID={id} can not be deleted");
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} was not found", ex);
        }
    }

    /// <summary>
    /// read Engineer entity by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>BO.Engineer </returns>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public BO.Engineer? Read(int id)
    {
        //read DO.Engineer by id
        DO.Engineer? dalEngineer = _dal.Engineer.Read(id);
        if (dalEngineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does not exist");

        //convert DO.Engineer to BO.Engineer
        BO.Engineer blEngineer = new BO.Engineer
        {
            Id = id,
            Name = dalEngineer.Name,
            Email = dalEngineer.Email,
            Level = (BO.EngineerExperience)dalEngineer.Level,
            Cost = dalEngineer.Cost,
            Task = new BO.TaskInEngineer() { Id = 0, Alias = "" }
        };

        //update the BO.Engineer with his task if exists
        IEnumerable<DO.Task> allTasks = _dal.Task.ReadAll()!;
        DO.Task? engineersTask = (from task in allTasks
                                  where task.EngineerId == id
                                  select task).FirstOrDefault();
        if (engineersTask != null)
        {
            BO.TaskInEngineer taskEngineer = new BO.TaskInEngineer { Id = engineersTask.Id, Alias = engineersTask.Alias };
            blEngineer.Task = taskEngineer;
        }
        return blEngineer;
    }

    /// <summary>
    /// read all BO.Engineer entities , passible with filter
    /// </summary>
    /// <param name="filter"></param>
    /// <returns>IEnumerable<BO.Engineer> </returns>
    public IEnumerable<BO.Engineer?> ReadAll(Func<DO.Engineer, bool>? filter = null)
    {
        //read all DO.engineer
        IEnumerable<DO.Engineer> dalEngineers = _dal.Engineer.ReadAll(filter)!;
        //convert each one to BO.Engineer, by BO.Read function
        IEnumerable<BO.Engineer> blEngineers = from doEngineer in dalEngineers
                                               select Read(doEngineer.Id);
        return blEngineers;
    }

    /// <summary>
    /// update Engineer properties
    /// </summary>
    /// <param name="engineer"></param>
    /// <exception cref="BO.BlNullPropertyException"></exception>
    /// <exception cref="BO.BlInvalidInputException"></exception>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public void Update(BO.Engineer engineer)
    {
        try
        {
            //validation tests
            if (engineer.Name is null || engineer.Name == "" || engineer.Email is null || engineer.Email == "")
                throw new BO.BlNullPropertyException("missing name or email");
            if (engineer.Id <= 0 || engineer.Cost <= 0)
                throw new BO.BlInvalidInputException("negative id or cost");

            //check if engineer has a task
            if (engineer.Task is not null && engineer.Task.Id != 0)
                try
                {
                    //check if task exist
                    DO.Task? task = _dal.Task.Read(engineer.Task.Id);
                    if (task == null)
                    {
                        throw new BO.BlDoesNotExistException($"Task was not found");
                    }

                    //if exist a diffrent task with the same engineer, updates its engineer to null
                    IEnumerable<DO.Task> allTasks = _dal.Task.ReadAll()!;
                    DO.Task? newDoTask = (from newTask in allTasks
                                          where newTask.EngineerId == engineer.Id
                                          select newTask).FirstOrDefault();
                    if (newDoTask != null && newDoTask.Id != engineer.Task.Id)
                    {
                        _dal.Task.Update(_dal.Task.Read(newDoTask!.Id)! with { EngineerId = null });
                    }
                    _dal.Task.Update(task! with { EngineerId = engineer.Id });//update task with current engineer
                }
                catch (DO.DalDoesNotExistException ex) { throw new BO.BlDoesNotExistException($"Task with ID={engineer.Id} was not found", ex); }

            //create and update DO.Engineer with updated properties
            DO.Engineer newDoEngineer = new DO.Engineer(engineer.Id, engineer.Name!, engineer.Email!, (DO.EngineerExperience)engineer.Level, engineer.Cost);
            _dal.Engineer.Update(newDoEngineer);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={engineer.Id} does not exists", ex);
        }
    }
}
