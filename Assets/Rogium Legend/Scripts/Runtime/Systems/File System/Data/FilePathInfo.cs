namespace Rogium.ExternalStorage
{
    public class FilePathInfo
    {
        private string id;
        private string title;
        private string path;

        public FilePathInfo(string id, string title, string path)
        {
            this.id = id;
            this.title = title;
            this.path = path;
        }

        /// <summary>
        /// Updates the title.
        /// </summary>
        /// <param name="newTitle">The new title to use.</param>
        public void UpdateTitle(string newTitle) => title = newTitle;

        /// <summary>
        /// Updates the path.
        /// </summary>
        /// <param name="newPath">The new path.</param>
        public void UpdatePath(string newPath) => path = newPath;
        
        public string ID { get => id; }
        public string Title { get => title; }
        public string Path { get => path; }
    }
}