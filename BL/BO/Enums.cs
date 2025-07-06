namespace BO;
/// <summary>
/// Represents user roles in the system.
/// </summary>
public enum Role { ADMINISTRATOR, STANDARD };
/// <summary>
/// Represents types of distance calculation.
/// </summary>
public enum TypeOfDistance { AIR, WALK, DRIVE };
/// <summary>
/// Represents types of emergency calls.
/// </summary>
public enum TypeOfCall
{
    CARDIAC_ARREST,
    DIFFICULTY_BREATHING,
    CAR_ACCIDENT,
    STROKE,
    SEVERE_BLEEDING,
    UNCONSCIOUS_PERSON,
    BURN_INJURY,
    CHOKING,
    FALL_FROM_HEIGHT,
    DIABETIC_EMERGENCY,
    SEIZURE,
    ALLERGIC_REACTION,
    CHILDBIRTH,
    POISONING,
    ASSAULT_INJURY,
    DROWNING,
    ELECTRIC_SHOCK,
    HEAD_INJURY,
    BROKEN_BONE,
    HEAT_STROKE,
    NONE
};

/// <summary>
/// Represents the status of a call in progress.
/// </summary>
public enum Status {WAITING, BEING_HANDELED, BEING_HANDELED_IN_RISK }
/// <summary>
/// Represents the status of a call.
/// </summary>
/// <summary>
/// Represents the status of a call.
/// </summary>
public enum CallStatus { OPEN, BEING_HANDELED, CLOSED, OUT_OF_DATE, OPEN_IN_RISK, BEING_HANDELED_IN_RISK,NONE }
/// <summary>
/// Represents fields related to volunteers.
/// </summary>
public enum VolunteerFields {  None,CallsOutOfdate, CallsCanceled, CallsDone, Active, Name, Id }
/// <summary>
/// Represents the active status of an entity.
/// </summary>
public enum Active { TRUE, FALSE }
/// <summary>
/// Represents the status of an assignment.
/// </summary>
public enum AssignmentStatus { OPEN, BEING_HANDELED, CLOSED, OUT_OF_DATE, OPEN_IN_RISK, BEING_HANDELED_IN_RISK, COMPLETED }
/// <summary>
/// Represents fields for filtering calls.
/// </summary>
public enum CallFieldFilter { Id, TypeOfCall, Description, Address, Latitude, Longitude, Openingtime, MaxClosingtime, StatusAssignedVolunteers }
/// <summary>
/// Represents units of time for clock operations.
/// </summary>
public enum TimeUnit { YEAR,MONTH, DAY, HOUR, MINUTE }
/// <summary>
/// Represents the way a treatment ended.
/// </summary>
public enum TypeOfTreatmentEnding { HOSPITAL_ADMISSION, STAY_AT_HOME, DEAD, EXPIRED, UNMATCHED };
/// <summary>
/// Represents fields for a call in a list.
/// </summary>
public enum CallInListField { Status,TypeOfCall ,None}
//public enum CallInListField {TYPEOFCALL, STATUS }

/// <summary>
/// Represents available options in the application.
/// </summary>
public enum Options
{
    EXIT,
    VOLUNTEER_MENU,
    ASSIGNMENT_MENU,
    CALL_MENU,
    INITIALIZE,
    PRINT_DATA,
    CONFIG_MENU,
    RESET_DB_CONFIG,
    MANAGE_VOLUNTEERS,
    MANAGE_CALLS,
    CONFIGURE_SYSTEM,
    RESET_DATABASE,
    INITIALIZE_DATABASE
}

/// <summary>
/// Represents available administrative options.
/// </summary>
public enum AdminOptions
{
    EXIT,
    GET_CLOCK,
    PROMOTION_CLOCK,        
    GET_RISK_RANGE,
    SET_RISK_RANGE,
    RESET_DB,
    INITIALIZE_DB
}
/// <summary>
/// Represents available volunteer options.
/// </summary>
public enum VolunteerOptions
{
    EXIT,
    CREATE,
    READ,
    READ_ALL,
    UPDATE,
    DELETE,
}

/// <summary>
/// Represents specific options for operations.
/// </summary>
public enum SpecificOptions
{
    EXIT,
    CALLCOUNT,
    CREATE,
    READ,
    READ_ALL,
    UPDATE,
    DELETE,
    CLOSEDCALLS,
    OPENCALLS,
    UPDATECALLCANCELATION,
    UPDATEENDOFCALL,
    ASSIGN_VOLUNTEER_TO_CALL,
    UNMATCH_VOLUNTEER_FROM_CALL
}
