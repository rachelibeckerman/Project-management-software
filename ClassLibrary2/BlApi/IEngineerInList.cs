namespace BlApi;

public interface IEngineerInList
{
    public IEnumerable<BO.EngineerInList?> ReadAll(Func<DO.Engineer, bool>? filter = null);

}
