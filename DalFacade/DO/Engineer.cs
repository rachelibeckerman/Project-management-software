namespace DO;
/// <summary>
/// Engineer entity definition
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="Email"></param>
/// <param name="Level"></param>
/// <param name="Cost"></param>
/// <param name="Status"></param>
public record Engineer
(
    int Id,
    string Name,
    string Email,
    EngineerExperience Level,
    double Cost,
    bool Status = true
)
{
    public Engineer() : this(0,"","",(EngineerExperience)0,0) { } //empty ctor for stage 3
}
