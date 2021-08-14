using BoubakProductions.Safety;
using RogiumLegend.Editors.PackData;
using RogiumLegend.ExternalStorage.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace RogiumLegend.ExternalStorage
{
    /// <summary>
    /// Handles Saving, Loading and Removing packs from external storage.
    /// </summary>
    public static class FileSystem
    {
        /// <summary>
        /// Save pack to external storage.
        /// </summary>
        /// <param name="path">Destination of the save.</param>
        /// <param name="pack">The pack to save.</param>
        public static void SavePackAsset(string path, PackAsset pack)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);
            SerializedPackAsset formattedAsset = new SerializedPackAsset(pack.PackInfo);

            formatter.Serialize(stream, formattedAsset);
            stream.Close();
        }

        /// <summary>
        /// Loads all packs under a path.
        /// </summary>
        /// <param name="path">Destination of the save.</param>
        public static IList<PackAsset> LoadPackAssets(string path)
        {
            SafetyNetIO.EnsureDirectoryExists(path);

            List<PackAsset> assets = new List<PackAsset>();

            BinaryFormatter formatter = new BinaryFormatter();
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] files = directory.GetFiles();

            for (int i = 0; i < files.Length; i++)
            {
                string filePath = Path.Combine(path, files[i].Name);
                FileStream stream = new FileStream(filePath, FileMode.Open);

                SerializedPackAsset asset = (SerializedPackAsset)formatter.Deserialize(stream);
                assets.Add(SerializationFuncs.DeserializePackAsset(asset));

                stream.Close();
            } 

            return assets;
        }

        /// <summary>
        /// Removes the pack data file under a specific path.
        /// </summary>
        /// <param name="path">The path of the pack.</param>
        public static void DeletePack(string path)
        {
            SafetyNetIO.EnsureFileExists(path);

            try
            {
                File.Delete(path);
            }
            catch (IOException e)
            {
                throw new IOException(e.Message);
            }
        }

    }
}