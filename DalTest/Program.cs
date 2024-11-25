using System;
using DalApi;
using Dal;
using DO;

namespace DalTest
{
    internal class Program
    {
        private static IVolunteer? s_dalVolunteer = new VolunteerImplementation();
        private static IAssignment? s_dalAssignment = new AssignmentImplementation();
        private static ICall? s_dalCall = new CallImplementation();
        private static IConfig? s_dalConfig = new ConfigImplementation();

        // Define an enum with specific values
        enum Options
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
        private void volunteerMenu() 
        { 

        }
        private void assignmentMenu() 
        {

        }
        private void callMenu() { }
        private void initialize() { }
        private void printAllData() { }
        private void configMenu() { }
        private void resetDbAndConfig() { }


        public static void Main(string[] args)
        {
            try
            {
                Options choice = Options.EXIT; // Set a default choice
                choice = (Options)Enum.Parse(typeof(Options), Console.ReadLine());
                //Initialization.Do(s_dalAssignment, s_dalCall, s_dalConfig, s_dalVolunteer);
                while (choice != Options.EXIT)
                {
                    Console.WriteLine("Enter your choice:");
                    choice = (Options)Enum.Parse(typeof(Options), Console.ReadLine());

                    switch (choice)
                    {
                        case Options.EXIT:
                            break;
                        case Options.VOLUNTEER_MENU:
                            volunteerMenue();
                            break;
                        case Options.ASSIGNMENT_MENU:
                            assignmentMenue();
                            break;
                        case Options.CALL_MENU:
                            callMenue();
                            break;
                        case Options.INITIALIZE:
                            initialize();
                            break;
                        case Options.PRINT_DATA:
                            printAllData();
                            break;
                        case Options.CONFIG_MENU:
                            configMenue();
                            break;
                        case Options.RESET_DB_CONFIG:
                            resetDbAndConfig();
                            break;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}