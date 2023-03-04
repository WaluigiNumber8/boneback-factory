using System.Collections.Generic;
using System.Text;
using RedRats.Core;
using RedRats.Systems.FileSystem;
using RedRats.Systems.FileSystem.Compression;
using RedRats.Systems.FileSystem.JSON;
using Unity.Plastic.Newtonsoft.Json.Serialization;
using UnityEngine;

namespace Rogium.ExternalStorage
{
    /// <summary>
    /// Encodes/Decodes data into a JSON format.
    /// Communicates with the <see cref="FileSystem"/> and <see cref="FileLoader"/>.
    /// </summary>
    public static class JSONSystem
    {
        private const string JSON_EXTENSION = ".json";
        
        /// <summary>
        /// Save a file to external storage in a compressed JSON format.
        /// </summary>
        /// <param name="path">Destination of the save. (without extension)</param>
        /// <param name="identifier">The unique identifier, differentiating data stored in the JSON file.</param>
        /// <param name="data">The data object to save.</param>
        /// <param name="newJSONFormat">Function that will create a data version of the object.</param>
        /// <typeparam name="T">The object to convert to JSON.</typeparam>
        /// <typeparam name="TS">The data class to convert to JSON.</typeparam>
        public static void Save<T,TS>(string path, string identifier, T data, Func<T, TS> newJSONFormat)
        {
            path += JSON_EXTENSION;
            
            StringBuilder JSONFormat = new();
            JSONFormat.Append(identifier.Length).Append(identifier).Append(JsonUtility.ToJson(newJSONFormat(data)));
            FileSystem.SaveFile(path, JSONFormat.ToString(), new DFLCompression());
        }

        /// <summary>
        /// Loads all JSON files under a directory path.
        /// </summary>
        /// <param name="path">Destination of the data.</param>
        /// <param name="identifier">Load only JSON files with this specific identifier.</param>
        /// <param name="deepSearch">If enabled, will also search all subdirectories.</param>
        /// <typeparam name="T">Unity readable Asset.</typeparam>
        /// <typeparam name="TS">Serialized form of the Asset.</typeparam>
        public static IList<T> LoadAll<T,TS>(string path, string identifier, bool deepSearch = false) where TS : IEncodedObject<T>
        {
            IList<string> data = FileSystem.LoadAllFiles(path, JSON_EXTENSION, deepSearch, new DFLCompression());
            IList<T> objects = new List<T>();
            foreach (string line in data)
            {
                if (!HasSameIdentifier(line, identifier)) continue;
                
                int lengthToCut = StringUtils.GrabIntFrom(line, 0) + 1;
                string jsonText = line[lengthToCut..];
                TS obj = JsonUtility.FromJson<TS>(jsonText);
                objects.Add(obj.Decode());
            }

            return objects;
        }

        /// <summary>
        /// Removes a file under a specific path.
        /// </summary>
        /// <param name="path">The path to the file. (No extension)</param>
        public static void Delete(string path)
        {
            path += JSON_EXTENSION;
            FileSystem.DeleteFile(path);
        }

        /// <summary>
        /// Checks if a data string contains the same identifier.
        /// </summary>
        /// <param name="allData">The data to check.</param>
        /// <param name="identifier">The identifier to check for.</param>
        /// <returns>TRUE if it contains the same identifier.</returns>
        private static bool HasSameIdentifier(string allData, string identifier)
        {
            if (string.IsNullOrEmpty(allData)) return false;
            if (!StringUtils.TryGrabIntFrom(allData, out int idLength, 0)) return false;
            if (identifier.Length != idLength) return false;
            if (identifier != allData.Substring(1, idLength)) return false;
            return true;
        }
    }
}