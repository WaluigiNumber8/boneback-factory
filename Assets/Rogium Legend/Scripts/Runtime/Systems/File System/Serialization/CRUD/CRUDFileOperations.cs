using System.IO;
using RedRats.Core;
using RedRats.Systems.FileSystem;

namespace Rogium.ExternalStorage
{
    /// <summary>
    /// CRUD operations for JSON files.
    /// </summary>
    public class CRUDFileOperations : ICRUDFileOperations
    {
        private const string JsonExtension = ".json";

        private readonly string path;
        private readonly string folderPath;
        private readonly bool prettify;

        public CRUDFileOperations(string title, string folderPath, bool prettify = true)
        {
            this.prettify = prettify;
            this.folderPath = folderPath;
            path = Path.Combine(this.folderPath, $"{title}{JsonExtension}");
            if (!File.Exists(path)) Save("");
        }

        public void Save(string data)
        {
            FileSystem.CreateDirectory(folderPath);
            string finalData = prettify ? data.AsPrettyJson() : data;
            FileSystem.SaveFile(path, finalData);
        }

        public string Load() => FileSystem.LoadFile(path, JsonExtension);

        public void Delete() => FileSystem.DeleteFile(path);
    }
}