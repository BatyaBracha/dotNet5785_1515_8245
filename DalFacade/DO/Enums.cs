namespace DO;

public enum TypeOfCall { };

public enum TypeOfTreatmentEnding {HOSPITAL_ADMISSION, STAY_AT_HOME,DEAD };

public enum TypeOfDistance {INTRACITY, INTRECITY, INTERNATIONAL };

public enum Role {ADMINISTRATOR,CO,STANDARD};

public enum Options
{
    EXIT,
    VOLUNTEER_MENU,
    ASSIGNMENT_MENU,
    CALL_MENU,
    INITIALIZE,
    PRINT_DATA,
    CONFIG_MENU,
    RESET_DB_CONFIG
}

public enum SpecificOptions
{
    EXIT,
    CREATE,
    READ,
    READ_ALL,
    UPDATE,
    DELETE,
    DELETE_ALL
}
public enum configOptions
{
    EXIT,
    ADVANCE_SYSTEM_CLOCK_BY_MINUTE,
    ADVANCE_SYSTEM_CLOCK_BY_HOUR,
    ADVANCE_SYSTEM_CLOCK_BY_DAY,
    ADVANCE_SYSTEM_CLOCK_BY_YEAR,
    DISPLAY_CURRENT_TIME,
    CHANGE_VALUE,
    DISPLAY_CURRENT_VALUE,
    RESET_CONFIG
}



