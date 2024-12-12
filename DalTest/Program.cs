using System;
using DalApi;
using Dal;
using DO;

namespace DalTest
{
    internal class Program
    {
        //static readonly IDal s_dal = new DalList();//stage 2
        static readonly IDal s_dal = new DalXml(); //stage 3
        private static void volunteerMenu()
        {
            try
            {
                SpecificOptions choice = SpecificOptions.EXIT;
                do
                {
                    Console.WriteLine("Enter your choice:\n" +
                        "to exit press 0\n" +
                        "to create a new volunteer press 1\n" +
                        "to read a volunteer's details press 2\n" +
                        "to read all volunteers' details press 3\n" +
                        "to update a volunteer's datails press 4\n" +
                        "to delete a volunteer press 5\n" +
                        "to delete all volunteers press 6");
                    choice = (SpecificOptions)Enum.Parse(typeof(SpecificOptions), Console.ReadLine()!);
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

        private static void volunteerUpdate()
        {
            Console.WriteLine("Enter your ID");
            string id = Console.ReadLine()!;
            if (s_dal.Volunteer!.Read(int.Parse(id)) != null)
            {
                Console.WriteLine("Enter your name");
                string name = Console.ReadLine()!;
                Console.WriteLine("Enter your phone");
                string phone = Console.ReadLine()!;
                Console.WriteLine("Enter your email");
                string email = Console.ReadLine()!;
                Console.WriteLine("Enter your address");
                string address = Console.ReadLine()!;
                Console.WriteLine("Enter your password");
                string password = Console.ReadLine()!;
                Console.WriteLine("Enter your max distance");
                string maxDistance = Console.ReadLine()!;
                //Console.WriteLine("Enter your role");
                //string role = Console.ReadLine()!;
                Console.WriteLine("Enter your type of distance");
                //Role convertedRole = (Role)Enum.Parse(typeof(Role), role);
                string typeOfDistance = Console.ReadLine()!;
                TypeOfDistance convertedTypeOfDistance = (TypeOfDistance)Enum.Parse(typeof(TypeOfDistance), typeOfDistance);
                s_dal.Volunteer!.Update(new(int.Parse(id), name, phone, email, password, address, null, null, Role.STANDARD, false, int.Parse(maxDistance), convertedTypeOfDistance));
            }
            else
                throw new DalDoesNotExistException("an obj with this id does not exist\n");
        }

        private static void volunteerReadAll()
        {
            IEnumerable<Volunteer> volunteerList = s_dal.Volunteer!.ReadAll();
            foreach (var v in volunteerList)
            {
                Console.WriteLine(v);
            }
        }

        private static void volunteerRead()
        {
            Console.WriteLine("Enter an ID");
            string id = Console.ReadLine()!;
            Volunteer volunteer = s_dal.Volunteer!.Read(int.Parse(id))!;
            Console.WriteLine(volunteer);//לבדוק איך כותב למסך
        }

        private static void volunteerCreate()
        {
            Console.WriteLine("Enter your ID");
            string id = Console.ReadLine()!;
            Console.WriteLine("Enter your name");
            string name = Console.ReadLine()!;
            Console.WriteLine("Enter your phone");
            string phone = Console.ReadLine()!;
            Console.WriteLine("Enter your email");
            string email = Console.ReadLine()!;
            Console.WriteLine("Enter your address");
            string address = Console.ReadLine()!;
            Console.WriteLine("Enter your password");
            string password = Console.ReadLine()!;
            Console.WriteLine("Enter your latitude");
            string latitude = Console.ReadLine()!;
            Console.WriteLine("Enter your longitude");
            string longitude = Console.ReadLine()!;
            Console.WriteLine("Enter your max distance");
            string maxDistance = Console.ReadLine()!;
            Console.WriteLine("Enter your role");
            string role = Console.ReadLine()!;
            Console.WriteLine("Enter your type of distance");
            Role convertedRole = (Role)Enum.Parse(typeof(Role), role);
            string typeOfDistance = Console.ReadLine()!;
            TypeOfDistance convertedTypeOfDistance = (TypeOfDistance)Enum.Parse(typeof(TypeOfDistance), typeOfDistance);
            s_dal.Volunteer!.Create(new(int.Parse(id), name, phone, email, password, address, Convert.ToDouble(latitude), Convert.ToDouble(longitude), convertedRole, false, int.Parse(maxDistance), convertedTypeOfDistance));
        }

        private static void volunteerDelete()
        {
            Console.WriteLine("Enter your ID");
            string id = Console.ReadLine()!;
            if (s_dal.Volunteer!.Read(int.Parse(id)) != null)
                s_dal.Volunteer!.Delete(int.Parse(id));
            else
                throw new DalDoesNotExistException("an obj with this id does not exist\n");
        }

        private static void volunteerDeleteAll()
        {
            s_dal.Volunteer!.DeleteAll();
        }

        private static void assignmentMenu()
        {
            try
            {
                SpecificOptions choice = SpecificOptions.EXIT;
                do
                {
                    Console.WriteLine("Enter your choice:\n" +
                        "to exit press 0\n" +
                        "to create a new assignment press 1\n" +
                        "to read an assignment's details press 2\n" +
                        "to read all assignments' details press 3\n" +
                        "to update an assignment's datails press 4\n" +
                        "to delete an assignment press 5\n" +
                        "to delete all assignments press 6");
                    choice = (SpecificOptions)Enum.Parse(typeof(SpecificOptions), Console.ReadLine()!);
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

        private static void assignmentCreate()
        {
            Console.WriteLine("Enter the call id");
            string callId = Console.ReadLine()!;
            Console.WriteLine("Enter the volunteer's id");
            string volunteerId = Console.ReadLine()!;
            s_dal.Assignment!.Create(new(0, int.Parse(callId), int.Parse(volunteerId), s_dal.Config.Clock, null, null));
        }

        private static void assignmentRead()
        {
            Console.WriteLine("Enter an ID");
            string id = Console.ReadLine()!;
            Console.WriteLine(s_dal.Assignment!.Read(int.Parse(id)));
        }

        private static void assignmentReadAll()
        {
            IEnumerable<Assignment> assignmentList = s_dal.Assignment!.ReadAll();
            foreach (var item in assignmentList)
            {
                Console.WriteLine(item);
            }
        }

        private static void assignmentUpdate()
        {
            Console.WriteLine("Enter assignment ID");
            string assignmentId = Console.ReadLine()!;
            if (s_dal.Assignment!.Read(int.Parse(assignmentId)) != null)
            {
                Console.WriteLine("Enter the call id");
                string callId = Console.ReadLine()!;
                Console.WriteLine("Enter the volunteer's id");
                string volunteerId = Console.ReadLine()!;
                s_dal.Assignment!.Update(new(int.Parse(assignmentId), int.Parse(callId), int.Parse(volunteerId), s_dal.Config!.Clock, null, null));
            }
            else
                throw new DalDoesNotExistException("an obj with this id does not exist\n");
        }

        private static void assignmentDelete()
        {
            Console.WriteLine("Enter assignment Id");
            string assignmentId = Console.ReadLine()!;
            if (s_dal.Assignment!.Read(int.Parse(assignmentId)) != null)
                s_dal.Assignment!.Delete(int.Parse(assignmentId));
            else
                throw new DalDoesNotExistException("an obj with this id does not exist\n");
        }

        private static void assignmentDeleteAll()
        {
            s_dal.Assignment!.DeleteAll();
        }

        private static void callMenu()
        {
            try
            {
                SpecificOptions choice = SpecificOptions.EXIT;
                do
                {
                    Console.WriteLine("Enter your choice:\n" +
                        "to exit press 0\n" +
                        "to create a new call press 1\n" +
                        "to read a cal's details press 2\n" +
                        "to read all calls' details press 3\n" +
                        "to update a call's datails press 4\n" +
                        "to delete a call press 5\n" +
                        "to delete all calls press 6");
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
        private static void callCreate()
        {
            //Console.WriteLine("Enter the call id");
            //string callId = Console.ReadLine()!;
            Console.WriteLine("Enter type of call");
            string typeOfCall = Console.ReadLine()!;
            TypeOfCall convertedType = (TypeOfCall)Enum.Parse(typeof(TypeOfCall), typeOfCall);
            Console.WriteLine("Enter a discription");
            string discription = Console.ReadLine()!;
            Console.WriteLine("Enter the call address");
            string address = Console.ReadLine()!;
            s_dal.Call!.Create(new(0, convertedType, discription, address, null, null, s_dal.Config!.RiskRange, s_dal.Config.Clock, null));
        }
        private static void callRead()
        {
            Console.WriteLine("Enter callId");
            string callId = Console.ReadLine()!;
            Call call = s_dal.Call!.Read(int.Parse(callId));
            Console.WriteLine(call);//לבדוק איך כותב למסך
        }
        private static void callReadAll()
        {
            IEnumerable<Call> callList = s_dal.Call!.ReadAll();
            foreach (var item in callList)
            {
                Console.WriteLine(item);
            }
        }
        private static void callUpdate()
        {
            Console.WriteLine("Enter the call id");
            string callId = Console.ReadLine()!;
            if (s_dal.Call!.Read(int.Parse(callId)) != null)
            {
                Console.WriteLine("Enter type of call");
                string typeOfCall = Console.ReadLine()!;
                TypeOfCall convertedTypeOfCall = (TypeOfCall)Enum.Parse(typeof(TypeOfCall), typeOfCall);
                Console.WriteLine("Enter a discription");
                string discription = Console.ReadLine()!;
                Console.WriteLine("Enter the call address");
                string address = Console.ReadLine()!;
                s_dal.Call!.Update(new(int.Parse(callId), convertedTypeOfCall, discription, address, null, null, s_dal.Config!.RiskRange, s_dal.Config.Clock, null));
            }
            else
                throw new DalDoesNotExistException("an obj with this id does not exist\n");
        }
        private static void callDelete()
        {
            Console.WriteLine("Enter call Id");
            string callId = Console.ReadLine()!;
            if (s_dal.Volunteer!.Read(int.Parse(callId)) != null)
                s_dal.Assignment!.Delete(int.Parse(callId));
            else
                throw new DalDoesNotExistException("an obj with this id does not exist\n");
        }
        private static void callDeleteAll()
        {
            s_dal.Call!.DeleteAll();
        }
        private static void initialize()
        {
            Initialization.Do(s_dal);
        }

        private static void printAllData()
        {
            IEnumerable<Volunteer> vList = s_dal.Volunteer!.ReadAll();
            foreach (var item in vList)
            {
                Console.WriteLine(item);
            }

            IEnumerable<Assignment> aList = s_dal.Assignment!.ReadAll();
            foreach (var item in aList)
            {
                Console.WriteLine(item);
            }

            IEnumerable<Call> cList = s_dal.Call!.ReadAll();
            foreach (var item in cList)
            {
                Console.WriteLine(item);
            }

        }
        private static void configMenu()
        {
            try
            {
                configOptions choice = configOptions.EXIT;
                do
                {
                    Console.WriteLine("Enter your choice:\n" +
                        "to exit press 0\n" +
                        "to advance the system clock by a minute press 1\n" +
                        "to advance the system clock by an hour press 2\n" +
                        "to advance the system clock by a day press 3\n" +
                        "to advance the system clock by a year press 4\n" +
                        "to display the current time press 5\n" +
                        "to change a configuration value press 6\n" +
                        "to display a current configuration value press 7\n" +
                        "to reset the configuration press 8");
                    choice = (configOptions)Enum.Parse(typeof(configOptions), Console.ReadLine()!);
                    switch (choice)
                    {
                        case configOptions.EXIT:
                            break;
                        case configOptions.ADVANCE_SYSTEM_CLOCK_BY_MINUTE:
                            advanceSystem(1);
                            break;
                        case configOptions.ADVANCE_SYSTEM_CLOCK_BY_HOUR:
                            advanceSystem(2);
                            break;
                        case configOptions.ADVANCE_SYSTEM_CLOCK_BY_DAY:
                            advanceSystem(3);
                            break;
                        case configOptions.ADVANCE_SYSTEM_CLOCK_BY_YEAR:
                            advanceSystem(4);
                            break;
                        case configOptions.DISPLAY_CURRENT_TIME:
                            Console.WriteLine(s_dal.Config!.Clock);
                            break;
                        case configOptions.CHANGE_VALUE:
                            changeValue();
                            break;
                        case configOptions.DISPLAY_CURRENT_VALUE:
                            displayCurrentValue();
                            break;
                        case configOptions.RESET_CONFIG:
                            s_dal.Config!.Reset();
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
        private static void advanceSystem(int amount)
        {
            Console.WriteLine($"current time:{s_dal.Config!.Clock}");
            switch (amount)
            {
                case 1:
                    s_dal.Config!.Clock = s_dal.Config.Clock.AddMinutes(1);
                    break;
                case 2:
                    s_dal.Config!.Clock = s_dal.Config.Clock.AddHours(1);
                    break;
                case 3:
                    s_dal.Config!.Clock = s_dal.Config.Clock.AddDays(1);
                    break;
                case 4:
                        s_dal.Config!.Clock = s_dal.Config.Clock.AddYears(1);
                    break;

            }
            Console.WriteLine($"updated time:{s_dal.Config!.Clock}");
        }
        private static void changeValue()
        {
            Console.WriteLine("Enter the number of the variable you want to change:\n" +
                " 1 for RiskRange\n" +
                " 2 for Clock");
            string choice = Console.ReadLine()!;
            switch (choice)
            {
                case "1":
                    {
                        Console.WriteLine($"current risk range:{s_dal.Config!.RiskRange}");
                        Console.WriteLine(s_dal.Config!.RiskRange);
                        Console.WriteLine("Enter the details of the new risk range:\nhours:\n");
                        string hours = Console.ReadLine()!;
                        Console.WriteLine("minutes:\n");
                        string minutes = Console.ReadLine()!;
                        Console.WriteLine("seconds:");
                        string seconds = Console.ReadLine()!;
                        s_dal.Config!.RiskRange = new TimeSpan(int.Parse(hours), int.Parse(minutes), int.Parse(seconds));
                        Console.WriteLine($"updated risk range:{s_dal.Config!.RiskRange}");
                    }
                    break;

                case "2":
                    {
                        Console.WriteLine($"current time:{s_dal.Config!.Clock}");
                        Console.WriteLine(s_dal.Config!.Clock);
                        Console.WriteLine("Enter the details of the new clock:\nhour:\n");
                        string hour = Console.ReadLine()!;
                        Console.WriteLine("minute:\n");
                        string minute = Console.ReadLine()!;
                        Console.WriteLine("second:");
                        string second = Console.ReadLine()!;
                        s_dal.Config!.Clock = new DateTime(int.Parse(hour), int.Parse(minute), int.Parse(second));
                        Console.WriteLine($"updated risk range:{s_dal.Config!.Clock}");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;

            }
        }
        private static void displayCurrentValue()
        {
            Console.WriteLine("Enter the number of the variable you want to be displayed:\n" +
                " 1 for RiskRange\n" +
                " 2 for Clock");
            string choice = Console.ReadLine()!;
            switch (choice)
            {
                case "1":
                    {
                        Console.WriteLine(s_dal.Config!.RiskRange);
                    }
                    break;

                case "2":
                    {
                        Console.WriteLine(s_dal.Config!.Clock);
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;

            }
        }
        private static void resetDbAndConfig()
        {
            s_dal.Volunteer!.DeleteAll();
            s_dal.Assignment!.DeleteAll();
            s_dal.Call!.DeleteAll();
            s_dal.Config!.Reset();
        }


        static void Main(string[] args)
        {
            try
            {
                //Console.WriteLine("Enter your choice:\n" +
                //             "to exit press 0\n" +
                //             "to the volunteer menu press 1\n" +
                //             "to the assignment menu press 2\n" +
                //             "to the call menue press 3\n" +
                //             "to initialize the system press 4\n" +
                //             "to print all the data press 5\n" +
                //             "to the configuration menu press 6\n" +
                //             "to reset the data base and the configuration press 7.");

                Options choice = Options.EXIT; // Set a default choice
                do
                {
                    Console.WriteLine("Enter your choice:\n" +
                        "to exit press 0\n" +
                        "to the volunteer menu press 1\n" +
                        "to the assignment menu press 2\n" +
                        "to the call menue press 3\n" +
                        "to initialize the system press 4\n" +
                        "to print all the data press 5\n" +
                        "to the configuration menu press 6\n" +
                        "to reset the data base and the configurationpress 7.");
                    choice = (Options)Enum.Parse(typeof(Options), Console.ReadLine()!);

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
                } while (choice != Options.EXIT);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}