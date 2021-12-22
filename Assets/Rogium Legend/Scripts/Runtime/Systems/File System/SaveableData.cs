using UnityEngine;

namespace Rogium.ExternalStorage
{
    /// <summary>
    /// Contains variables for a saveable data type.
    /// </summary>
    public class SaveableData
    {
        private string folderTitle;
        private string extension;
        private string path;

        public SaveableData(string folderTitle, string extension)
        {
            this.folderTitle = folderTitle;
            this.extension = extension;
            this.path = System.IO.Path.Combine(Application.persistentDataPath, folderTitle);
        }
        
        public string FolderName { get => folderTitle; }
        public string Extension { get => extension; }
        public string Path { get => path; }
    }
}