namespace BIApi;

public interface IMilestone
{
    public void create();
    public BO.Milestone? Read(int id);
    public BO.Milestone update(int id);
}
