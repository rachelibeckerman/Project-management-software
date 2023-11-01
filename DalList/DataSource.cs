namespace Dal;
using DO;

internal static class DataSource
{
    internal static List<Engineer>? Engineers = new ();
    internal static List<Task>? Tasks = new();
    internal static List<Dependency>? Dependencies = new();

    internal static class Config
    {
        internal const int startTaskId = 1;
        private static int nextTaskId = startTaskId;
        internal static int NextTaskId { get => nextTaskId++;  }

        internal const int startDependencyId = 1;
        private static int nextDependencyId = startDependencyId;
        internal static int NextDependencyId { get => nextDependencyId++; }
    }

}
