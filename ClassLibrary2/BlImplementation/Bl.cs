namespace BlImplementation;
using BlApi;
internal class Bl : IBl
{
    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public IMilestone Milestone =>  new MilestoneImplementation();

    public IEngineerInList EngineerInList => new EngineerInListImplementation();

    public ITaskInEngineer TaskInEngineer => new TaskInEngineerImplementation();

    public ITaskInList TaskInList => new TaskInListImplementation();
}
 