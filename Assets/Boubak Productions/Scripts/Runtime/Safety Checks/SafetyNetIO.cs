using System;
using System.IO;

namespace BoubakProductions.Safety
{
    public static class SafetyNetIO
    {
        public static event Action<string> OnFireErrorMessage;

        /// <summary>
        /// Checks if a given directory exists on the external storage.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        public static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                string message = $"The directory of '{path}' doesn't exist.";
                OnFireErrorMessage?.Invoke(message);
                throw new IOException(message);
            }
        }

        /// <summary>
        /// Checks if a given file exists on the external storage.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        public static void EnsureFileExists(string path)
        {
            if (!File.Exists(path))
            {
                string message = $"The file of '{path}' doesn't exist.";
                OnFireErrorMessage?.Invoke(message);
                throw new IOException(message);
            }
        }
        
        /// <summary>
        /// Throw a custom exception, that is recognized by the Safety Net System.
        /// </summary>
        /// <param name="exception"></param>
        /// <exception cref="Exception"></exception>
        public static void ThrowCustom(Exception exception)
        {
            OnFireErrorMessage?.Invoke($"IO - {exception.Message}");
            throw exception;
        }
    }
}