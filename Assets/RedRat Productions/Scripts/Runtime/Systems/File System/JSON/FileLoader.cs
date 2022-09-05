using System.Collections.Generic;
using System.IO;
using RedRats.Core;
using RedRats.Safety;
using UnityEngine;

namespace RedRats.Systems.FileSystem.JSON
{
    /// <summary>
    /// Loads a list of JSON files.
    /// </summary>
    /// <typeparam name="T">Any Type</typeparam>
    /// <typeparam name="TS">A Serialized form of <see cref="T"/>.</typeparam>
    public class FileLoader<T,TS> where TS : IEncodedObject<T>
    {
        private readonly List<T> dataList;
        private string typeIdentifier;
        private bool deepSearchEnabled;

        public FileLoader()
        {
            dataList = new List<T>();
        }

        /// <summary>
        /// Loads all files under a specific path.
        /// </summary>
        /// <param name="path">The root path to start loading from.</param>
        /// <param name="identifier">The identifier, differentiating data stored in the JSON file.</param>
        /// <param name="deepSearch">If enabled, will also search all subdirectories.</param>
        /// <returns>A list of deserialized objects.</returns>
        public IList<T> LoadAllFiles(string path, string identifier, bool deepSearch = false)
        {
            SafetyNetIO.EnsureDirectoryExists(path);

            dataList.Clear();
            typeIdentifier = identifier;
            deepSearchEnabled = deepSearch;

            SearchDirectory(new DirectoryInfo(path));

            return dataList;
        }

        /// <summary>
        /// Process files in a specific directory.
        /// </summary>
        /// <param name="directory">The directory to search.</param>
        private void SearchDirectory(DirectoryInfo directory)
        {
            ProcessFiles(directory.GetFiles(), directory.FullName);

            if (!deepSearchEnabled) return;
            
            DirectoryInfo[] directories = directory.GetDirectories();
            if (directories.Length <= 0) return;

            foreach (DirectoryInfo dir in directories)
            {
                SearchDirectory(dir);
            }
        }

        /// <summary>
        /// Load a list of files.
        /// </summary>
        /// <param name="files">The files to load.</param>
        /// <param name="path">The path of the files.</param>
        private void ProcessFiles(FileInfo[] files, string path)
        {
            if (files.Length <= 0) return;
            
            foreach (FileInfo file in files)
            {
                try
                {
                    if (!IsJSON(file)) continue;
                
                    string filePath = Path.Combine(path, file.Name);
                    string allData = File.ReadAllText(filePath);
                    
                    if (!HasSameIdentifier(allData)) continue;

                    int lengthToCut = StringUtils.GrabIntFrom(allData, 0) + 1;
                    string json = allData[lengthToCut..];
                    TS data = JsonUtility.FromJson<TS>(json);
                    dataList.Add(data.Decode());
                }
                catch (IOException)
                {
                    SafetyNetIO.ThrowMessage($"The file '{file.DirectoryName}' could not be loaded.");
                }
            } 
        }

        /// <summary>
        /// Check if file has the JSON extension. 
        /// </summary>
        /// <param name="file">The file whose extension to check.</param>
        /// <returns>True if extensions are the same.</returns>
        private bool IsJSON(FileSystemInfo file)
        {
            string fileExtension = Path.GetExtension(file.Name);
            return (fileExtension == FileSystem.JSON_EXTENSION);
        }

        /// <summary>
        /// Checks if a data string contains the same identifier.
        /// </summary>
        /// <param name="allData">The data to check.</param>
        /// <returns>TRUE if it contains the same identifier.</returns>
        private bool HasSameIdentifier(string allData)
        {
            if (string.IsNullOrEmpty(allData)) return false;
            if (!StringUtils.TryGrabIntFrom(allData, out int idLength, 0)) return false;
            if (typeIdentifier.Length != idLength) return false;
            if (typeIdentifier != allData.Substring(1, idLength)) return false;
            return true;
        }
    }
}