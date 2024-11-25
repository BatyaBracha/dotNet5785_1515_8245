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
            string assignmentId = Console.ReadLine();
            s_dalAssignment.Read(int.Parse(assignmentId));
            Console.WriteLine("Enter the call id");
            string callId = Console.ReadLine();
            Console.WriteLine("Enter the volunteer's id");
            string volunteerId = Console.ReadLine();
            s_dalAssignment.Update(new(int.Parse(assignmentId), int.Parse(callId), int.Parse(volunteerId), s_dalConfig.Clock, null, null));
        }

        private void assignmentDelete()
        {
            Console.WriteLine("Enter assignment Id");
            string assignmentId = Console.ReadLine();
            s_dalAssignment.Delete(int.Parse(assignmentId));
        }

        private void assignmentDeleteAll()
        {
            s_dalAssignment.DeleteAll();
        }

        private void callMenu()
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
                            callCreate();
                            break;
                        case SpecificOptions.READ:
                            callRead();
                            break;
                        case SpecificOptions.READ_ALL:
                            callReadAll();
                            break;
                        case SpecificOptions.UPDATE:
                            callUpdate();
                            break;
                        case SpecificOptions.DELETE:
                            callDelete();
                            break;
                        case SpecificOptions.DELETE_ALL:
                            callDeleteAll();
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
        private void callCreate()
        {
            Console.WriteLine("Enter the call id");
            string callId = Console.ReadLine();
            Console.WriteLine("Enter type of call");
            string typeOfCall = Console.ReadLine();
            TypeOfCall convertedTypeOfCall = (TypeOfCall)Enum.Parse(typeof(TypeOfCall), convertedTypeOfCall);
            Console.WriteLine("Enter a discription");
            string discription = Console.ReadLine();
            Console.WriteLine("Enter the call address");
            string address = Console.ReadLine();
            s_dalCall.Create(new(0, convertedTypeOfCall, discription, address, null, null, s_dalConfig.Clock, null));
            //newId,item.TypeOfCall,item.Description,item.Address,item.latitude,item.longitude,item.OpeningTime,item.MaxClosingTime
        }
        private void callRead()
        {
            Console.WriteLine("Enter callId");
            string callId = Console.ReadLine();
            Call call = s_dalCall.Read(int.Parse(callId));
            Console.WriteLine(call);//לבדוק איך כותב למסך
        }
        private void callReadAll()
        {
            List<Call> callList = s_dalCall.ReadAll();
            foreach (var item in callList)
            {
                Console.WriteLine(item);
            }
        }
        private void callUpdate()
        {
            Console.WriteLine("Enter the call id");
            string callId = Console.ReadLine();
            s_dalAssignment.Read(int.Parse(callId));
            Console.WriteLine("Enter type of call");
            string typeOfCall = Console.ReadLine();
            TypeOfCall convertedTypeOfCall = (TypeOfCall)Enum.Parse(typeof(TypeOfCall), convertedTypeOfCall);
            Console.WriteLine("Enter a discription");
            string discription = Console.ReadLine();
            Console.WriteLine("Enter the call address");
            string address = Console.ReadLine();
            s_dalCall.Update(new(int.Parse(callId), convertedTypeOfCall, discription, address, null, null, s_dalConfig.Clock, null));
        }
        private void callDelete()
        {
            Console.WriteLine("Enter call Id");
            string callId = Console.ReadLine();
            s_dalAssignment.Delete(int.Parse(callId));
        }
        private void callDeleteAll()
        {
            s_dalCall.DeleteAll();
        }
        private void initialize()
        {
            Initialization.Do(s_dalAssignment, s_dalCall, s_dalConfig, s_dalVolunteer);
        }

        private void printAllData()
        {
            List<Volunteer> vList = s_dalVolunteer.ReadAll();
            foreach (var item in vList)
            {
                Console.WriteLine(item);
            }

            List<Assignment> aList = s_dalAssignment.ReadAll();
            foreach (var item in aList)
            {
                Console.WriteLine(item);
            }

            List<Call> cList = s_dalCall.ReadAll();
            foreach (var item in cList)
            {
                Console.WriteLine(item);
            }

        }
        private void configMenu()
        {
            try
            {
                configOptions choice = configOptions.EXIT;
                do
                {
                    choice = (configOptions)Enum.Parse(typeof(configOptions), Console.ReadLine());
                    switch (choice)
                    {
                        case configOptions.EXIT:
                            break;
                        case configOptions.ADVANCE_SYSTEM_CLOCK_BY_MINUTE:
                            advanceSystem("minute");
                            break;
                        case configOptions.ADVANCE_SYSTEM_CLOCK_BY_HOUR:
                            advanceSystem("hour");
                            break;
                        case configOptions.ADVANCE_SYSTEM_CLOCK_BY_DAY:
                            advanceSystem("day");
                            break;
                        case configOptions.ADVANCE_SYSTEM_CLOCK_BY_YEAR:
                            advanceSystem("year");

                            callCreate();
                            break;
                        case configOptions.DISPLAY_CURRENT_TIME:
                            callRead();
                            break;
                        case configOptions.CHANGE_VALUE:
                            callReadAll();
                            break;
                        case configOptions.DISPLAY_CURRENT_VALUE:
                            callUpdate();
                            break;
                        case configOptions.RESET_CONFIG:
                            callDelete();
                            break;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                } while (choice != configOptions.EXIT);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

        }
        private void advanceSystem(string amount)
        {
            switch (amount)
            {
                case "minute":
                    if (int.TryParse(amount, out int minutes))
                    {
                        s_dalConfig.Clock = s_dalConfig.Clock.AddMinutes(minutes);
                    }
                    break;
                case "hour":
                    if (int.TryParse(amount, out int hours))
                    {
                        s_dalConfig.Clock = s_dalConfig.Clock.AddHours(hours);
                    }
                    break;
                case "day":
                    if (int.TryParse(amount, out int days))
                    {
                        s_dalConfig.Clock = s_dalConfig.Clock.AddHours(days);
                    }
                    break;
                case "year":
                    if (int.TryParse(amount, out int years))
                    {
                        s_dalConfig.Clock = s_dalConfig.Clock.AddHours(years);
                    }
                    break;

            }
        }
            private void resetDbAndConfig() { }
        private void configMenu() { }
        private void resetDbAndConfig()
        {
            s_dalVolunteer.DeleteAll();
            s_dalAssignment.DeleteAll();
            s_dalCall.DeleteAll();
            s_dalConfig.Reset();
        }


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