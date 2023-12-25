namespace BO;

public class Task
{
    public int Id { get; init; }
    public string Description { get; set; }
    public string Alias { get; set; }
    public DateTime CreatedAt { get; set; }
    public  BO.Status? Status { get; set;}

    //int Id,
    //string Description,
    //string Alias,
    //bool Milestone,
    //DateTime CreatedAt,
    //DateTime? Start,
    //DateTime? ScheduledDate,
    //DateTime? ForecastDate,
    //DateTime? Deadline,
    //DateTime? Complete,
    //string? Deliverables,
    //string? Remarks,
    //int? EngineerId,
    //EngineerExperience? ComplexityLevel
}
