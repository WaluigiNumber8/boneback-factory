namespace Rogium.Editors.Core
{
    /// <summary>
    /// A base for any data object, resembling an asset with and ID & title.
    /// </summary>
    public interface IDataAsset : IIDHolder
    {
        public string Title { get; }
    }
}