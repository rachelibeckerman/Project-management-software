namespace DO;
/// <summary>
/// Task entity definition
/// </summary>
/// <param name="Id"></param>
/// <param name="Description"></param>
/// <param name="Alias"></param>
/// <param name="Milestone"></param>
/// <param name="CreatedAt"></param>
/// <param name="Start"></param>
/// <param name="ScheduledDate"></param>
/// <param name="ForecastDate"></param>
/// <param name="Deadline"></param>
/// <param name="Complete"></param>
/// <param name="Deliverables"></param>
/// <param name="Remarks"></param>
/// <param name="EngineerId"></param>
/// <param name="ComplexityLevel"></param>
public record Task
(
    int Id,
    string Description,
    string Alias,
    bool Milestone,
    DateTime CreatedAt,
    DateTime? Start,
    DateTime? ScheduledDate,
    DateTime? ForecastDate,
    DateTime? Deadline,
    DateTime? Complete,
    string? Deliverables,
    string? Remarks,
    int? EngineerId,
    EngineerExperience? ComplexityLevel
)
{
    public Task() : this(0, "", "", false, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, "","",0,0) { } //empty ctor for stage 3
}
