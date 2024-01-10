namespace BlApi;

public interface ITask
{
    public IEnumerable<Task> ReadAll();
    public BO.Task? Read(int id);
    public int Create(BO.Task task);
    public void Update(BO.Task task);
    public void Delete(int id);
}
