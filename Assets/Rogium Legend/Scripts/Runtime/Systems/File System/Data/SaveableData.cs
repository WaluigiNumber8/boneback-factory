using System;
using System.Collections.Generic;
using BoubakProductions.Core;
using BoubakProductions.Safety;
using Rogium.Core;
using UnityEngine;

namespace Rogium.ExternalStorage
{
    /// <summary>
    /// Contains variables for a saveable data type.
    /// </summary>
    public class SaveableData
    {
        private readonly string folderTitle;
        private readonly string extension;
        private string path;

        private readonly IList<FilePathInfo> filePaths;

        /// <summary>
        /// The Constructor.
        /// </summary>
        /// <param name="folderTitle">Name of the folder that will store the files.</param>
        /// <param name="extension">The extension of teh files.</param>
        public SaveableData(string folderTitle, string extension) : this(folderTitle, extension, Application.persistentDataPath) { }
        /// <summary>
        /// The Constructor.
        /// </summary>
        /// <param name="folderTitle">Name of the folder that will store the files.</param>
        /// <param name="extension">The extension of teh files.</param>
        /// <param name="path">The root location of the folder.</param>
        public SaveableData(string folderTitle, string extension, string path)
        {
            this.folderTitle = folderTitle;
            this.extension = extension;
            UpdatePath(path);
            
            filePaths = new List<FilePathInfo>();
        }

        /// <summary>
        /// Changes the path to read from.
        /// </summary>
        /// <param name="newPath">The new directory path to read from.</param>
        public void UpdatePath(string newPath)
        {
            path = System.IO.Path.Combine(newPath, folderTitle);
        }

        /// <summary>
        /// Register a new file.
        /// </summary>
        /// <param name="id">The ID of the file.</param>
        /// <param name="title">The title of the file.</param>
        public void AddFilePath(string id, string title)
        {
            FilePathInfo filePathInfo = ConvertToFileInfo(id, title);
            filePaths.Add(filePathInfo);
        }

        /// <summary>
        /// Removes a file from the list.
        /// </summary>
        /// <param name="id">The id of the file to remove.</param>
        public void RemoveFilePath(string id)
        {
            filePaths.RemoveAt(filePaths.FindIndexFirst(id));
        }

        /// <summary>
        /// Updates the title of a file on the list.
        /// </summary>
        /// <param name="id">The ID of the file to update.</param>
        /// <param name="newTitle">The nw to title to use.</param>
        public void UpdateFileTitle(string id, string newTitle)
        {
            SafetyNet.EnsureListIsNotNullOrEmpty(filePaths, "filePaths");
            foreach (FilePathInfo pathInfo in filePaths)
            {
                if (pathInfo.ID != id) continue;
                
                pathInfo.UpdateTitle(newTitle);
                pathInfo.UpdatePath(CombineFilePath(newTitle));
                return;
            }
            throw new ArgumentNullException($"No element with id '{id}' was found.");
        }
        
        /// <summary>
        /// Grabs the path to a registered file.
        /// </summary>
        /// <param name="id">The id of the file.</param>
        /// <returns>Path of the file with the same title.</returns>
        public string GetFilePath(string id)
        {
            return filePaths.FindValueFirst(id).Path;
        }

        /// <summary>
        /// Grabs a title registered under a specific id.
        /// </summary>
        /// <param name="id">The id of the file.</param>
        /// <param name="title">The stored title of the file.</param>
        /// <param name="path">The stored path of the file.</param>
        /// <returns>Title of the file with the same id.</returns>
        public void GetFileTitleAndPath(string id, out string title, out string path)
        {
            FilePathInfo info = filePaths.FindValueFirst(id);
            title = info.Title;
            path = info.Path;
        }

        /// <summary>
        /// Converts a file title and converts it to it's path.
        /// </summary>
        /// <param name="id">The ID of the asset.</param>
        /// <param name="title">The title of the file.</param>
        /// <returns>The path of the file.</returns>
        private FilePathInfo ConvertToFileInfo(string id, string title)
        {
            return new FilePathInfo(id, title, CombineFilePath(title));
        }
        
        /// <summary>
        /// Returns a path to a file with a specific name.
        /// </summary>
        /// <param name="title">The title of the file.</param>
        /// <returns>The file's path.</returns>
        private string CombineFilePath(string title)
        {
            return System.IO.Path.Combine(path, $"{title}.{extension}");
        }
        
        public string FolderName { get => folderTitle; }
        public string Extension { get => extension; }
        public string Path { get => path; }
    }
}