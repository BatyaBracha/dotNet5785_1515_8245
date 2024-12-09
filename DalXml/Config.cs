namespace Dal;
internal static class Config
{
    internal const string s_data_config_xml = "data-config.xml";
    internal const string s_assignments_xml = "assignments.xml"; 
    internal const string s_calls_xml = "calls.xml";
    internal const string s_volunteers_xml = "volunteers.xml";

    //...	

    internal static int nextAssignmentId
    {
        get => XMLTools.GetAndIncreaseConfigIntVal(s_data_config_xml, "nextAssignmentId");
        private set => XMLTools.SetConfigIntVal(s_data_config_xml, "nextAssignmentId", value);
    }

    internal static int nextCallId
    {
        get => XMLTools.GetAndIncreaseConfigIntVal(s_data_config_xml, "nextCallId");
        private set => XMLTools.SetConfigIntVal(s_data_config_xml, "nextCallId", value);
    }


    //...
    //	
    internal static DateTime RiskRange
    {
        get => XMLTools.GetConfigDateVal(s_data_config_xml, "RiskRange");
        set => XMLTools.SetConfigDateVal(s_data_config_xml, "RiskRange", value);
    }

    internal static DateTime Clock
    {
        get => XMLTools.GetConfigDateVal(s_data_config_xml, "Clock");
        set => XMLTools.SetConfigDateVal(s_data_config_xml, "Clock", value);
    }

    internal static void Reset()
    {

        NextCourseId = 1000
            //...
        Clock = DateTime.Now;
        //...
    }
}
