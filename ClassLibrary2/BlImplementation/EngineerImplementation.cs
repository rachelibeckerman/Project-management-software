namespace BlImplementation;
using BlApi;
using BO;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Engineer engineer)
    {
        if (engineer.Name is null || engineer.Name == "" || engineer.Email is null || engineer.Email == "")
            throw new BO.BlNullPropertyException("missing name or email");
        if (engineer.Id <= 0 || engineer.Cost <= 0)
            throw new BO.BlInvalidInputException("negative id or cost");

        DO.Engineer newDoEngineer = new DO.Engineer(engineer.Id, engineer.Name!, engineer.Email!, (DO.EngineerExperience)engineer.Level, engineer.Cost);
        try
        {
            _dal.Engineer.Create(newDoEngineer);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistException($"Engineer with ID={engineer.Id} already exists", ex);
        }
        if (engineer.Task is not null)
            try
            {
                _dal.Task.Update(_dal.Task.Read(engineer.Task.Id)! with { EngineerId = engineer.Id });
            }
            catch (DO.DalDoesNotExistException ex) { throw new BO.BlDoesNotExistException($"Task with ID={engineer.Id} was not found", ex); }

        return engineer.Id;
    }

    public void delete(int id)
    {
        try
        {           
            bool EngineerHasTask = false;
            IEnumerable<DO.Task> allTasks = _dal.Task.ReadAll()!;
            allTasks.FirstOrDefault(task => task.EngineerId == id ? EngineerHasTask = true : EngineerHasTask);
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

    public BO.Engineer? Read(int id)
    {
            DO.Engineer? dalEngineer = _dal.Engineer.Read(id);
            if(dalEngineer == null)
                throw new BO.BlDoesNotExistException($"Engineer with ID={id} does not exist");
            BO.Engineer blEngineer = new BO.Engineer 
            {
                Id = id,
                Name = dalEngineer.Name,
                Email = dalEngineer.Email,
                Level = (BO.EngineerExperience)dalEngineer.Level,
                Cost = dalEngineer.Cost,
                Task = new TaskInEngineer() { Id = 0, Alias = "" }
            };

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

    public IEnumerable<BO.Engineer?> ReadAll(Func<DO.Engineer, bool>? filter = null)
    {
        IEnumerable<DO.Engineer> dalEngineers = _dal.Engineer.ReadAll(filter)!;
        IEnumerable<BO.Engineer> blEngineers = from doEngineer in dalEngineers
                                               select Read(doEngineer.Id);
        return blEngineers;
    }


    public void Update(BO.Engineer engineer)
    {
        try
        {
            if (engineer.Name is null || engineer.Name == "" || engineer.Email is null || engineer.Email == "")
                throw new BO.BlNullPropertyException("missing name or email");
            if (engineer.Id <= 0 || engineer.Cost <= 0)
                throw new BO.BlInvalidInputException("negative id or cost");
            if (engineer.Task is not null)
                try
                {
                    _dal.Task.Update(_dal.Task.Read(engineer.Task.Id)! with { EngineerId = engineer.Id });
                }
                catch (DO.DalDoesNotExistException ex) { throw new BO.BlDoesNotExistException($"Task with ID={engineer.Id} was not found", ex); }

            DO.Engineer newDoEngineer = new DO.Engineer(engineer.Id, engineer.Name!,engineer.Email!, (DO.EngineerExperience)engineer.Level, engineer.Cost);
            _dal.Engineer.Update(newDoEngineer);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={engineer.Id} does not exists", ex);
        }
    }
}
