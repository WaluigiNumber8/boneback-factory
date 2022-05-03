using System;
using BoubakProductions.Safety;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BoubakProductions.Systems.FileSystem.Serialization;

namespace BoubakProductions.Systems.FileSystem
{
    /// <summary>
    /// Handles Saving, Loading and Removing packs from external storage.
    /// </summary>
    public static class FileSystem
    {

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
        /// Save an asset to external storage.
        /// </summary>
        /// <param name="path">Destination of the save.</param>
        /// <param name="data">The data object to save.</param>
        /// <param name="newSerializedObject">A method that will create the create the serialized form of the object.</param>
        /// <typeparam name="T">The object to serialize.</typeparam>
        /// <typeparam name="TS">Serialized form of the object.</typeparam>
        public static void SaveFile<T,TS>(string path, T data, Func<T,TS> newSerializedObject)
        {
            SafetyNetIO.EnsurePathNotContainsInvalidCharacters(path);

            try
            {
                BinaryFormatter formatter = new();
                FileStream stream = new(path, FileMode.Create);
                TS formattedAsset = newSerializedObject(data);

                formatter.Serialize(stream, formattedAsset);
                stream.Close();
            }
            catch (IOException)
            {
                SafetyNetIO.ThrowMessage($"File '{Path.GetFileName(path)} could not be saved.'");
            }
        }

        /// <summary>
        /// Loads all objects under a path.
        /// </summary>
        /// <param name="path">Destination of the data.</param>
        /// <param name="typeExtension">The extension of the files that will be read.</param>
        /// <param name="deepSearch">If enabled, will also search all subdirectories.</param>
        /// <typeparam name="T">Unity readable Asset.</typeparam>
        /// <typeparam name="TS">Serialized form of the Asset.</typeparam>
        public static IList<T> LoadAllFiles<T, TS>(string path, string typeExtension, bool deepSearch = false) where TS : ISerializedObject<T>
        {
            SafetyNetIO.EnsurePathNotContainsInvalidCharacters(path);
            return new FileLoader<T, TS>().LoadAllFiles(path, typeExtension, deepSearch);
        }

        /// <summary>
        /// Removes a file under a specific path.
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
        /// <param name="oldPath">The full path of the old file.</param>
        /// <param name="newPath">The full path of the new file.</param>
        public static void RenameFile(string oldPath, string newPath)
        {
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