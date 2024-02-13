namespace BlApi;

public interface IEngineerInTask
{
    public IEnumerable<BO.EngineerInTask?> ReadAll(Func<DO.Engineer, bool>? filter = null);

}
