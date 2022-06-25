namespace Rogium.Editors.Rooms
{
    /// <summary>
    /// Different types a room can be.
    /// </summary>
    public enum RoomType
    {
        /// <summary>
        /// A room that appears commonly, with no special stipulation needed.
        /// </summary>
        Common = 0,
        /// <summary>
        /// A room that appears very rarely or only, when the user specifies an entrance to it.
        /// </summary>
        Rare = 1,
        /// <summary>
        /// Appears at the beginning of each difficulty level.
        /// </summary>
        Entrance = 2,
        /// <summary>
        /// Appears at the end of each difficulty level.
        /// </summary>
        Exit = 3,
        /// <summary>
        /// Appears after a certain interval is reached.
        /// </summary>
        Periodic = 4,
    }
}