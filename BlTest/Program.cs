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
                    "to assign a volunteer to a call press 7\n" +
                    "to unmatch a volunteer from a call press 8");
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
                    case SpecificOptions.ASSIGN_VOLUNTEER_TO_CALL:
                        AssignVolunteerToCall();
                        break;
                    case SpecificOptions.UNMATCH_VOLUNTEER_FROM_CALL:
                        UnmatchVolunteerFromCall();
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
                    "to create a new call press 1\n" +
                    "to read a cal's details press 2\n" +
                    "to read all calls' details press 3\n" +
                    "to update a call's datails press 4\n" +
                    "to delete a call press 5\n" +
                    "to delete all calls press 6");
                choice = (SpecificOptions)Enum.Parse(typeof(SpecificOptions), Console.ReadLine()!);
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

        BO.Call call = s_bl.Call!.Read(id)!;
        Console.WriteLine(call); // Display the volunteer details
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