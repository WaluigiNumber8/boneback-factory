using System;
using System.IO;
using System.Linq;
using RedRats.UI.ErrorMessageWindow;

namespace RedRats.Safety
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
                string message = $"'{path}' doesn't exist.";
                OnFireErrorMessage?.Invoke(message);
                throw new SafetyNetIOException(message);
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
        public static void EnsurePathNotContainsInvalidCharacters(string path)
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