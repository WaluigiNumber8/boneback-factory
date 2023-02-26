using System.Collections.Generic;
using System.IO;
using RedRats.Safety;

namespace RedRats.Systems.FileSystem.JSON
{
    /// <summary>
    /// Saves, reads and removes files and directories.
    /// </summary>
    public static class FileSystem
    {
        private static readonly FileLoader loader = new();

        /// <summary>
        /// If it doesn't exist, creates a directory at specific path.
        /// </summary>
        /// <param name="path">The location to create the directory in.</param>
        /// <param name="name">The name of the directory.</param>
        public static void CreateDirectory(string path, string name)
        {
            CreateDirectory(Path.Combine(path, name));
        }

        /// <summary>
        /// If it doesn't exist, creates a directory at specific path.
        /// </summary>
        /// <param name="path">The location to create the directory in. (including directory title)</param>
        public static void CreateDirectory(string path)
        {
            SafetyNetIO.EnsurePathNotContainsInvalidCharacters(path);
            if (Directory.Exists(path)) return;
            Directory.CreateDirectory(path);
        }

        /// <summary>
        /// Save a file to external storage.
        /// </summary>
        /// <param name="path">Destination of the save. (without extension)</param>
        /// <param name="data">The string of data to save.</param>
        public static void SaveFile(string path, string data)
        {
            SafetyNetIO.EnsurePathNotContainsInvalidCharacters(path);
            try
            {
                File.WriteAllText(path, data);
            }
            catch (IOException)
            {
                SafetyNetIO.ThrowMessage($"File '{Path.GetFileName(path)} could not be saved.'");
            }
        }

        /// <summary>
        /// Load a file under a specific path.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <typeparam name="T">Any type.</typeparam>
        /// <typeparam name="TS">A Serialized form of <see cref="T"/>.</typeparam>
        /// <returns>A deserialized form of the object.</returns>
        /// <exception cref="IOException">IS thrown when the object could not be loaded.</exception>
        public static string LoadFile<T, TS>(string path) where TS : IEncodedObject<T>
        {
            SafetyNetIO.EnsurePathNotContainsInvalidCharacters(path);
            SafetyNetIO.EnsureFileExists(path);
            try
            {
                string allData = File.ReadAllText(path);
                return allData;
            }
            catch (IOException)
            {
                throw new IOException($"File under the path '{path}' could not be loaded.");
            }
        }

        /// <summary>
        /// Loads all objects under a path.
        /// </summary>
        /// <param name="path">Destination of the data.</param>
        /// <param name="extension">The extension of files to take into consideration.</param>
        /// <param name="deepSearch">If enabled, will also search all subdirectories.</param>
        public static IList<string> LoadAllFiles(string path, string extension, bool deepSearch = false)
        {
            SafetyNetIO.EnsurePathNotContainsInvalidCharacters(path);
            return loader.LoadAllFiles(path, extension, deepSearch);
        }

        /// <summary>
        /// Removes a file under a specific path. (No extension)
        /// </summary>
        /// <param name="path">The path to the file.</param>
        public static void DeleteFile(string path)
        {
            SafetyNetIO.EnsurePathNotContainsInvalidCharacters(path);
            SafetyNetIO.EnsureFileExists(path);
            File.Delete(path);
        }

        /// <summary>
        /// Removes a directory and all it's contents under a specific path.
        /// </summary>
        /// <param name="path">The path to the directory.</param>
        public static void DeleteDirectory(string path)
        {
            SafetyNetIO.EnsurePathNotContainsInvalidCharacters(path);
            SafetyNetIO.EnsureDirectoryExists(path);
            Directory.Delete(path, true);
        }

        /// <summary>
        /// Renames a specific file.
        /// </summary>
        /// <param name="oldPath">The full path of the old file. (no extension)</param>
        /// <param name="newPath">The full path of the new file. (no extension)</param>
        public static void RenameFile(string oldPath, string newPath)
        {
            SafetyNetIO.EnsurePathNotContainsInvalidCharacters(oldPath);
            SafetyNetIO.EnsurePathNotContainsInvalidCharacters(newPath);
            SafetyNetIO.EnsureFileExists(oldPath);
            File.Move(oldPath, newPath);
        }

        /// <summary>
        /// Renames a specific directory.
        /// </summary>
        /// <param name="oldPath">The full path of the old directory.</param>
        /// <param name="newPath">The full path of the new directory.</param>
        public static void RenameDirectory(string oldPath, string newPath)
        {
            SafetyNetIO.EnsurePathNotContainsInvalidCharacters(oldPath);
            SafetyNetIO.EnsurePathNotContainsInvalidCharacters(newPath);
            SafetyNetIO.EnsureDirectoryExists(oldPath);
            Directory.Move(oldPath, newPath);
        }
    }
}
