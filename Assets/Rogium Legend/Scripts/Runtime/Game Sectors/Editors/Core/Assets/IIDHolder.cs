using System;

namespace Rogium.Editors.Core
{
    /// <summary>
    /// A base for any object storing an ID.
    /// </summary>
    public interface IIDHolder
    {
        string ID { get; }
    }
}