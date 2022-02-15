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
        /// Save an asset to external storage.
        /// </summary>
        /// <param name="path">Destination of the save.</param>
        /// <param name="data">The data object to save.</param>
        /// <param name="newSerializedObject">A method that will create the create the serialized form of the object.</param>
        /// <typeparam name="T">The object to serialize.</typeparam>
        /// <typeparam name="TS">Serialized form of the object.</typeparam>
        public static void Save<T,TS>(string path, T data, Func<T,TS> newSerializedObject)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);
            TS formattedAsset = newSerializedObject(data);

            formatter.Serialize(stream, formattedAsset);
            stream.Close();
        }

        /// <summary>
        /// Loads all objects under a path.
        /// </summary>
        /// <param name="path">Destination of the data.</param>
        /// <param name="typeExtension">The extension of the files that will be read.</param>
        /// <typeparam name="T">Unity readable Asset.</typeparam>
        /// <typeparam name="TS">Serialized form of the Asset.</typeparam>
        public static IList<T> LoadAll<T, TS>(string path, string typeExtension) where TS : ISerializedObject<T>
        {
            SafetyNetIO.EnsureDirectoryExists(path);

            IList<T> dataList = new List<T>();

            BinaryFormatter formatter = new BinaryFormatter();
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] files = directory.GetFiles();

            foreach (FileInfo file in files)
            {
                //If file does not have the type we want, skip it.
                string extension = Path.GetExtension(file.Name).Replace(".", "");
                if (extension != typeExtension) continue;
                
                string filePath = Path.Combine(path, file.Name);
                FileStream stream = new FileStream(filePath, FileMode.Open);
                
                TS asset = (TS)formatter.Deserialize(stream);
                dataList.Add(asset.Deserialize());

                stream.Close();
            } 

            return dataList;
        }

        /// <summary>
        /// Removes a file under a specific path.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        public static void DeleteFile(string path)
        {
            SafetyNetIO.EnsureFileExists(path);
            File.Delete(path);
        }
        
        /// <summary>
        /// Removes a directory under a specific path.
        /// </summary>
        /// <param name="path">The path to the directory.</param>
        public static void DeleteDirectory(string path)
        {
            SafetyNetIO.EnsureDirectoryExists(path);
            Directory.Delete(path);
        }
    }
}