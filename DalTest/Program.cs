using DalApi;
using Dal;

namespace DalTest;

internal class Program
{
    static void Main(string[] args)
    {
    private static IVolunteer? s_dalLink = new VolunteerImplementation(); //stage 1
    private static IAssignment? s_dalStudent = new AssignmentImplementation(); //stage 1
    private static ICall? s_dalCourse = new CallImplementation(); //stage 1
    private static IConfig? s_dalConfig = new ConfigImplementation(); //stage 1
}
}


