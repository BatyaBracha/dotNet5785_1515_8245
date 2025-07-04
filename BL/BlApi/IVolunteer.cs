
namespace BlApi;

public interface IVolunteer : IObservable //stage 5 הרחבת ממשק
{
    public BO.Role Login(int id, string password);
    void Create(BO.Volunteer boVolunteer);
    BO.Volunteer? Read(int id);
    IEnumerable<BO.VolunteerInList> ReadAll(BO.Active? filter = null, BO.VolunteerFields? sort = null, BO.TypeOfCall? typeOfCallFilter = null);
    void Update(int userId,BO.Volunteer boVolunteer);
    void Delete(int id);
    int GetVolunteersCount();

    //void MatchVolunteerToCall(int volunteerId, int callId);
    //void UnMatchVolunteerToCall(int volunteerId, int callId);

    //IEnumerable<BO.VolunteerInList> GetAssignedCallsForVolunteer(int volunteerId, BO.Year year = BO.Year.None);
    //IEnumerable<BO.VolunteerInList> GetUnRegisteredCoursesForStudent(int studentId, BO.Year year = BO.Year.None);

    //BO.StudentGradeSheet GetGradeSheetPerStudent(int studentId, BO.Year year = BO.Year.None);
    //void UpdateGrade(int studentId, int courseId, double grade);
}
