namespace BlImplementation;
using BlApi;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

internal class EngineerInListImplementation : IEngineerInList
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    /// <summary>
    /// read a single engineerInList
    /// </summary>
    /// <param name="id"></param>
    /// <returns>BO.EngineerInList? </returns>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public BO.EngineerInList? Read(int id)
    {
            DO.Engineer? dalEngineer = _dal.Engineer.Read(id);
            if(dalEngineer == null)
                throw new BO.BlDoesNotExistException($"Engineer with ID={id} does not exist");
            BO.EngineerInList blEngineer = new BO.EngineerInList 
            {
                Id = id,
                Name = dalEngineer.Name,
                Email = dalEngineer.Email,
                Level = (BO.EngineerExperience)dalEngineer.Level
            };
            return blEngineer;
    }
    /// <summary>
    /// read all engineerInList
    /// </summary>
    /// <param name="filter"></param>
    /// <returns>m IEnumerable<BO.EngineerInList> </returns>
    public IEnumerable<BO.EngineerInList?> ReadAll(Func<DO.Engineer, bool>? filter = null)
    {
        IEnumerable<DO.Engineer> dalEngineers = _dal.Engineer.ReadAll(filter)!;
        IEnumerable<BO.EngineerInList> blEngineers = from doEngineer in dalEngineers
                                               select Read(doEngineer.Id);
        return blEngineers;
    }


}
