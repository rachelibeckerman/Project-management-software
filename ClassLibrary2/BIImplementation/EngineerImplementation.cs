namespace BIImplementation;
using BIApi;
using System.Collections.Generic;

internal class EngineerImplementation : BO.IEngineer
{
    private DalApi.IDal _dal = Factory.Get;
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
        throw new NotImplementedException();
    }

    public IEnumerable<BO.Engineer> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Engineer engineer)
    {
        throw new NotImplementedException();
    }
}
