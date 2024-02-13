namespace BlApi;

public interface ITaskInEngineer { 
    public IEnumerable<BO.TaskInEngineer?> ReadAll(Func<DO.Task, bool>? filter = null);
    public BO.TaskInEngineer? Read(int id);
}
