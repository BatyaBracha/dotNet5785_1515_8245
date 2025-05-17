using BO;
using DalApi;
using DO;
using System.Data;

namespace BlTest;

/// <summary>
/// The main program class for managing volunteer-related operations.
/// </summary>
internal class Program
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    /// <summary>
    /// Displays the volunteer menu and handles user choices for volunteer operations.
    /// </summary>
    private static void volunteerMenu()
    {
        try
        {
            BO.VolunteerOptions choice = BO.VolunteerOptions.EXIT;
            do
            {
                Console.WriteLine("Enter your choice:\n" +
                    "to exit press 0\n" +
                    "to create a new volunteer press 1\n" +
                    "to read a volunteer's details press 2\n" +
                    "to read all volunteers' details and filter by active status or sort by name or ID, press 3\n" +
                    "to update a volunteer's details press 4\n" +
                    "to delete a volunteer press 5\n" +
                    "to delete all volunteers press 6");
                choice = (BO.VolunteerOptions)Enum.Parse(typeof(BO.VolunteerOptions), Console.ReadLine()!);
                switch (choice)
                {
                    case BO.VolunteerOptions.EXIT:
                        break;
                    case BO.VolunteerOptions.CREATE:
                        volunteerCreate();
                        break;
                    case BO.VolunteerOptions.READ:
                        volunteerRead();
                        break;
                    case BO.VolunteerOptions.READ_ALL:
                        volunteerReadAll();
                        break;
                    case BO.VolunteerOptions.UPDATE:
                        volunteerUpdate();
                        break;
                    case BO.VolunteerOptions.DELETE:
                        volunteerDelete();
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            } while (choice != BO.VolunteerOptions.EXIT);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Prompts the user for volunteer details and creates a new volunteer.
    /// </summary>
    private static void volunteerCreate()
    {
        Console.WriteLine("Enter ID");
        if (!int.TryParse(Console.ReadLine(), out int id))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer.");

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
        if (!int.TryParse(Console.ReadLine(), out int maxDistance))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer.");

        Console.WriteLine("Enter your role");
        string role = Console.ReadLine()!;

        Console.WriteLine("Enter your type of distance");
        BO.Role convertedRole = (BO.Role)Enum.Parse(typeof(BO.Role), role);

        string typeOfDistance = Console.ReadLine()!;
        BO.TypeOfDistance convertedTypeOfDistance = (BO.TypeOfDistance)Enum.Parse(typeof(BO.TypeOfDistance), typeOfDistance);


        s_bl.Volunteer!.Create(new BO.Volunteer(id, name, phone, email, password, address, null, null, convertedRole, false, maxDistance, convertedTypeOfDistance, 0, 0, 0, null));
    }

    /// <summary>
    /// Prompts the user for an ID and reads the details of the corresponding volunteer.
    /// </summary>
    private static void volunteerRead()
    {
        Console.WriteLine("Enter an ID");
        if (!int.TryParse(Console.ReadLine(), out int id))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer.");

        BO.Volunteer volunteer = s_bl.Volunteer!.Read(id)!;
        Console.WriteLine(volunteer); // Display the volunteer details
    }

    /// <summary>
    /// Reads and displays the details of all volunteers.
    /// </summary>
    private static void volunteerReadAll()
    {
        BO.Active? sort = null;
        BO.VolunteerFields? filter = null;

        Console.WriteLine("Would you like to filter the list by active status? (yes/no)");
        string filterActiveResponse = Console.ReadLine();

        if (filterActiveResponse?.ToLower() == "yes")
        {
            Console.WriteLine("Please specify the active status: (true/false)");
            string activeStatus = Console.ReadLine();
            sort = activeStatus?.ToLower() == "true" ? BO.Active.TRUE : BO.Active.FALSE;
        }

        Console.WriteLine("Would you like to sort the list? (yes/no)");
        string sortResponse = Console.ReadLine();

        if (sortResponse?.ToLower() == "yes")
        {
            Console.WriteLine("Please specify the field to sort by: (1 for Name, 2 for ID)");
            string sortField = Console.ReadLine();

            filter = sortField == "1" ? BO.VolunteerFields.Name : BO.VolunteerFields.Id;
        }

        IEnumerable<BO.VolunteerInList> volunteerList = s_bl.Volunteer!.ReadAll(sort, filter);
        foreach (var v in volunteerList)
        {
            Console.WriteLine(v);
        }
    }

    /// <summary>
    /// Prompts the user for volunteer details and updates the corresponding volunteer's information.
    /// </summary>
    private static void volunteerUpdate()
    {
        Console.WriteLine("Enter your ID");
        if (!int.TryParse(Console.ReadLine(), out int id))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer.");

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
        if (!int.TryParse(Console.ReadLine(), out int maxDistance))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer.");

        Console.WriteLine("Enter calls done");
        if (!int.TryParse(Console.ReadLine(), out int callsDone))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer.");

        Console.WriteLine("Enter calls deleted");
        if (!int.TryParse(Console.ReadLine(), out int callsDeleted))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer.");

        Console.WriteLine("Enter calls chosen out of date");
        if (!int.TryParse(Console.ReadLine(), out int callsChosenOutOfdate))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer.");
        Console.WriteLine("Enter type of distance");

        string typeOfDistance = Console.ReadLine()!;
        BO.TypeOfDistance convertedTypeOfDistance = (BO.TypeOfDistance)Enum.Parse(typeof(BO.TypeOfDistance), typeOfDistance);
        //בשלב 5 צריך לשנות את זה שזה ישלח את התס של מי שמשתמש במערכת כדי לבדק שזה אדם שמשנה לעצמו את הפרטים או שזה המנהל
        s_bl.Volunteer!.Update(id,new BO.Volunteer(id, name, phone, email, password, address, null, null, BO.Role.STANDARD, false, maxDistance, convertedTypeOfDistance, callsDone, callsDeleted, callsChosenOutOfdate, null));
    }

    /// <summary>
    /// Prompts the user for an ID and deletes the corresponding volunteer.
    /// </summary>
    private static void volunteerDelete()
    {
        Console.WriteLine("Enter your ID");
        if (!int.TryParse(Console.ReadLine(), out int id))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer.");

        if (s_bl.Volunteer!.Read(id) != null)
            s_bl.Volunteer!.Delete(id);
        else
            throw new BlUnauthorizedOperationException("An object with this ID does not exist.");
    }

    /// <summary>
    /// Assigns a volunteer to a specific call based on user input.
    /// </summary>
    //private static void AssignVolunteerToCall()
    //{
    //    int volunteerId;
    //    int callId;

    //    Console.WriteLine("Please enter the Volunteer ID:");
    //    if (!int.TryParse(Console.ReadLine(), out volunteerId))
    //    {
    //        Console.WriteLine("Invalid input. Please enter a numeric value for Volunteer ID.");
    //        return;
    //    }

    //    Console.WriteLine("Please enter the Call ID:");
    //    if (!int.TryParse(Console.ReadLine(), out callId))
    //    {
    //        Console.WriteLine("Invalid input. Please enter a numeric value for Call ID.");
    //        return;
    //    }

    //    s_bl.Volunteer!.MatchVolunteerToCall(volunteerId, callId);
    //    Console.WriteLine("The volunteer has been successfully assigned to the call.");
    //}

    /// <summary>
    /// Unmatches a volunteer from a specific call based on user input.
    /// </summary>
    //private static void UnmatchVolunteerFromCall()
    //{
    //    Console.WriteLine("Please enter the Volunteer ID:");
    //    string volunteerIdInput = Console.ReadLine();
    //    int volunteerId;

    //    if (!int.TryParse(volunteerIdInput, out volunteerId))
    //    {
    //        Console.WriteLine("Invalid input. Please enter a numeric value for Volunteer ID.");
    //        return;
    //    }

    //    Console.WriteLine("Please enter the Call ID:");
    //    string callIdInput = Console.ReadLine();
    //    int callId;

    //    if (!int.TryParse(callIdInput, out callId))
    //    {
    //        Console.WriteLine("Invalid input. Please enter a numeric value for Call ID.");
    //        return;
    //    }

    //    try
    //    {
    //        s_bl.Volunteer!.UnMatchVolunteerToCall(volunteerId, callId);
    //        Console.WriteLine("The volunteer has been successfully unmatched from the call.");
    //    }
    //    catch (BO.BlDoesNotExistException ex)
    //    {
    //        Console.WriteLine($"Error: {ex.Message}");
    //    }
    //    catch (BO.BlUnauthorizedOperationException ex)
    //    {
    //        Console.WriteLine($"Data access error: {ex.Message}");
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine($"An unexpected error occurred: {ex.Message}");
    //    }
    //}

    /// <summary>
    /// Displays the call menu and handles user choices for call operations.
    /// </summary>
    private static void callMenu()
    {
        try
        {
            BO.SpecificOptions choice = BO.SpecificOptions.EXIT;
            do
            {
                Console.WriteLine("Enter your choice:\n" +
                    "to exit press 0\n" +
                    "to get the amount of calls press 1\n" +
                    "to create a new call press 2\n" +
                    "to read a call's details press 3\n" +
                    "to read all calls' details press 4\n" +
                    "to update a call's details press 5\n" +
                    "to delete a call press 6\n" +
                    "to read all closed calls press 7\n" +
                    "to read all open calls press 8\n" +
                    "to update a call cancelation press 9\n" +
                    "to end a call press 10\n" +
                    "to choose a call for treatment press 11\n");
                choice = (BO.SpecificOptions)Enum.Parse(typeof(BO.SpecificOptions), Console.ReadLine()!);
                switch (choice)
                {
                    case BO.SpecificOptions.EXIT:
                        break;
                    case BO.SpecificOptions.CALLCOUNT:
                        callCount();
                        break;
                    case BO.SpecificOptions.CREATE:
                        callCreate();
                        break;
                    case BO.SpecificOptions.READ:
                        callRead();
                        break;
                    case BO.SpecificOptions.READ_ALL:
                        callReadAll();
                        break;
                    case BO.SpecificOptions.UPDATE:
                        callUpdate();
                        break;
                    case BO.SpecificOptions.DELETE:
                        callDelete();
                        break;
                    case BO.SpecificOptions.CLOSEDCALLS:
                        closedCalls();
                        break;
                    case BO.SpecificOptions.OPENCALLS:
                        openCalls();
                        break;
                    case BO.SpecificOptions.UPDATECALLCANCELATION:
                        updateCallCancelation();
                        break;
                    case BO.SpecificOptions.UPDATEENDOFCALL:
                        updateEndOfcall();
                        break;
                    case BO.SpecificOptions.ASSIGN_VOLUNTEER_TO_CALL:
                        ChooseACallForTreatment();
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            } while (choice != BO.SpecificOptions.EXIT);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves and displays the count of calls.
    /// </summary>
    private static void callCount()
    {
        IEnumerable<int> callsCount = s_bl.Call.GetCallsCount(); // Call the method
        foreach (var count in callsCount)
        {
            Console.WriteLine(count);
        }
    }

    /// <summary>
    /// Prompts the user for call details and creates a new call.
    /// </summary>
    private static void callCreate()
    {
        Console.WriteLine("Enter type of call");
        string typeOfCallInput = Console.ReadLine()!;
        if (!Enum.TryParse<BO.TypeOfCall>(typeOfCallInput, out BO.TypeOfCall convertedType))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid type of call.");

        Console.WriteLine("Enter a description");
        string description = Console.ReadLine()!;

        Console.WriteLine("Enter the call address");
        string address = Console.ReadLine()!;

        Console.WriteLine("Enter the maximum closing time (format: yyyy-MM-dd HH:mm:ss)");
        string maxClosingTimeInput = Console.ReadLine()!;
        if (!DateTime.TryParse(maxClosingTimeInput, out DateTime maxClosingTime))
        {
            throw new BlArgumentException("Invalid input. Please enter a valid date and time in the format yyyy-MM-dd HH:mm:ss.");
        }
        s_bl.Call!.Create(new BO.Call(0, convertedType, description, address, null, null, s_bl.Admin.GetClock(), maxClosingTime, BO.CallStatus.OPEN, null));
    }

    /// <summary>
    /// Prompts the user for an ID and reads the details of the corresponding call.
    /// </summary>
    private static void callRead()
    {
        Console.WriteLine("Enter an ID");
        if (!int.TryParse(Console.ReadLine(), out int id))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer.");

        BO.Call call = s_bl.Call!.GetCallDetails(id)!;
        Console.WriteLine(call); // Display the call details
    }

    /// <summary>
    /// Reads and displays the details of all calls, with optional filtering and sorting.
    /// </summary>
    public static void callReadAll()
    {
        Console.WriteLine("Do you want to filter by a specific field? (yes/no)");
        string filterResponse = Console.ReadLine()?.ToLower();

        Enum? filterBy = null;
        object? filterValue = null;

        if (filterResponse == "yes")
        {
            Console.WriteLine("Select a filter field by entering the corresponding number:");
            Console.WriteLine("0 for STATUS");
            //Console.WriteLine("2 for PRIORITY");
            Console.WriteLine("1 for TYPE");
            string filterFieldInput = Console.ReadLine();
            filterBy = Enum.TryParse<BO.CallField>(filterFieldInput, out var filterField) ? filterField : null;

            Console.WriteLine("Enter the filter value: (if filtering according to type, these are some options:CARDIAC_ARREST,DIFFICULTY_BREATHING");
            filterValue = Console.ReadLine(); // Adjust parsing based on expected type
        }

        Console.WriteLine("Do you want to sort the results? (yes/no)");
        string sortResponse = Console.ReadLine()?.ToLower();

        Enum? sortBy = null;

        if (sortResponse == "yes")
        {
            Console.WriteLine("Select a sort field:");
            Console.WriteLine("0 for Type of call");
            Console.WriteLine("1 for Status");
            string sortFieldInput = Console.ReadLine();
            sortBy = Enum.TryParse<BO.CallInListField>(sortFieldInput, out var sortField) ? sortField : null;
        }

        IEnumerable<BO.CallInList> callList = s_bl.Call!.ReadAll(filterBy, filterValue, sortBy);
        foreach (var c in callList)
        {
            Console.WriteLine(c);
        }
    }

    /// <summary>
    /// Prompts the user for call details and updates the corresponding call's information.
    /// </summary>
    private static void callUpdate()
    {
        Console.WriteLine("Enter the Call ID");
        if (!int.TryParse(Console.ReadLine(), out int id))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer.");

        Console.WriteLine("Enter the type of call (e.g., TYPE1, TYPE2):");
        string typeOfCallInput = Console.ReadLine()!;
        BO.TypeOfCall typeOfCall = (BO.TypeOfCall)Enum.Parse(typeof(BO.TypeOfCall), typeOfCallInput);

        Console.WriteLine("Enter the description:");
        string? description = Console.ReadLine();

        Console.WriteLine("Enter the address:");
        string? address = Console.ReadLine();

        Console.WriteLine("Enter the max closing time (yyyy-mm-dd hh:mm:ss, or leave blank):");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime maxClosingTime))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid date and time.");

        Console.WriteLine("Enter the status of the call (e.g., OPEN, CLOSED):");
        string statusInput = Console.ReadLine()!;
        BO.CallStatus status = (BO.CallStatus)Enum.Parse(typeof(BO.CallStatus), statusInput);

        s_bl.Call!.Update(new BO.Call(id, typeOfCall, description, address, null, null, DateTime.MinValue, maxClosingTime, status, null));
    }

    /// <summary>
    /// Prompts the user for a call ID and deletes the corresponding call.
    /// </summary>
    private static void callDelete()
    {
        Console.WriteLine("Enter the call ID");
        if (!int.TryParse(Console.ReadLine(), out int id))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer.");

        if (s_bl.Call!.GetCallDetails(id) != null)
            s_bl.Call!.Delete(id);
        else
            throw new BlUnauthorizedOperationException("An object with this ID does not exist.");
    }

    /// <summary>
    /// Retrieves and displays the closed calls for a specific volunteer based on user input.
    /// </summary>
    private static void closedCalls()
    {
        Console.WriteLine("Enter your volunteer ID:");
        if (!int.TryParse(Console.ReadLine(), out int volunteerId))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer for the volunteer ID.");

        Console.WriteLine("Do you want to filter by a specific field? (yes/no)");
        string filterResponse = Console.ReadLine()?.ToLower();

        Enum? filterBy = null;
        object? filterValue = null;

        if (filterResponse == "yes")
        {
            Console.WriteLine("Select a filter field (STATUS, PRIORITY, TYPE, ADDRESS, CALL_VOLUNTEER_DISTANCE, ID):");
            string filterFieldInput = Console.ReadLine();
            filterBy = Enum.TryParse<BO.CallField>(filterFieldInput, out var filterField) ? filterField : null;

            Console.WriteLine("Enter the filter value:");
            filterValue = Console.ReadLine(); // Adjust parsing based on expected type
        }

        Console.WriteLine("Do you want to sort the results? (yes/no)");
        string sortResponse = Console.ReadLine()?.ToLower();

        Enum? sortBy = null;

        if (sortResponse == "yes")
        {
            Console.WriteLine("Select a sort field (ADDRESS, CALL_VOLUNTEER_DISTANCE, ID):");
            string sortFieldInput = Console.ReadLine();
            sortBy = Enum.TryParse<BO.CallField>(sortFieldInput, out var sortField) ? sortField : null;
        }

        var closedCalls = s_bl.Call.GetClosedCallsHandledByTheVolunteer(volunteerId, filterBy, filterValue, sortBy);

        foreach (var call in closedCalls)
        {
            Console.WriteLine(call);
        }
    }

    /// <summary>
    /// Retrieves and displays the open calls for a specific volunteer based on user input.
    /// </summary>
    private static void openCalls()
    {
        Console.WriteLine("Enter your volunteer ID:");
        if (!int.TryParse(Console.ReadLine(), out int volunteerId))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer for the volunteer ID.");

        Console.WriteLine("Do you want to filter by a specific field? (yes/no)");
        string filterResponse = Console.ReadLine()?.ToLower();

        Enum? filterBy = null;
        object? filterValue = null;

        if (filterResponse == "yes")
        {
            Console.WriteLine("Select a filter field (STATUS, PRIORITY, TYPE, ADDRESS, CALL_VOLUNTEER_DISTANCE, ID):");
            string filterFieldInput = Console.ReadLine();
            filterBy = Enum.TryParse<BO.CallField>(filterFieldInput, out var filterField) ? filterField : null;

            Console.WriteLine("Enter the filter value:");
            filterValue = Console.ReadLine(); // Adjust parsing based on expected type
        }

        Console.WriteLine("Do you want to sort the results? (yes/no)");
        string sortResponse = Console.ReadLine()?.ToLower();

        Enum? sortBy = null;

        if (sortResponse == "yes")
        {
            Console.WriteLine("Select a sort field (ADDRESS, CALL_VOLUNTEER_DISTANCE, ID):");
            string sortFieldInput = Console.ReadLine();
            sortBy = Enum.TryParse<BO.CallField>(sortFieldInput, out var sortField) ? sortField : null;
        }

        IEnumerable<BO.OpenCallInList> openCalls = s_bl.Call!.GetOpenCallsCanBeSelectedByAVolunteer(volunteerId, filterBy, sortBy);
        foreach (var call in openCalls)
        {
            Console.WriteLine(call);
        }
    }

    /// <summary>
    /// Updates the cancellation status of a treatment based on user input.
    /// </summary>
    private static void updateCallCancelation()
    {
        Console.WriteLine("Enter your volunteer ID:");
        if (!int.TryParse(Console.ReadLine(), out int volunteerId))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer for the volunteer ID.");

        Console.WriteLine("Enter the assignment ID:");
        if (!int.TryParse(Console.ReadLine(), out int assignmentId))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer for the assignment ID.");

        s_bl.Call.TreatmentCancellationUpdate(volunteerId, assignmentId);
        Console.WriteLine("Treatment has been successfully canceled.");
    }

    /// <summary>
    /// Updates the completion status of a treatment based on user input.
    /// </summary>
    private static void updateEndOfcall()
    {
        Console.WriteLine("Enter your volunteer ID:");
        if (!int.TryParse(Console.ReadLine(), out int volunteerId))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer for the volunteer ID.");

        Console.WriteLine("Enter the assignment ID:");
        if (!int.TryParse(Console.ReadLine(), out int assignmentId))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer for the assignment ID.");

        s_bl.Call.TreatmentCompletionUpdate(volunteerId, assignmentId);
        Console.WriteLine("Treatment has been successfully marked as completed.");
    }

    /// <summary>
    /// Assigns a volunteer to a specific call based on user input.
    /// </summary>
    private static void ChooseACallForTreatment()
    {
        Console.WriteLine("Enter your volunteer ID:");
        if (!int.TryParse(Console.ReadLine(), out int volunteerId))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer for the volunteer ID.");

        Console.WriteLine("Enter the call ID:");
        if (!int.TryParse(Console.ReadLine(), out int callId))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer for the call ID.");

        s_bl.Call.ChoosingACallForTreatment(volunteerId, callId);
        Console.WriteLine("The call has been successfully assigned for treatment.");
    }

    /// <summary>
    /// Handles user login for both managers and volunteers.
    /// </summary>
    //private static void Login()
    //{
    //    Console.WriteLine("Enter your ID:");
    //    if (!int.TryParse(Console.ReadLine(), out int userId))
    //    {
    //        Console.WriteLine("Invalid ID. Please enter a valid integer.");
    //        return;
    //    }

    //    Console.WriteLine("Enter your password (leave blank if not applicable):");
    //    string password = Console.ReadLine();

    //    BO.Volunteer user = s_bl.Volunteer.Read(userId);
    //    BO.Role role = s_bl.Volunteer.Login(user.Id, password);
    //    if (user == null || (password != null &&user.Password != password))
    //    {
    //        Console.WriteLine("Invalid credentials. Please try again.");
    //        return;
    //    }

    //    Console.WriteLine("Welcome to the Main Screen! Choose your next step:");
    //    Console.WriteLine("1. Go to Volunteer Screen");
    //    Console.WriteLine("2. Go to Call Screen");
    //    Console.WriteLine("3. Go to Main Management Screen");
    //    string choice = Console.ReadLine()!;
    //    if (choice == "1")
    //    {
    //        volunteerMenu();
    //    }
    //    else if (choice == "2")
    //    {
    //        callMenu();
    //    }
    //    else if (choice == "3")
    //    {
    //        adminMenu();
    //    }
    //    else
    //    {
    //        Console.WriteLine("Invalid choice. Returning to login.");
    //    }
    //}
    /// <summary>
    /// Handles user login for both managers and volunteers.
    /// </summary>
    private static void Login()
    {
        while (true) // Loop to allow retrying the login process
        {
            try
            {
                Console.WriteLine("Enter your ID (or type 'exit' to quit):");
                string userIdInput = Console.ReadLine();
                if (userIdInput?.ToLower() == "exit")
                {
                    Console.WriteLine("Exiting the program. Goodbye!");
                    Environment.Exit(0); // Exit the program
                }

                if (!int.TryParse(userIdInput, out int userId))
                {
                    Console.WriteLine("Invalid ID. Please enter a valid integer.");
                    continue; // Restart the login process
                }

                Console.WriteLine("Enter your password (leave blank if not applicable):");
                string password = Console.ReadLine();

                // Attempt to log in
                BO.Role role = s_bl.Volunteer.Login(userId, password);

                Console.WriteLine("Login successful!");
                MainMenu(); // Call the main menu after successful login
                break; // Exit the login loop after successful login
            }
            catch (BO.BlUnauthorizedOperationException ex)
            {
                Console.WriteLine($"Login failed: {ex.Message}");
                Console.WriteLine("Would you like to try again? (yes/no)");
                string retry = Console.ReadLine()?.ToLower();
                if (retry != "yes")
                {
                    Console.WriteLine("Exiting the program. Goodbye!");
                    Environment.Exit(0); // Exit the program
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                Console.WriteLine("Would you like to try again? (yes/no)");
                string retry = Console.ReadLine()?.ToLower();
                if (retry != "yes")
                {
                    Console.WriteLine("Exiting the program. Goodbye!");
                    Environment.Exit(0); // Exit the program
                }
            }
        }
    }

    /// <summary>
    /// Displays the main menu and handles user choices after login.
    /// </summary>
    private static void MainMenu()
    {
        while (true) // Loop to keep the user in the main menu
        {
            Console.WriteLine("Welcome to the Main Screen! Choose your next step:");
            Console.WriteLine("1. Go to Volunteer Screen");
            Console.WriteLine("2. Go to Call Screen");
            Console.WriteLine("3. Go to Main Management Screen");
            Console.WriteLine("4. Exit");

            string choice = Console.ReadLine()!;
            switch (choice)
            {
                case "1":
                    volunteerMenu();
                    break;
                case "2":
                    callMenu();
                    break;
                case "3":
                    adminMenu();
                    break;
                case "4":
                    Console.WriteLine("Exiting the program. Goodbye!");
                    Environment.Exit(0); // Exit the program
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    /// <summary>
    /// Displays the admin menu and handles user choices for admin operations.
    /// </summary>
    private static void adminMenu()
    {
        try
        {
            AdminOptions choice = AdminOptions.EXIT;
            do
            {
                Console.WriteLine("Enter your choice:\n" +
                    "to exit press 0\n" +
                    "to see what the system time is, press 1\n" +
                    "to advance the clock, press 2\n" +
                    "to get the risk range, press 3\n" +
                    "to set the risk range, press 4\n" +
                    "to reset all the DB, press 5\n" +
                    "to initialize the DB, press 6\n");
                choice = (AdminOptions)Enum.Parse(typeof(AdminOptions), Console.ReadLine()!);
                switch (choice)
                {
                    case AdminOptions.EXIT:
                        break;
                    case AdminOptions.GET_CLOCK:
                        getClock();
                        break;
                    case AdminOptions.PROMOTION_CLOCK:
                        promotionClock();
                        break;
                    case AdminOptions.GET_RISK_RANGE:
                        getRiskRange();
                        break;
                    case AdminOptions.SET_RISK_RANGE:
                        setRiskRange();
                        break;
                    case AdminOptions.RESET_DB:
                        resetDB();
                        break;
                    case AdminOptions.INITIALIZE_DB:
                        initializeDB();
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            } while (choice != AdminOptions.EXIT);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    private static void initializeDB()
    {
        s_bl.Admin.InitializeDB();
    }

    private static void resetDB()
    {
        s_bl.Admin.ResetDB();
    }

    private static void setRiskRange()
    {
        Console.WriteLine("Please enter the risk range:");
        if (!TimeSpan.TryParse(Console.ReadLine(), out TimeSpan riskRange))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer for the risk range.");
        s_bl.Admin.SetRiskRange(riskRange);
    }

    private static void getRiskRange()
    {
        Console.WriteLine(s_bl.Admin.GetRiskRange());
    }

    private static void getClock()
    {
        Console.WriteLine(s_bl.Admin.GetClock());
    }

    private static void promotionClock()
    {
        Console.WriteLine("Enter the time unit you want to advance by:");
        if (!TimeUnit.TryParse(Console.ReadLine(), out TimeUnit timeUnit))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer for the time unit.");

        Console.WriteLine("Enter your choice:\n" +
            "to advance the clock by one year, press 0\n" +
            "to advance the clock by one month, press 1\n" +
            "to advance the clock by one day, press 2\n" +
            "to advance the clock by one hour, press 3\n" +
            "to advance the clock by one minute, press 4\n");

        switch (timeUnit)
        {
            case BO.TimeUnit.MINUTE:
                timeUnit = TimeUnit.MINUTE;
                break;
            case BO.TimeUnit.HOUR:
                timeUnit = TimeUnit.HOUR;
                break;
            case BO.TimeUnit.DAY:
                timeUnit = TimeUnit.DAY;
                break;
            case BO.TimeUnit.MONTH:
                timeUnit = TimeUnit.MONTH;
                break;
            case BO.TimeUnit.YEAR:
                timeUnit = TimeUnit.YEAR;
                break;
            default:
                throw new BlArgumentException("Unknown time unit");
        }

        s_bl.Admin.PromotionClock(timeUnit);
    }

    /// <summary>
    /// The main entry point of the program.
    /// </summary>
    /// <param name="args">Command-line arguments.</param>
    static void Main(string[] args)
    {
        try
        {
            Login();
        }
        catch (BlUnauthorizedOperationException ex)
        {
            Console.WriteLine($"Unauthorized operation: {ex.Message}");
        }
        catch (BO.BlDoesNotExistException ex)
        {
            Console.WriteLine($"Not found: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}