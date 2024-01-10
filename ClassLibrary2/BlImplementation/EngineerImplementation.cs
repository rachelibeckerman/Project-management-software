namespace BlImplementation;
using BlApi;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public void Create(BO.Engineer engineer)
    {
        throw new NotImplementedException();
    }

    public void delete(int id)
    {
        throw new NotImplementedException();
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
                Task = null
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
        throw new NotImplementedException();
    }
}
