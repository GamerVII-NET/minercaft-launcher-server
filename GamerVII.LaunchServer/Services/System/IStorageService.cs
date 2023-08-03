namespace GamerVII.LaunchServer.Services.System;

public interface IStorageService
{
    /// <summary>
    /// Represents the base directory for storage operations.
    /// </summary>
    public string BaseDirectory { get; set; }

    /// <summary>
    /// Represents the client directory for storage operations.
    /// </summary>
    public string ClientDirectory { get; set; }

    /// <summary>
    /// Asynchronously creates a directory with the specified relative path.
    /// </summary>
    /// <param name="relativePath">The relative path of the directory to create.</param>
    /// <returns>A task representing the asynchronous operation that returns the created directory's path.</returns>
    Task<string> CreateDirectoryAsync(string relativePath);

    /// <summary>
    /// Asynchronously checks if a directory with the specified relative path exists.
    /// </summary>
    /// <param name="relativePath">The relative path of the directory to check.</param>
    /// <returns>A task representing the asynchronous operation that returns true if the directory exists; otherwise, false.</returns>
    Task<bool> CheckDirectoryExistsAsync(string relativePath);

    /// <summary>
    /// Asynchronously creates a JSON file with the given content of type T.
    /// </summary>
    /// <typeparam name="T">The type of data to serialize and store in the JSON file.</typeparam>
    /// <param name="fileName">The name of the JSON file to create.</param>
    /// <param name="template">The content template to use for serialization.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task<T> CreateJsonFileAsync<T>(string fileName, string template);

    /// <summary>
    /// Asynchronously reads the content of a JSON file with the specified relative path and deserializes it to type T.
    /// </summary>
    /// <typeparam name="T">The type of data to deserialize from the JSON file.</typeparam>
    /// <param name="relativePath">The relative path of the JSON file to read.</param>
    /// <returns>A task representing the asynchronous operation that returns the deserialized data of type T, or null if the file does not exist.</returns>
    Task<T?> ReadJsonFileAsync<T>(string relativePath);

    /// <summary>
    /// Asynchronously deletes a folder with the specified relative path.
    /// </summary>
    /// <param name="relativePath">The relative path of the folder to delete.</param>
    /// <returns>A task representing the asynchronous operation that returns true if the folder is successfully deleted; otherwise, false.</returns>
    Task<bool> DeleteFolder(string relativePath);

    /// <summary>
    /// Asynchronously reads the content of a file with the specified relative path.
    /// </summary>
    /// <param name="relativePath">The relative path of the file to read.</param>
    /// <returns>A task representing the asynchronous operation that returns the content of the file as a string, or null if the file does not exist.</returns>
    Task<string?> ReadFile(string relativePath);

    /// <summary>
    /// Asynchronously retrieves a collection of DirectoryInfo objects representing folders in the base directory.
    /// </summary>
    /// <returns>A task representing the asynchronous operation that returns a collection of DirectoryInfo objects.</returns>
    Task<IEnumerable<DirectoryInfo>> GetFolders();

    /// <summary>
    /// Asynchronously retrieves the DirectoryInfo object for a specific client folder by name.
    /// </summary>
    /// <param name="name">The name of the client folder to retrieve.</param>
    /// <returns>A task representing the asynchronous operation that returns the DirectoryInfo object for the specified client folder.</returns>
    Task<DirectoryInfo> GetClientFolder(string name);
    /// <summary>
    /// Retrieves a task that asynchronously gets the DirectoryInfo object for the specified folder with the given name.
    /// </summary>
    /// <param name="name">The name of the folder for which to retrieve information.</param>
    /// <returns>A task that represents the operation, and upon completion, contains the DirectoryInfo object representing the specified folder.</returns>

    Task<DirectoryInfo> GetFolder(string name);
}