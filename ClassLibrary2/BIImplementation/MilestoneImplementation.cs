namespace BIImplementation;
using BIApi;

internal class MilestoneImplementation : BO.IMilestone
{
    private DalApi.IDal _dal = Factory.Get;
    public void create()
    {
        throw new NotImplementedException();
    }

    public BO.Milestone? Read(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Milestone update(int id)
    {
        throw new NotImplementedException();
    }
}
