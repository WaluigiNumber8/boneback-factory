using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace RedRats.Core
{
    /// <summary>
    /// Helper methods for working with <see cref="Enum"/>s.
    /// </summary>
    public class EnumUtils
    {
        /// <summary>
        /// Get the description text of all available enum values.
        /// </summary>
        /// <param name="enumType">The enum type to read from.</param>
        /// <typeparam name="T">Any type of <see cref="Type"/>.</typeparam>
        /// <returns>A list of description strings.</returns>
        public static IList<string> GetAllDescriptions<T>(T enumType) where T : Type
        {
            string[] names = Enum.GetNames(enumType);
            IList<string> descriptions = new List<string>();
            foreach (string name in names)
            {
                FieldInfo field = enumType.GetField(name);
                string description = GetDescriptionText(field);
                if (description == null) continue;
                descriptions.Add(description);
            }
            return descriptions;
        }
        
        /// <summary>
        /// Get the description string of an enum type.
        /// </summary>
        /// <param name="enumValue"></param>
        /// <typeparam name="T">Any type of struct.</typeparam>
        /// <returns>The description attribute string.</returns>
        public static string GetDescription<T>(T enumValue) where T : struct
        {
            Type type = enumValue.GetType();
            string name = Enum.GetName(type, enumValue);
            
            if (name == null) return null;
            
            FieldInfo field = type.GetField(name);

            return GetDescriptionText(field);
        }

        /// <summary>
        /// Grab a description text from FieldInfo.
        /// </summary>
        /// <param name="field">The field to grab description from.</param>
        /// <returns>Description text.</returns>
        private static string GetDescriptionText(FieldInfo field)
        {
            if (field == null) return null;

            return (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attr) ? attr.Description: null;
        }
    }
}