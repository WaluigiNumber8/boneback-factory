using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using RedRats.Safety;
using UnityEngine;

namespace RedRats.Systems.FileSystem.JSON
{
    /// <summary>
    /// Saves, loads and removes data from external storage using the <see cref="JsonUtility"/>.
    /// </summary>
    public static class FileSystem
    {
        public const string JSON_EXTENSION = ".json";
        
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
        /// <param name="identifier">The unique identifier, differentiating data stored in the JSON file.</param>
        /// <param name="data">The data object to save.</param>
        /// <param name="newJSONFormat">Function that will create a data version of the object.</param>
        /// <typeparam name="T">The object to convert to JSON.</typeparam>
        /// <typeparam name="TS">The data class to convert to JSON.</typeparam>
        public static void SaveFile<T,TS>(string path, string identifier, T data, Func<T, TS> newJSONFormat)
        {
            path += JSON_EXTENSION;
            SafetyNetIO.EnsurePathNotContainsInvalidCharacters(path);

            try
            {
                StringBuilder JSONFormat = new();
                JSONFormat.Append(identifier.Length).Append(identifier).Append(JsonUtility.ToJson(newJSONFormat(data)));
                File.WriteAllText(path, JSONFormat.ToString());
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
        public static T LoadFile<T, TS>(string path, string identifier) where TS : IEncodedObject<T>
        {
            path += JSON_EXTENSION;
            SafetyNetIO.EnsureFileExists(path);
            try
            {
                string allData = File.ReadAllText(path);
                string id = allData.Substring(1, allData[0]);

                if (id != identifier) throw new InvalidDataException($"The file is not of the required type ({id} != {identifier})");
                    
                TS data = JsonUtility.FromJson<TS>(allData[(allData[0]-1)..]);
                return data.Decode();
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
        /// <param name="identifier">The unique identifier, differentiating data between JSON assets.</param>
        /// <param name="deepSearch">If enabled, will also search all subdirectories.</param>
        /// <typeparam name="T">Unity readable Asset.</typeparam>
        /// <typeparam name="TS">Serialized form of the Asset.</typeparam>
        public static IList<T> LoadAllFiles<T, TS>(string path, string identifier, bool deepSearch = false) where TS : IEncodedObject<T>
        {
            SafetyNetIO.EnsurePathNotContainsInvalidCharacters(path);
            return new FileLoader<T,TS>().LoadAllFiles(path, identifier, deepSearch);
        }

        /// <summary>
        /// Removes a file under a specific path. (No extension)
        /// </summary>
        /// <param name="path">The path to the file.</param>
        public static void DeleteFile(string path)
        {
            path += JSON_EXTENSION;
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
            oldPath += JSON_EXTENSION;
            newPath += JSON_EXTENSION;
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
            SafetyNetIO.EnsurePathNotContainsInvalidCharacters(newPath);
            SafetyNetIO.EnsureDirectoryExists(oldPath);
            Directory.Move(oldPath, newPath);
        }
    }
}