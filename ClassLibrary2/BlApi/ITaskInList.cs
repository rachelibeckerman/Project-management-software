namespace BlApi;

public interface ITaskInList
{
    public IEnumerable<BO.TaskInList?> ReadAll(Func<DO.Task, bool>? filter = null);
    public BO.TaskInList? Read(int id);
}
