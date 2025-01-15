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

public enum Status { BEING_HANDELED, BEING_HANDELED_IN_RISK }
public enum CallStatus { OPEN, BEING_HANDELED, CLOSED, OUT_OF_DATE, OPEN_IN_RISK, BEING_HANDELED_IN_RISK }
public enum VolunteerFields { CallInProgress, CallsChosenOutOfdate, CallsDeleted, CallsDone, TypeOfDistance, MaxDistance, Active, Role, Longitude, Latitude, CurrentAddress, Password, Email, PhoneNumber, Name, Id }
public enum Active { TRUE, FALSE }
public enum CallFieldFilter { Id, TypeOfCall, Description, Address, Latitude, Longitude, Openingtime, MaxClosingtime, StatusAssignedVolunteers }
public enum TimeUnit { YEAR, DAY, HOUR, MINUTE }
public enum TypeOfTreatmentEnding { HOSPITAL_ADMISSION, STAY_AT_HOME, DEAD, EXPIRED };

