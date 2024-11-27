namespace Dal;
internal static class Config
{
    internal const int startCallId = 1000;
    private static int nextCallId = startCallId;
    internal static int NextCallId { get => nextCallId++; }

    internal const int startAssignmentId = 1000;
    private static int nextAssignmentId = startAssignmentId;
    internal static int NextAssignmentId { get => nextAssignmentId++; }

    static TimeSpan RiskRange { get; set; }=new TimeSpan(8,0,0);
    //...
    internal static DateTime Clock { get; set; } = DateTime.Now;
    //...

    internal static void Reset()
    {
        nextCallId = startCallId;
        Clock = DateTime.Now;
    }
}

