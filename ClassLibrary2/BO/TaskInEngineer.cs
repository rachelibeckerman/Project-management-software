namespace BO;
/// <summary>
/// TaskInEngineer entity defenition
/// </summary>
public class TaskInEngineer
{
    public int Id { get; init; }
    public required string  Alias { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);
}
