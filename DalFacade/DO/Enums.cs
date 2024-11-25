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



