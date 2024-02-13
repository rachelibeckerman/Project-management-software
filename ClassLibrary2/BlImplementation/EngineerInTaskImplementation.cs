namespace BlImplementation;
using BlApi;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

internal class EngineerInTaskImplementation : IEngineerInTask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;


    public BO.EngineerInTask? Read(int id)
    {
            DO.Engineer? dalEngineer = _dal.Engineer.Read(id);
            if(dalEngineer == null)
                throw new BO.BlDoesNotExistException($"Engineer with ID={id} does not exist");
            BO.EngineerInTask blEngineer = new BO.EngineerInTask
            {
                Id = id,
                Name = dalEngineer.Name,
            };
            return blEngineer;
    }

    public IEnumerable<BO.EngineerInTask?> ReadAll(Func<DO.Engineer, bool>? filter = null)
    {
        IEnumerable<DO.Engineer> dalEngineers = _dal.Engineer.ReadAll(filter)!;
        IEnumerable<BO.EngineerInTask> blEngineers = from doEngineer in dalEngineers
                                               select Read(doEngineer.Id);
        return blEngineers;
    }


}
