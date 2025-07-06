partial /// <summary>
/// Entry point for the Stage0 Program1515 project.
/// </summary>
class Program1515
{
    private static void Main(string[] args)
    {
        /// <summary>
        /// Displays a welcome message to the user.
        /// </summary>
        welcome1515();
        /// <summary>
        /// Displays a welcome message to the user (partial method).
        /// </summary>
        welcome8245();
        Console.ReadKey();
    }
    static partial void welcome8245();
    private static void welcome1515()
    {
        Console.WriteLine("Enter your name: ");
        string name = Console.ReadLine();
        Console.WriteLine("{0}, welcome to my first console application", name);
    }
}