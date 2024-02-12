namespace BO;
/// <summary>
/// EngineerInList entity defenition
/// </summary>
public class EngineerInList
{
    public int Id { get; init; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public EngineerExperience Level { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);
}
