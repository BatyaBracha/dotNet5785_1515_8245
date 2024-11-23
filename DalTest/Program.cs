using DalApi;
using Dal;

namespace DalTest;

internal class Program
{

    private static IVolunteer? s_dalVolunteer = new VolunteerImplementation(); //stage 1
    private static IAssignment? s_dalAssignment = new AssignmentImplementation(); //stage 1
    private static ICall? s_dalCall = new CallImplementation(); //stage 1
    private static IConfig? s_dalConfig = new ConfigImplementation();//stage 1

    public static void Main(string[] args)
    {
        Initialization.Do( s_dalAssignment, s_dalCall, s_dalConfig, s_dalVolunteer); //stage 1
        Console.WriteLine("Enter your choice:");
        int choice = Console.Read();
        switch(choice) {    
            case 0:break;       
                case 1:Console.WriteLine();

    }
}
        










