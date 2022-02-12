using System;
using System.Collections.Generic;
using System.Linq;

namespace BoubakProductions.Core
{
    /// <summary>
    /// Contains various utilities for working with enums.
    /// </summary>
    public static class EnumUtils
    {
        public static IList<string> ToStringList(Type enumType)
        {
            return Enum.GetNames(enumType).ToList();
        }
    }
}