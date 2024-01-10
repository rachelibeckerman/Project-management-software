namespace BO;
/// <summary>
/// MilestoneInTask entity defenition
/// </summary>
public class MilestoneInTask
{
    public int Id { get; init; }
    public required string Alias { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);
}
