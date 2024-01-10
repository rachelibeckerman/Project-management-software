namespace BO;
/// <summary>
/// MilestoneInList entity defenition
/// </summary>
internal class MilestoneInList
{
    public int Id { get; init; }
    public required string Description { get; set; }
    public required string Alias { get; set; }
    public DateTime? CreateAt { get; set; }
    public Status Status { get; set; }
    public double? CompletionPercentage { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);
}
