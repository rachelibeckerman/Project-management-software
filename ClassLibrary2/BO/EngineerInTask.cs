namespace BO;
/// <summary>
/// EngineerInTask entity defenition
/// </summary>
public class EngineerInTask
{
    public int Id { get; init; }
    public required string Name { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);
}
