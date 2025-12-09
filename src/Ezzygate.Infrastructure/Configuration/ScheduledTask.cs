namespace Ezzygate.Infrastructure.Configuration;

public class ScheduledTask
{
    public string GroupId { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Tag { get; set; } = string.Empty;
    public string AssemblyName { get; set; } = string.Empty;
    public string TypeName { get; set; } = string.Empty;
    public string MethodName { get; set; } = string.Empty;
    public string Args { get; set; } = string.Empty;
    public ScheduleInfo Schedule { get; set; } = new();
}

public class ScheduleInfo
{
    public string Interval { get; set; } = string.Empty; // Minute, Hour, Day, Week, Month
    public int Every { get; set; } = 1;
    public int? Hour { get; set; }
    public int? Minute { get; set; }
    public string? DayOfWeek { get; set; } // For weekly schedules
    public int? DayOfMonth { get; set; } // For monthly schedules
}
