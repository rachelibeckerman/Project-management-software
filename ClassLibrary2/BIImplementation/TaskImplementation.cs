namespace BIImplementation;
using BIApi;
using System.Collections.Generic;

internal class TaskImplementation : BO.ITask
{
    private DalApi.IDal _dal = Factory.Get;
    public int Create(BO.Task task)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Task? Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<System.Threading.Tasks.Task> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Task task)
    {
        throw new NotImplementedException();
    }
}
