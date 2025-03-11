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
            SpecificOptions choice = SpecificOptions.EXIT;
            do
            {
                Console.WriteLine("Enter your choice:\n" +
                    "to exit press 0\n" +
                    "to create a new volunteer press 1\n" +
                    "to read a volunteer's details press 2\n" +
                    "to read all volunteers' details and filter by active status or sort by name or ID, press 3\n" +
                    "to update a volunteer's details press 4\n" +
                    "to delete a volunteer press 5\n" +
                    "to delete all volunteers press 6\n" +
                    //"to assign a volunteer to a call press 7\n" +
                    //"to unmatch a volunteer from a call press 8"
                    );
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
                    //case SpecificOptions.ASSIGN_VOLUNTEER_TO_CALL:
                    //    AssignVolunteerToCall();
                    //    break;
                    //case SpecificOptions.UNMATCH_VOLUNTEER_FROM_CALL:
                    //    UnmatchVolunteerFromCall();
                    //    break;
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

    /// <summary>
    /// Prompts the user for volunteer details and creates a new volunteer.
    /// </summary>
    /// <exception cref="BlUnauthorizedOperationException">Thrown when user input is invalid.</exception>
    private static void volunteerCreate()
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
        Console.WriteLine("Enter your role");
        string role = Console.ReadLine()!;
        Console.WriteLine("Enter your type of distance");
        BO.Role convertedRole = (BO.Role)Enum.Parse(typeof(BO.Role), role);
        string typeOfDistance = Console.ReadLine()!;
        BO.TypeOfDistance convertedTypeOfDistance = (BO.TypeOfDistance)Enum.Parse(typeof(BO.TypeOfDistance), typeOfDistance);

        Console.WriteLine("Enter calls done");
        if (!int.TryParse(Console.ReadLine(), out int callsDone))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer.");

        Console.WriteLine("Enter calls deleted");
        if (!int.TryParse(Console.ReadLine(), out int callsDeleted))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer.");

        Console.WriteLine("Enter calls chosen out of date");
        if (!int.TryParse(Console.ReadLine(), out int callsChosenOutOfdate))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer.");

        s_bl.Volunteer!.Create(new BO.Volunteer(id, name, phone, email, password, address, null, null, convertedRole, false, maxDistance, convertedTypeOfDistance, callsDone, callsDeleted, callsChosenOutOfdate, null));
    }

    /// <summary>
    /// Prompts the user for an ID and reads the details of the corresponding volunteer.
    /// </summary>
    /// <exception cref="BlUnauthorizedOperationException">Thrown when user input is invalid.</exception>
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

        // Ask the user if they want to filter by active status
        Console.WriteLine("Would you like to filter the list by active status? (yes/no)");
        string filterActiveResponse = Console.ReadLine();

        if (filterActiveResponse?.ToLower() == "yes")
        {
            Console.WriteLine("Please specify the active status: (true/false)");
            string activeStatus = Console.ReadLine();
            sort = activeStatus?.ToLower() == "true" ? BO.Active.TRUE : BO.Active.FALSE;
        }

        // Ask the user if they want to sort the list
        Console.WriteLine("Would you like to sort the list? (yes/no)");
        string sortResponse = Console.ReadLine();

        if (sortResponse?.ToLower() == "yes")
        {
            Console.WriteLine("Please specify the field to sort by: (1 for Name, 2 for ID)");
            string sortField = Console.ReadLine();

            filter = sortField == "1" ? BO.VolunteerFields.Name : BO.VolunteerFields.Id;
        }

        // Call the ReadAll method with the gathered parameters
        IEnumerable<BO.VolunteerInList> volunteerList = s_bl.Volunteer!.ReadAll(sort, filter);
        foreach (var v in volunteerList)
        {
            Console.WriteLine(v);
        }
    }

    /// <summary>
    /// Prompts the user for volunteer details and updates the corresponding volunteer's information.
    /// </summary>
    /// <exception cref="BlUnauthorizedOperationException">Thrown when user input is invalid.</exception>
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

        Console.WriteLine("Enter your type of distance");
        string typeOfDistance = Console.ReadLine()!;
        BO.TypeOfDistance convertedTypeOfDistance = (BO.TypeOfDistance)Enum.Parse(typeof(BO.TypeOfDistance), typeOfDistance);
        s_bl.Volunteer!.Update(new BO.Volunteer(id, name, phone, email, password, address, null, null, BO.Role.STANDARD, false, maxDistance, convertedTypeOfDistance, callsDone, callsDeleted, callsChosenOutOfdate, null));
    }

    /// <summary>
    /// Prompts the user for an ID and deletes the corresponding volunteer.
    /// </summary>
    /// <exception cref="BlUnauthorizedOperationException">Thrown when user input is invalid.</exception>
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
    private static void AssignVolunteerToCall()
    {
        int volunteerId;
        int callId;

        // Prompt user for volunteer ID
        Console.WriteLine("Please enter the Volunteer ID:");
        if (!int.TryParse(Console.ReadLine(), out volunteerId))
        {
            Console.WriteLine("Invalid input. Please enter a numeric value for Volunteer ID.");
            return; // Exit the method if input is invalid
        }

        // Prompt user for call ID
        Console.WriteLine("Please enter the Call ID:");
        if (!int.TryParse(Console.ReadLine(), out callId))
        {
            Console.WriteLine("Invalid input. Please enter a numeric value for Call ID.");
            return; // Exit the method if input is invalid
        }

        // Call the MatchVolunteerToCall function
        s_bl.Volunteer!.MatchVolunteerToCall(volunteerId, callId);

        // Inform the user of success
        Console.WriteLine("The volunteer has been successfully assigned to the call.");
    }

    /// <summary>
    /// Unmatches a volunteer from a specific call based on user input.
    /// </summary>
    private static void UnmatchVolunteerFromCall()
    {
        Console.WriteLine("Please enter the Volunteer ID:");
        string volunteerIdInput = Console.ReadLine();
        int volunteerId;

        // Attempt to parse the volunteer ID
        if (!int.TryParse(volunteerIdInput, out volunteerId))
        {
            Console.WriteLine("Invalid input. Please enter a numeric value for Volunteer ID.");
            return; // Exit the method if input is invalid
        }

        Console.WriteLine("Please enter the Call ID:");
        string callIdInput = Console.ReadLine();
        int callId;

        // Attempt to parse the call ID
        if (!int.TryParse(callIdInput, out callId))
        {
            Console.WriteLine("Invalid input. Please enter a numeric value for Call ID.");
            return; // Exit the method if input is invalid
        }

        // Call the UnMatchVolunteerToCall function
        try
        {
            s_bl.Volunteer!.UnMatchVolunteerToCall(volunteerId, callId);
            Console.WriteLine("The volunteer has been successfully unmatched from the call.");
        }
        catch (BO.NotFoundException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (BO.DataAccessException ex)
        {
            Console.WriteLine($"Data access error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
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
                    "to get the amount of calls prss 1\n"
                    "to create a new call press 2\n" +
                    "to read a cal's details press 3\n" +
                    "to read all calls' details press 4\n" +
                    "to update a call's datails press 5\n" +
                    "to delete a call press 6\n" +
                    "to read all closed calls press 7\n"+
                    "to read all open calls press 8\n"+
                    "to update a call cancelation press 9\n"+
                    "to end a call press 10\n"+
                    "to choose a call for treatment press 11\n"
                    );
                choice = (SpecificOptions)Enum.Parse(typeof(SpecificOptions), Console.ReadLine()!);
                switch (choice)
                {
                    case SpecificOptions.EXIT:
                        break;
                    case SpecificOptions.CALLCOUNT:
                        callCount();
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
                    case SpecificOptions.CLOSEDCALLS:
                        closedCalls();
                        break;
                    case SpecificOptions.OPENCALLS:
                        openCalls();
                        break;
                    case SpecificOptions.UPDATECALLCANCELATION:
                        updateCallCancelation();
                        break;
                    case SpecificOptions.UPDATEENDOFCALL:
                        updateEndOfcall();
                        break;
                    case SpecificOptions.ASSIGN_VOLUNTEER_TO_CALL:
                        ChooseACallForTreatment();
                        break;

                    //case SpecificOptions.DELETE_ALL:
                    //    callDeleteAll();
                    //    break;
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

    private static void callCount()
    {
        // Assuming the class name is CallService
        //CallService callService = new CallService(); // Create an instance of the class
        // currentcall=new BO.Call();
        IEnumerable<int> callsCount = s_bl.GetCallsCount(); // Call the method

        // Optionally, you can iterate through the results to display them
        foreach (var count in callsCount)
        {
            Console.WriteLine(count);
        }
    }

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
        DateTime? maxClosingTime = DateTime.TryParse(maxClosingTimeInput, out DateTime parsedMaxClosingTime) ? parsedMaxClosingTime : (DateTime?)null;

        s_bl.Call!.Create(new BO.Call(0, convertedType, description, address, null, null,  s_bl.Admin.Clock(), maxClosingTime, BO.CallStatus.OPEN));
    }

    private static void callRead()
    {
        Console.WriteLine("Enter an ID");
        if (!int.TryParse(Console.ReadLine(), out int id))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer.");

        BO.Call call = s_bl.Call!.GetCallDetails(id)!;
        Console.WriteLine(call); // Display the volunteer details
    }

    public static void callReadAll()
    {
        // Prompt user for filter options
        Console.WriteLine("Do you want to filter by a specific field? (yes/no)");
        string filterResponse = Console.ReadLine()?.ToLower();

        Enum? filterBy = null;
        object? filterValue = null;

        if (filterResponse == "yes")
        {
            Console.WriteLine("Select a filter field (STATUS, PRIORITY, TYPE):");
            string filterFieldInput = Console.ReadLine();
            filterBy = Enum.TryParse<BO.CallField>(filterFieldInput, out var filterField) ? filterField : null;

            Console.WriteLine("Enter the filter value:");
            filterValue = Console.ReadLine(); // Adjust parsing based on expected type
        }

        // Prompt user for sorting options
        Console.WriteLine("Do you want to sort the results? (yes/no)");
        string sortResponse = Console.ReadLine()?.ToLower();

        Enum? sortBy = null;

        if (sortResponse == "yes")
        {
            Console.WriteLine("Select a sort field (ADDRESS, CALL_VOLUNTEER_DISTANCE, ID):");
            string sortFieldInput = Console.ReadLine();
            sortBy = Enum.TryParse<BO.CallField>(sortFieldInput, out var sortField) ? sortField : null;
        }

        // Call the existing ReadAll method with user inputs
        IEnumerable<BO.CallInList> callList = s_bl.Call!.ReadAll(filterBy, filterValue,sortBy);
        foreach (var c in callList)
        {
            Console.WriteLine(c);
        }
    }

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

        Console.WriteLine("Enter the latitude:");
        if (!double.TryParse(Console.ReadLine(), out double latitude))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid double.");

        Console.WriteLine("Enter the longitude:");
        if (!double.TryParse(Console.ReadLine(), out double longitude))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid double.");

        Console.WriteLine("Enter the opening time (yyyy-mm-dd hh:mm:ss, or leave blank):");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime openingTime))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid date and time.");

        Console.WriteLine("Enter the max closing time (yyyy-mm-dd hh:mm:ss, or leave blank):");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime maxClosingTime))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid date and time.");

        Console.WriteLine("Enter the status of the call (e.g., OPEN, CLOSED):");
        string statusInput = Console.ReadLine()!;
        BO.CallStatus status = (BO.CallStatus)Enum.Parse(typeof(BO.CallStatus), statusInput);

        // Assuming s_bl.Call is the service layer for managing calls
        s_bl.Call!.Update(new BO.Call(id, typeOfCall, description, address, latitude, longitude, openingTime, maxClosingTime, status));
    }

    private static void callDelete()
    {
        Console.WriteLine("Enter the call ID");
        if (!int.TryParse(Console.ReadLine(), out int id))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer.");
        if (s_bl.Call!.Read(id) != null)
            s_bl.Call!.Delete(id);
        else
            throw new BlUnauthorizedOperationException("An object with this ID does not exist.");
    }

    private static void closedCalls()
    {
        // Get volunteer ID from user
        Console.WriteLine("Enter your volunteer ID:");
        if (!int.TryParse(Console.ReadLine(), out int volunteerId))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer for the volunteer ID.");

        // Get sorting criteria from user
        Console.WriteLine("Enter sorting criteria (e.g., Name, Address):");
        string sortByInput = Console.ReadLine();
        Enum? sortBy = ConvertToSortByEnum(sortByInput); // Implement this method based on your Enum

        // Call the method to get closed calls
        var closedCalls = s_bl.GetClosedCallsHandledByTheVolunteer(volunteerId, sortBy);

        // Print all closed calls
        foreach (var call in closedCalls)
        {
            Console.WriteLine(call);
        }
    }
    private static void openCalls()
    {
        // Get volunteer ID from user
        Console.WriteLine("Enter your volunteer ID:");
        if (!int.TryParse(Console.ReadLine(), out int volunteerId))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer for the volunteer ID.");

        // Get sorting criteria from user
        Console.WriteLine("Enter sorting criteria (e.g., Name, Address):");
        string sortByInput = Console.ReadLine();
        Enum? sortBy = ConvertToSortByEnum(sortByInput); // Implement this method based on your Enum

        // Call the method to get closed calls
        var openCalls = s_bl.GetOpenCallsCanBeSelectedByAVolunteer(volunteerId, sortBy);

        // Print all closed calls
        foreach (var call in openCalls)
        {
            Console.WriteLine(call);
        }
    }
    private static void updateCallCancelation()
    {
        Console.WriteLine("Enter your volunteer ID:");
        if (!int.TryParse(Console.ReadLine(), out int volunteerId))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer for the volunteer ID.");

        // Get assignment ID from user
        Console.WriteLine("Enter the assignment ID:");
        if (!int.TryParse(Console.ReadLine(), out int assignmentId))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer for the assignment ID.");

        // Call the TreatmentCancellationUpdate method
        s_bl.TreatmentCancellationUpdate(volunteerId, assignmentId);

        Console.WriteLine("Treatment has been successfully canceled.");
    }

    private static void updateEndOfcall()
    {
        Console.WriteLine("Enter your volunteer ID:");
        if (!int.TryParse(Console.ReadLine(), out int volunteerId))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer for the volunteer ID.");

        // Get assignment ID from user
        Console.WriteLine("Enter the assignment ID:");
        if (!int.TryParse(Console.ReadLine(), out int assignmentId))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer for the assignment ID.");

        // Call the TreatmentCompletionUpdate method
        s_bl.TreatmentCompletionUpdate(volunteerId, assignmentId);

        Console.WriteLine("Treatment has been successfully marked as completed.");
    }

    private static void ChooseACallForTreatment()
    {
        // Get volunteer ID from user
        Console.WriteLine("Enter your volunteer ID:");
        if (!int.TryParse(Console.ReadLine(), out int volunteerId))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer for the volunteer ID.");

        // Get call ID from user
        Console.WriteLine("Enter the call ID:");
        if (!int.TryParse(Console.ReadLine(), out int callId))
            throw new BlUnauthorizedOperationException("Invalid input. Please enter a valid integer for the call ID.");

        // Call the ChoosingACallForTreatment method
        s_bl.ChoosingACallForTreatment(volunteerId, callId);

        Console.WriteLine("The call has been successfully assigned for treatment.");
    }


    /// <summary>
    /// Handles user login for both managers and volunteers.
    /// </summary>
    private static void Login()
    {
        Console.WriteLine("Enter your ID:");
        if (!int.TryParse(Console.ReadLine(), out int userId))
        {
            Console.WriteLine("Invalid ID. Please enter a valid integer.");
            return;
        }

        Console.WriteLine("Enter your password (leave blank if not applicable):");
        string password = Console.ReadLine();

        // Validate user credentials (you will need to implement this method)
        BO.Volunteer user = s_bl.Volunteer.Read(userId);
        if (user == null || (password != null && user.Password != password))
        {
            Console.WriteLine("Invalid credentials. Please try again.");
            return;
        }

        // Determine user type and redirect accordingly
        if (user.Role == BO.Role.STANDARD)
        {
            Console.WriteLine("Welcome to the Volunteer Screen!");
            // Call method to display volunteer screen
            DisplayVolunteerScreen();
        }
        else if (user.Role == BO.Role.ADMINISTRATOR)
        {
            Console.WriteLine("Welcome to the Manager Screen! Choose your next step:");
            Console.WriteLine("1. Go to Volunteer Screen");
            Console.WriteLine("2. Go to Main Management Screen");
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                // Call method to display volunteer screen
                volunteerMenu();
            }
            else if (choice == "2")
            {
                // Call method to display main management screen
                callMenue();
            }
            else
            {
                Console.WriteLine("Invalid choice. Returning to login.");
            }
        }
    }

    /// <summary>
    /// The main entry point of the program.
    /// </summary>
    /// <param name="args">Command-line arguments.</param>
    static void Main(string[] args)
    {
        try
        {
            // Start the login process
            Login();
        }
        catch (BlUnauthorizedOperationException ex)
        {
            Console.WriteLine($"Unauthorized operation: {ex.Message}");
        }
        catch (BO.NotFoundException ex)
        {
            Console.WriteLine($"Not found: {ex.Message}");
        }
        catch (BO.DataAccessException ex)
        {
            Console.WriteLine($"Data access error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}