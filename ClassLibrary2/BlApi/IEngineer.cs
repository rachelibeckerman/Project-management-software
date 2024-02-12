namespace BlApi;

public interface IEngineer
{
    public IEnumerable<BO.Engineer?> ReadAll(Func<DO.Engineer, bool>? filter = null);
    public BO.Engineer? Read(int id);
    public int Create(BO.Engineer engineer);
    public void  delete(int id);
    public void Update(BO.Engineer engineer);

}
