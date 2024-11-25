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

        private void volunteerMenu()
        {
            try
            {
                SpecificOptions choice = SpecificOptions.EXIT;
                do
                {
                    choice = (SpecificOptions)Enum.Parse(typeof(SpecificOptions), Console.ReadLine());
                    switch (choice)
                    {
                        case SpecificOptions.EXIT:
                            break;
                        case SpecificOptions.CREATE:
                            volunteerCreate();
                            break;
                        case SpecificOptions.READ:
                            volunteerRead();
                            break;
                        case SpecificOptions.READ_ALL:
                            volunteerReadAll();
                            break;
                        case SpecificOptions.UPDATE:
                            volunteerUpdate();
                            break;
                        case SpecificOptions.DELETE:
                            volunteerDelete();
                            break;
                        case SpecificOptions.DELETE_ALL:
                            volunteerDeleteAll();
                            break;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                } while (choice != SpecificOptions.EXIT);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private void volunteerUpdate()
        {
            Console.WriteLine("Enter your ID");
            string id = Console.ReadLine();
            Console.WriteLine("Enter your name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter your phone");
            string phone = Console.ReadLine();
            Console.WriteLine("Enter your email");
            string email = Console.ReadLine();
            Console.WriteLine("Enter your address");
            string address = Console.ReadLine();
            Console.WriteLine("Enter your password");
            string password = Console.ReadLine();
            Console.WriteLine("Enter your max distance");
            string maxDistance = Console.ReadLine();
            Console.WriteLine("Enter your role");
            string role = Console.ReadLine();
            Console.WriteLine("Enter your type of distance");
            Role convertedRole = (Role)Enum.Parse(typeof(Role), role);
            string typeOfDistance = Console.ReadLine();
            TypeOfDistance convertedTypeOfDistance = (TypeOfDistance)Enum.Parse(typeof(TypeOfDistance), typeOfDistance);
            s_dalVolunteer.Update(new(int.Parse(id), name, phone, email, password, address, null, null, convertedRole, false, int.Parse(maxDistance), convertedTypeOfDistance));
        }

        private void volunteerReadAll()
        {
            List<Volunteer> volunteerList = s_dalVolunteer.ReadAll();
            foreach (var v in volunteerList)
            {
                Console.WriteLine(v);
            }
        }
        private void volunteerRead()
        {
            Console.WriteLine("Enter an ID");
            string id = Console.ReadLine();
            Volunteer volunteer = s_dalVolunteer.Read(int.Parse(id));
            Console.WriteLine(volunteer);//לבדוק איך כותב למסך
        }

        private void volunteerCreate()
        {
            Console.WriteLine("Enter your ID");
            string id = Console.ReadLine();
            Console.WriteLine("Enter your name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter your phone");
            string phone = Console.ReadLine();
            Console.WriteLine("Enter your email");
            string email = Console.ReadLine();
            Console.WriteLine("Enter your address");
            string address = Console.ReadLine();
            Console.WriteLine("Enter your password");
            string password = Console.ReadLine();
            Console.WriteLine("Enter your max distance");
            string maxDistance = Console.ReadLine();
            Console.WriteLine("Enter your role");
            string role = Console.ReadLine();
            Console.WriteLine("Enter your type of distance");
            Role convertedRole = (Role)Enum.Parse(typeof(Role), role);
            string typeOfDistance = Console.ReadLine();
            TypeOfDistance convertedTypeOfDistance = (TypeOfDistance)Enum.Parse(typeof(TypeOfDistance), typeOfDistance);
            s_dalVolunteer.Create(new(int.Parse(id), name, phone, email, password, address, null, null, convertedRole, false, int.Parse(maxDistance), convertedTypeOfDistance));
        }
        private void volunteerDelete()
        {
            Console.WriteLine("Enter your ID");
            string id = Console.ReadLine();
            s_dalVolunteer.Delete(int.Parse(id));
        }
        private void volunteerDeleteAll()
        {
            s_dalVolunteer.DeleteAll();
        }
        private void assignmentMenu()
        {
            try
            {
                SpecificOptions choice = SpecificOptions.EXIT;
                do
                {
                    choice = (SpecificOptions)Enum.Parse(typeof(SpecificOptions), Console.ReadLine());
                    switch (choice)
                    {
                        case SpecificOptions.EXIT:
                            break;
                        case SpecificOptions.CREATE:
                            assignmentCreate();
                            break;
                        case SpecificOptions.READ:
                            assignmentRead();
                            break;
                        case SpecificOptions.READ_ALL:
                            assignmentReadAll();
                            break;
                        case SpecificOptions.UPDATE:
                            assignmentUpdate();
                            break;
                        case SpecificOptions.DELETE:
                            assignmentDelete();
                            break;
                        case SpecificOptions.DELETE_ALL:
                            assignmentDeleteAll();
                            break;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                } while (choice != SpecificOptions.EXIT);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        private void assignmentCreate()
        {
            Console.WriteLine("Enter the call id");
            string callId = Console.ReadLine();
            Console.WriteLine("Enter the volunteer's id");
            string volunteerId = Console.ReadLine();
            s_dalAssignment.Create(new(0, int.Parse(callId), int.Parse(volunteerId), s_dalConfig.Clock, null, null));
            //newId, item.CallId, item.VolunteerId, item.TreatmentStartTime, item.TreatmentEndTime, item.TypeOfTreatmentEnding
        }

        private void assignmentRead()
        {
            Console.WriteLine("Enter an ID");
            string id = Console.ReadLine();
            Console.WriteLine(s_dalAssignment.Read(int.Parse(id)));
        }

        private void assignmentReadAll()
        {
            List<Assignment> assignmentList = s_dalAssignment.ReadAll();
            foreach (var item in assignmentList)
            {
                Console.WriteLine(item);
            }
        }

        private void assignmentUpdate()
        {
            Console.WriteLine("Enter assignment ID");
            string assignmentId=Console.ReadLine();
            s_dalAssignment.Read(int.Parse(assignmentId));
            Console.WriteLine("Enter the call id");
            string callId = Console.ReadLine();
            Console.WriteLine("Enter the volunteer's id");
            string volunteerId = Console.ReadLine();
            s_dalAssignment.Update(new(int.Parse(assignmentId), int.Parse(callId), int.Parse(volunteerId), s_dalConfig.Clock, null, null));
        }

        private void assignmentDelete()
        {

        }
        private void callMenu() { }
        private void initialize() { }
        private void printAllData() { }
        private void configMenu() { }
        private void resetDbAndConfig() { }


        public void Main(string[] args)
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
                            volunteerMenu();
                            break;
                        case Options.ASSIGNMENT_MENU:
                            assignmentMenu();
                            break;
                        case Options.CALL_MENU:
                            callMenu();
                            break;
                        case Options.INITIALIZE:
                            initialize();
                            break;
                        case Options.PRINT_DATA:
                            printAllData();
                            break;
                        case Options.CONFIG_MENU:
                            configMenu();
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