using System.Runtime.CompilerServices;

namespace Dal;
/// <summary>
/// Provides configuration settings for the DAL XML layer.
/// </summary>
internal static class Config
{
    /// <summary>
    /// The name of the data configuration XML file.
    /// </summary>
    internal const string s_data_config_xml = "data-config.xml";
    /// <summary>
    /// The name of the assignments XML file.
    /// </summary>
    internal const string s_assignments_xml = "assignments.xml"; 
    /// <summary>
    /// The name of the calls XML file.
    /// </summary>
    internal const string s_calls_xml = "calls.xml";
    /// <summary>
    /// The name of the volunteers XML file.
    /// </summary>
    internal const string s_volunteers_xml = "volunteers.xml";

   
        /// <summary>
    /// Gets the next assignment ID and increments the stored value.
    /// </summary>
    internal static int NextAssignmentId
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        get => XMLTools.GetAndIncreaseConfigIntVal(s_data_config_xml, "nextAssignmentId");
        [MethodImpl(MethodImplOptions.Synchronized)]
        private set => XMLTools.SetConfigIntVal(s_data_config_xml, "nextAssignmentId", value);
    }

        /// <summary>
    /// Gets the next call ID and increments the stored value.
    /// </summary>
    internal static int NextCallId
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        get => XMLTools.GetAndIncreaseConfigIntVal(s_data_config_xml, "nextCallId");
        [MethodImpl(MethodImplOptions.Synchronized)]
        private set => XMLTools.SetConfigIntVal(s_data_config_xml, "nextCallId", value);
    }


        /// <summary>
    /// Gets or sets the risk range value.
    /// </summary>
    internal static TimeSpan RiskRange 
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        get => XMLTools.GetConfigTimeSpanVal(s_data_config_xml, "RiskRange");
        [MethodImpl(MethodImplOptions.Synchronized)]
        set => XMLTools.SetConfigTimeSpanVal(s_data_config_xml, "RiskRange", value);
    }

        /// <summary>
    /// Gets or sets the current clock value.
    /// </summary>
    internal static DateTime Clock
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        get => XMLTools.GetConfigDateVal(s_data_config_xml, "Clock");
        [MethodImpl(MethodImplOptions.Synchronized)]
        set => XMLTools.SetConfigDateVal(s_data_config_xml, "Clock", value);
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
        /// <summary>
    /// Resets the configuration values to their defaults.
    /// </summary>
    internal static void Reset()
    {

        NextCallId = 1000;
        //...
        Clock = DateTime.Now;
        //...
    }
}
