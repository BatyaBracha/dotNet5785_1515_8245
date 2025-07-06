namespace BlApi;

/// <summary>
/// Interface for volunteer-related operations in the BL layer.
/// </summary>
/// <summary>
/// Interface for volunteer-related operations in the business logic layer.
/// </summary>
public interface IVolunteer : IObservable //stage 5 הרחבת ממשק
{
    /// <summary>
    /// Logs in a volunteer and returns their role.
    /// </summary>
    /// <param name="id">The unique identifier of the volunteer.</param>
    /// <param name="password">The password of the volunteer.</param>
    /// <returns>The role of the volunteer.</returns>
    public BO.Role Login(int id, string password);

    /// <summary>
    /// Creates a new volunteer in the system.
    /// </summary>
    /// <param name="boVolunteer">The volunteer to create.</param>
    void Create(BO.Volunteer boVolunteer);

    /// <summary>
    /// Retrieves a volunteer by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the volunteer.</param>
    /// <returns>The volunteer with the specified identifier, or null if not found.</returns>
    BO.Volunteer? Read(int id);

    /// <summary>
    /// Retrieves all volunteers in the system, filtered by the specified criteria.
    /// </summary>
    /// <param name="filter">The filter to apply to the volunteers.</param>
    /// <param name="sort">The sort order to apply to the volunteers.</param>
    /// <param name="typeOfCallFilter">The type of call to filter by.</param>
    /// <returns>A collection of volunteers in list form.</returns>
    IEnumerable<BO.VolunteerInList> ReadAll(BO.Active? filter = null, BO.VolunteerFields? sort = null, BO.TypeOfCall? typeOfCallFilter = null);

    /// <summary>
    /// Updates a volunteer in the system.
    /// </summary>
    /// <param name="userId">The unique identifier of the volunteer to update.</param>
    /// <param name="boVolunteer">The updated volunteer.</param>
    void Update(int userId, BO.Volunteer boVolunteer);

    /// <summary>
    /// Deletes a volunteer from the system by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the volunteer to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Retrieves the number of volunteers in the system.
    /// </summary>
    /// <returns>The number of volunteers.</returns>
    int GetVolunteersCount();

    //void MatchVolunteerToCall(int volunteerId, int callId);
    //void UnMatchVolunteerToCall(int volunteerId, int callId);

    //IEnumerable<BO.VolunteerInList> GetAssignedCallsForVolunteer(int volunteerId, BO.Year year = BO.Year.None);
    //IEnumerable<BO.VolunteerInList> GetUnRegisteredCoursesForStudent(int studentId, BO.Year year = BO.Year.None);

    //BO.StudentGradeSheet GetGradeSheetPerStudent(int studentId, BO.Year year = BO.Year.None);
    //void UpdateGrade(int studentId, int courseId, double grade);
}
