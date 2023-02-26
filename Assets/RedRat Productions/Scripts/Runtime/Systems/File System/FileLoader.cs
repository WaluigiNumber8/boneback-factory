using System.Collections.Generic;
using System.IO;
using RedRats.Safety;

namespace RedRats.Systems.FileSystem
{
    /// <summary>
    /// Loads a list of JSON files.
    /// </summary>
    public class FileLoader
    {
        private readonly IList<string> dataList;
        private string searchedExtension;
        private bool deepSearchEnabled;

        public FileLoader() => dataList = new List<string>();

        /// <summary>
        /// Loads all files under a specific path.
        /// </summary>
        /// <param name="path">The root path to start loading from.</param>
        /// <param name="extension">The extension of files to take into consideration.</param>
        /// <param name="deepSearch">If enabled, will also search all subdirectories.</param>
        /// <returns>A list where each element being file data represented by a string.</returns>
        public IList<string> LoadAllFiles(string path, string extension, bool deepSearch = false)
        {
            SafetyNetIO.EnsureDirectoryExists(path);

            dataList.Clear();
            searchedExtension = extension;
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
                    if (file.Extension != searchedExtension) continue;
                
                    string filePath = Path.Combine(path, file.Name);
                    string data = File.ReadAllText(filePath);
                    dataList.Add(data);
                }
                catch (IOException)
                {
                    SafetyNetIO.ThrowMessage($"The file '{file.DirectoryName}' could not be loaded.");
                }
            } 
        }
    }
}