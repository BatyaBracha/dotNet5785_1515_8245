using BO;
using DalApi;
using DO;

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
                    "to read all volunteers' details press 3\n" +
                    "to update a volunteer's details press 4\n" +
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
        IEnumerable<BO.VolunteerInList> volunteerList = s_bl.Volunteer!.ReadAll();
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
    /// Deletes all volunteers from the database.
    /// </summary>
    private static void volunteerDeleteAll()
    {
        s_bl.Volunteer!.DeleteAll();
    }

    /// <summary>
    /// The main entry point of the program.
    /// </summary>
    /// <param name="args">Command-line arguments.</param>
    static void Main(string[] args)
    {
        try
        {
            Options choice = Options.EXIT;
            do
            {
                Console.WriteLine("Enter your choice:\n" +
                    "to exit press 0\n" +
                    "to the volunteer menu press 1\n" +
                    "to the assignment menu press 2\n" +
                    "to the call menu press 3\n" +
                    "to initialize the system press 4\n" +
                    "to print all the data press 5\n" +
                    "to the configuration menu press 6\n" +
                    "to reset the database and the configuration press 7.");
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