namespace BO;
/// <summary>
/// Task entity defenition
/// </summary>
public class Task
{
    public int Id { get; init; }
    public  required string Description { get; set; }
    public required string Alias { get; set; }
    public DateTime CreatedAt { get; set; }
    public BO.Status? Status { get; set; }
    public List<TaskInList>? Dependencies { get; set; }
    public BO.MilestoneInTask? Milestone {get; set;}
    public DateTime? Start { get; set; }
    public DateTime? ScheduledDate { get; set; }
    public DateTime? ForecastDate { get; set; }
    public DateTime? Deadline { get; set; }
    public DateTime? Complete { get; set; }
    public string? Deliverables { get; set; }
    public string? Remarks { get; set; }
    public BO.EngineerInTask? Engineer { get; set; }
    public BO.EngineerExperience? ComplexityLevel { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);

}
