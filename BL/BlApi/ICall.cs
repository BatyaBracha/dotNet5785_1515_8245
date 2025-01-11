
namespace BlApi;

public interface ICall
{
    void Create(BO.Call boCall);
    BO.Call? Read(int id);
    public int[] GetCallQuantitiesByStatus(BO.CallInList callInList);
    IEnumerable<BO.CallInList> ReadAll(BO.CallFieldFilter? sort = null, BO.CallFieldFilter? filter = null, object? value = null);
    void Update(BO.Call boCall);
    void Delete(int id);

    void RegisterStudentToCourse(int studentId, int courseId);
    void UnRegisterStudentFromCourse(int studentId, int courseId);

    IEnumerable<BO.CallInList> GetRegisteredCoursesForStudent(int studentId, BO.Year year = BO.Year.None);
    IEnumerable<BO.CallInList> GetUnRegisteredCoursesForStudent(int studentId, BO.Year year = BO.Year.None);

    BO.StudentGradeSheet GetGradeSheetPerStudent(int studentId, BO.Year year = BO.Year.None);
    void UpdateGrade(int studentId, int courseId, double grade);

}
