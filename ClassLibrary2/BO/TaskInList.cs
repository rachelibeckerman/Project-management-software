namespace BO;
/// <summary>
/// TaskInList entity defenition
/// </summary>
public class TaskInList
{
    public int Id { get; init; }
    public required string Description { get; set; }
    public required string Alias { get; set; }
    public Status Status { get; set; }
    public BO.EngineerExperience? ComplexityLevel { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);
}
