using DalApi;

namespace Helpers;

/// <summary>
/// Provides assignment management operations in the BL layer.
/// </summary>
internal static class AssignmentManager
{
    /// <summary>
    /// Gets or sets the data access layer instance.
    /// </summary>
    /// <value>The data access layer instance.</value>
    private static IDal s_dal = Factory.Get; //stage 4

    /// <summary>
    /// Gets or sets the observer manager instance.
    /// </summary>
    internal static ObserverManager Observers = new(); //stage 5 
}
