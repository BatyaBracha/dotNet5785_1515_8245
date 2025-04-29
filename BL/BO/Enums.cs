namespace BO;
public enum Role { ADMINISTRATOR, STANDARD };
public enum TypeOfDistance { AIR, WALK, DRIVE };
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
    HEAT_STROKE
};

public enum Status {WAITING, BEING_HANDELED, BEING_HANDELED_IN_RISK }
public enum CallStatus { OPEN, BEING_HANDELED, CLOSED, OUT_OF_DATE, OPEN_IN_RISK, BEING_HANDELED_IN_RISK }
public enum VolunteerFields {  None,CallsOutOfdate, CallsCanceled, CallsDone, Active, Name, Id }
public enum Active { TRUE, FALSE }
public enum AssignmentStatus { OPEN, BEING_HANDELED, CLOSED, OUT_OF_DATE, OPEN_IN_RISK, BEING_HANDELED_IN_RISK, COMPLETED }
public enum CallFieldFilter { Id, TypeOfCall, Description, Address, Latitude, Longitude, Openingtime, MaxClosingtime, StatusAssignedVolunteers }
public enum TimeUnit { YEAR,MONTH, DAY, HOUR, MINUTE }
public enum TypeOfTreatmentEnding { HOSPITAL_ADMISSION, STAY_AT_HOME, DEAD, EXPIRED, UNMATCHED };
public enum CallField { STATUS,PRIORITY,TYPE, ADDRESS, CALL_VOLUNTEER_DISTANCE,ID }
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
public enum VolunteerOptions
{
    EXIT,
    CREATE,
    READ,
    READ_ALL,
    UPDATE,
    DELETE,
}

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
