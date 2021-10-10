using System;
using BoubakProductions.Safety;
using Rogium.Editors.PackData;
using Rogium.ExternalStorage.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Rogium.Editors.Campaign;

namespace Rogium.ExternalStorage
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
        /// <param name="pack">The pack to save.</param>
        public static void SavePack(string path, PackAsset pack)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);
            SerializedPackAsset formattedAsset = new SerializedPackAsset(pack);

            formatter.Serialize(stream, formattedAsset);
            stream.Close();
        }
        /// <summary>
        /// Save an asset to external storage.
        /// </summary>
        /// <param name="path">Destination of the save.</param>
        /// <param name="campaign">The campaign to save.</param>
        public static void SaveCampaign(string path, CampaignAsset campaign)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);
            SerializedCampaignAsset formattedAsset = new SerializedCampaignAsset(campaign);

            formatter.Serialize(stream, formattedAsset);
            stream.Close();
        }

        /// <summary>
        /// Loads all assets under a path.
        /// </summary>
        /// <param name="path">Destination of the save.</param>
        public static IList<PackAsset> LoadAllPacks(string path)
        {
            SafetyNetIO.EnsureDirectoryExists(path);

            IList<PackAsset> assets = new List<PackAsset>();

            BinaryFormatter formatter = new BinaryFormatter();
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] files = directory.GetFiles();

            foreach (FileInfo file in files)
            {
                string filePath = Path.Combine(path, file.Name);
                FileStream stream = new FileStream(filePath, FileMode.Open);

                SerializedPackAsset asset = (SerializedPackAsset)formatter.Deserialize(stream);
                assets.Add(asset.Deserialize());

                stream.Close();
            } 

            return assets;
        }
        /// <summary>
        /// Loads all assets under a path.
        /// </summary>
        /// <param name="path">Destination of the save.</param>
        public static IList<CampaignAsset> LoadAllCampaigns(string path)
        {
            SafetyNetIO.EnsureDirectoryExists(path);

            IList<CampaignAsset> assets = new List<CampaignAsset>();

            BinaryFormatter formatter = new BinaryFormatter();
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] files = directory.GetFiles();

            foreach (FileInfo file in files)
            {
                string filePath = Path.Combine(path, file.Name);
                FileStream stream = new FileStream(filePath, FileMode.Open);

                SerializedCampaignAsset asset = (SerializedCampaignAsset)formatter.Deserialize(stream);
                assets.Add(asset.Deserialize());

                stream.Close();
            } 

            return assets;
        }

        /// <summary>
        /// Removes an asset data file under a specific path.
        /// </summary>
        /// <param name="path">The path of the pack.</param>
        public static void Delete(string path)
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