namespace DO;
/// <summary>
/// Dependency entity definition
/// </summary>
/// <param name="Id"></param>
/// <param name="DependentTask"></param>
/// <param name="DependentOnTask"></param>
public record Dependency
(
    int Id,
    int? DependentTask,
    int? DependentOnTask
);
