using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using BoubakProductions.Safety;
using BoubakProductions.Systems.FileSystem.Serialization;

namespace BoubakProductions.Systems.FileSystem
{
    /// <summary>
    /// Handles loading of files.
    /// </summary>
    /// <typeparam name="T">Any Type</typeparam>
    /// <typeparam name="TS">A Serialized form of <see cref="T"/>.</typeparam>
    public class FileLoader<T,TS> where TS : ISerializedObject<T>
    {
        private List<T> dataList;
        private string extension;
        private bool deepSearchEnabled;
        private BinaryFormatter formatter;

        public FileLoader()
        {
            dataList = new List<T>();
            formatter = new BinaryFormatter();
        }
        
        /// <summary>
        /// Loads all files under a specific path.
        /// </summary>
        /// <param name="path">The root path to start loading from.</param>
        /// <param name="typeExtension">The extension of the requested file type.</param>
        /// <param name="deepSearch">If enabled, will also search all subdirectories.</param>
        /// <returns>A list of deserialized objects.</returns>
        public IList<T> LoadAllFiles(string path, string typeExtension, bool deepSearch = false)
        {
            SafetyNetIO.EnsureDirectoryExists(path);

            dataList.Clear();
            extension = typeExtension;
            deepSearchEnabled = deepSearch;

            SearchDirectory(new DirectoryInfo(path));

            return dataList;
        }

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

        private void ProcessFiles(FileInfo[] files, string path)
        {
            if (files.Length <= 0) return;
            
            foreach (FileInfo file in files)
            {
                try
                {
                    if (!HasSameExtension(file)) continue;
                
                    string filePath = Path.Combine(path, file.Name);
                    FileStream stream = new FileStream(filePath, FileMode.Open);
                
                    TS deserializedObject = (TS)formatter.Deserialize(stream);
                    dataList.Add(deserializedObject.Deserialize());

                    stream.Close();
                }
                catch (IOException)
                {
                    SafetyNetIO.ThrowMessage($"The file '{file.DirectoryName}' could not be loaded.");
                }
                catch (SerializationException)
                {
                    SafetyNetIO.ThrowMessage($"The file '{file.DirectoryName}' was tampered without the application's consent and thus will not be loaded.");
                }
            } 
        }

        /// <summary>
        /// Check if file has the same extension as the requested one. 
        /// </summary>
        /// <param name="file">The file whose extension to check.</param>
        /// <returns>True if extensions are the same.</returns>
        private bool HasSameExtension(FileInfo file)
        {
            string fileExtension = Path.GetExtension(file.Name).Replace(".", "");
            return (fileExtension == extension);
        }
    }
}