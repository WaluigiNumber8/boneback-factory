using System.IO;
using System.IO.Compression;

namespace RedRats.Systems.FileSystem
{
    /// <summary>
    /// Compresses/Decompresses files.
    /// </summary>
    public static class CompressionSystem
    {
        private const string CompressedExtension = ".bdata";
        
        public static void Compress(string fileName)
        {
            using FileStream originalFileStream = File.Open(fileName, FileMode.Open);
            using FileStream compressedFileStream = File.Create(Path.GetFileNameWithoutExtension(fileName) + (CompressedExtension));
            using DeflateStream compressor = new(compressedFileStream, CompressionMode.Compress);
            originalFileStream.CopyTo(compressor);
        }
    }
}