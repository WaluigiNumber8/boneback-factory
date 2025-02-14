using System;
using System.IO;
using System.Linq;
using RedRats.UI.ErrorMessageWindow;

namespace RedRats.Safety
{
    /// <summary>
    /// Contains methods for checking preconditions related to I/O operations.
    /// </summary>
    public static class PreconditionsIO
    {
        public static event Action<string> OnFireErrorMessage;

        /// <summary>
        /// Checks if a given directory exists on the external storage.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        public static void DirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                string message = $"'{path}' doesn't exist.";
                OnFireErrorMessage?.Invoke(message);
                throw new SafetyNetIOException(message);
            }
        }

        /// <summary>
        /// Checks if a given file exists on the external storage.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        public static void FileExists(string path)
        {
            if (!File.Exists(path))
            {
                string message = $"'{path}' doesn't exist.";
                OnFireErrorMessage?.Invoke(message);
                throw new SafetyNetIOException(message);
            }
        }

        /// <summary>
        /// Checks if a given path contains any invalid characters.
        /// </summary>
        /// <param name="path">The path to check.</param>
        /// <exception cref="SafetyNetIOException">Is thrown if path contains invalid characters.</exception>
        public static void PathNotContainsInvalidCharacters(string path)
        {
            if (Path.GetInvalidFileNameChars().All(path.Contains))
            {
                throw new SafetyNetIOException($"'{path}' contains invalid symbols.");
            }
        }
        
        /// <summary>
        /// Throw a custom error message, without raising an exception.
        /// </summary>
        /// <param name="message">The message to throw.</param>
        public static void ThrowMessage(string message)
        {
            ErrorMessageController.GetInstance().Open(message);
            OnFireErrorMessage?.Invoke(message);
        }
    }
}