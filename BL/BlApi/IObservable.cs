namespace BlApi;

/// <summary>
/// This interface provides actions to register (add) and unregister (remove) observers
/// for changes in a list of entities and in a speecific entity
/// </summary>
/// <summary>
/// Interface for observable pattern support in the BL layer.
/// </summary>
/// <summary>
/// Defines a provider for notifications to observers.
/// </summary>
/// <summary>
/// Interface for observable pattern implementation in the business logic layer.
/// </summary>
public interface IObservable //stage 5
{
    /// <summary>
    /// Register observer for changes in a list of entities
    /// </summary>
    /// <param name="listObserver">the observer method to be registered</param>
    /// <summary>
    /// Adds an observer to receive notifications.
    /// </summary>
    /// <param name="observer">The observer to add.</param>
    void AddObserver(Action listObserver);
    /// <summary>
    /// Register observer for changes in a specific entity instance
    /// </summary>
    /// <param name="id">the identifier of the entity instance to be observed</param>
    /// <param name="observer">the observer method to be registered</param>
    void AddObserver(int id, Action observer);
    /// <summary>
    /// Unregister observer for changes in a list of entities
    /// </summary>
    /// <param name="listObserver">the observer method to be unregistered</param>
    /// <summary>
    /// Removes an observer from receiving notifications.
    /// </summary>
    /// <param name="observer">The observer to remove.</param>
    void RemoveObserver(Action listObserver);
    /// <summary>
    /// Unregister observer for changes in a specific entity instance
    /// </summary>
    /// <param name="id">the identifier of the entity instance that was observed</param>
    /// <param name="observer">the observer method to be unregistered</param>
    void RemoveObserver(int id, Action observer);
}