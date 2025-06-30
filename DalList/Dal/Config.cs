using System.Runtime.CompilerServices;

namespace Dal;
/// <summary>
/// Configuration Entity
/// </summary>
/// <param name="NextCallId">an ID number for the next new call</param>
/// <param name="NextAssignmentId">an ID number for a new instance of the assignment entity between volunteer and call</param>
/// <param name="Clock">A system clock that is maintained separately from the actual computer clock</param>
/// <param name="RiskRange">Time range from which onwards reading is considered at risk</param>

internal static class Config
{
    internal const int startCallId = 1000;
    private static int nextCallId = startCallId;
    internal static int NextCallId { [MethodImpl(MethodImplOptions.Synchronized)] 
        get => nextCallId++; }

    internal const int startAssignmentId = 1000;
    private static int nextAssignmentId = startAssignmentId;
    internal static int NextAssignmentId { [MethodImpl(MethodImplOptions.Synchronized)] 
        get => nextAssignmentId++; }

    static TimeSpan RiskRange { [MethodImpl(MethodImplOptions.Synchronized)] get; [MethodImpl(MethodImplOptions.Synchronized)] set; }=new TimeSpan(0,10,0);
    //...
    internal static DateTime Clock { [MethodImpl(MethodImplOptions.Synchronized)] get; [MethodImpl(MethodImplOptions.Synchronized)] set; } = DateTime.Now;
    //...
    [MethodImpl(MethodImplOptions.Synchronized)]
    internal static void Reset()
    {
        nextCallId = startCallId;
        Clock = DateTime.Now;
    }
}

