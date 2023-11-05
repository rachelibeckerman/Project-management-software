namespace DO;
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
);
