

namespace BO;

/// <summary>
/// Represents a volunteer in a summarized list context.
/// </summary>
/// <summary>
/// Represents a volunteer entry in a list.
/// </summary>
public class VolunteerInList
{
    /// <summary>
/// The unique identifier for the volunteer.
/// </summary>
        /// <summary>
        /// Gets or sets the unique ID of the volunteer.
        /// </summary>
        public int Id { get; set; }
    /// <summary>
    /// Gets or sets the unique ID of the volunteer.
    /// </summary>
    /// <value>The unique identifier for the volunteer.</value>
    //public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the volunteer.
    /// </summary>
    /// <value>The name of the volunteer.</value>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the phone number of the volunteer.
    /// </summary>
    /// <value>The phone number of the volunteer.</value>
    public string? Phone { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the volunteer is active.
    /// </summary>
    /// <value><c>true</c> if the volunteer is active; otherwise, <c>false</c>.</value>
    public bool Active { get; set; }

    /// <summary>
    /// Gets or sets the number of calls successfully handled by the volunteer.
    /// </summary>
    /// <value>The number of calls done by the volunteer.</value>
    public int CallsDone { get; set; }

    /// <summary>
    /// Gets or sets the number of calls canceled by the volunteer.
    /// </summary>
    /// <value>The number of calls canceled by the volunteer.</value>
    public int CallsCanceled { get; set; }

    /// <summary>
    /// Gets or sets the number of calls that became out of date for the volunteer.
    /// </summary>
    /// <value>The number of calls out of date for the volunteer.</value>
    public int CallsOutOfDate { get; set; }

    /// <summary>
    /// Gets or sets the ID of the call currently assigned to the volunteer, if any.
    /// </summary>
    /// <value>The ID of the call currently assigned to the volunteer.</value>
    public int? CallId { get; set; }

    /// <summary>
    /// Gets or sets the type of call currently assigned to the volunteer.
    /// </summary>
    /// <value>The type of call currently assigned to the volunteer.</value>
    public TypeOfCall TypeOfCall { get; set; }

    /// <summary>
    /// Gets or sets the type of distance for the volunteer.
    /// </summary>
    /// <value>The type of distance for the volunteer.</value>
    public TypeOfDistance TypeOfDistance { get; set; }

    /// <summary>
    /// Returns a string representation of the volunteer in the list.
    /// </summary>
    /// <returns>A string representation of the volunteer.</returns>
    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, Phone: {Phone}, " +
               $"Active: {Active}, " +
               $"Calls Handeled: {CallsDone}, " +
               $"Calls Canceled: {CallsCanceled}, CallsOutOfDate: {CallsOutOfDate}, CallId: {CallId}, " +
               $"TypeOfCall: {TypeOfCall}";
    }

}
