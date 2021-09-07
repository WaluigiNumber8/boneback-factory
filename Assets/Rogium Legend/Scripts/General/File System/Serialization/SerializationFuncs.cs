using RogiumLegend.Editors.PackData;
using RogiumLegend.Editors.RoomData;
using RogiumLegend.Editors.TileData;
using RogiumLegend.Global.GridSystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace RogiumLegend.ExternalStorage.Serialization
{

    /// <summary>
    /// Stores methods, that will serialize special data types (like sprites).
    /// </summary>
    public static class SerializationFuncs
    {
        /// <summary>
        /// Converts a formatted pack asset into a normal pack asset.
        /// </summary>
        /// <param name="packAsset">Formatted pack asset to convert.</param>
        /// <returns>A normal pack asset.</returns>
        public static PackAsset DeserializePackAsset(SerializedPackAsset packAsset)
        {
            PackInfoAsset packInfo = new PackInfoAsset(packAsset.packInfo.packName,
                                                       packAsset.packInfo.description,
                                                       packAsset.packInfo.author,
                                                       DeserializeSprite(packAsset.packInfo.icon),
                                                       DateTime.Parse(packAsset.packInfo.creationDateTime));
            PackAsset pack = new PackAsset(packInfo, DeserializeRooms(packAsset.rooms));
            return pack;
        }

        /// <summary>
        /// Deserializes a serialized sprite and returns in the sprite format.
        /// </summary>
        /// <param name="spr">Serialized Sprite to deserialize.</param>
        /// <returns>A Sprite that Unity can use.</returns>
        public static Sprite DeserializeSprite(SerializedSprite spr)
        {
            Texture2D texture = new Texture2D(spr.textureWidth, spr.textureHeight);
            ImageConversion.LoadImage(texture, spr.textureBytes);
            Sprite sprite = Sprite.Create(texture,
                                          new Rect(spr.x, spr.y, spr.width, spr.height),
                                          new Vector2(spr.pivotX, spr.pivotY));

            return sprite;
        }

        /// <summary>
        /// Turns a serialized color into a unity color format.
        /// </summary>
        /// <param name="serializedColor"></param>
        /// <returns></returns>
        public static Color DeserializeColor(SerializedColor serializedColor)
        {
            Color color = new Color(serializedColor.r, serializedColor.g, serializedColor.b, serializedColor.a);
            return color;
        }

        /// <summary>
        /// Turns a Serialized Matrix4x4 into a normal Matrix4x4.
        /// </summary>
        /// <param name="serializedMatrix">Serialized matrix.</param>
        /// <returns>A normal Matrix4x4</returns>
        public static Matrix4x4 DeserializeMatrix4x4(SerializedMatrix4x4 serializedMatrix)
        {
            Matrix4x4 matrix = new Matrix4x4();
            matrix.m00 = serializedMatrix.m00;
            matrix.m01 = serializedMatrix.m00;
            matrix.m02 = serializedMatrix.m00;
            matrix.m03 = serializedMatrix.m00;
            matrix.m10 = serializedMatrix.m00;
            matrix.m11 = serializedMatrix.m00;
            matrix.m12 = serializedMatrix.m00;
            matrix.m13 = serializedMatrix.m00;
            matrix.m20 = serializedMatrix.m00;
            matrix.m21 = serializedMatrix.m00;
            matrix.m22 = serializedMatrix.m00;
            matrix.m23 = serializedMatrix.m00;
            matrix.m30 = serializedMatrix.m00;
            matrix.m31 = serializedMatrix.m00;
            matrix.m32 = serializedMatrix.m00;
            matrix.m33 = serializedMatrix.m00;
            return matrix;
        }

        /// <summary>
        /// Turns a list of Rooms to a serialized list.
        /// </summary>
        /// <param name="rooms">The lst of rooms to serialize,</param>
        /// <returns>Serialized List.</returns>
        public static IList<SerializedRoomAsset> SerializeRooms(IList<RoomAsset> rooms)
        {
            IList<SerializedRoomAsset> serializedRooms = new List<SerializedRoomAsset>();
            for (int i = 0; i < rooms.Count; i++)
            {
                RoomAsset rm = rooms[i];
                serializedRooms.Add(new SerializedRoomAsset(rm.Title, rm.DifficultyLevel, SerializeTileGrid(rm.TileGrid)));
            }
            return serializedRooms;
        }

        /// <summary>
        /// Turns a Tile grid into a serialized tile grid.
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static SerializedTileAsset[,] SerializeTileGrid(ObjectGrid<TileAsset> grid)
        {
            ObjectGrid<SerializedTileAsset> serializedGrid = new ObjectGrid<SerializedTileAsset>(grid.Width, grid.Height, () => new SerializedTileAsset());
            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    TileAsset tile = grid.GetValue(x, y);
                    serializedGrid.SetValue(x, y, new SerializedTileAsset(tile));
                }
            }
            return serializedGrid.CellArray;
        }

        /// <summary>
        /// Turns a Serialized Tile Grid to a normal grid.
        /// </summary>
        /// <param name="grid">Serialized Grid of tiles.</param>
        /// <returns>A Grid</returns>
        public static ObjectGrid<TileAsset> DeserializeTileGrid(SerializedTileAsset[,] serializedGrid)
        {
            ObjectGrid<TileAsset> grid = new ObjectGrid<TileAsset>(serializedGrid.GetLength(0), serializedGrid.GetLength(1), () => new TileAsset());
            for (int x = 0; x < serializedGrid.GetLength(0); x++)
            {
                for (int y = 0; y < serializedGrid.GetLength(1); y++)
                {
                    SerializedTileAsset serializedTile = serializedGrid[x, y];
                    Tile tile = new Tile();
                    tile.sprite = DeserializeSprite(serializedTile.sprite);
                    tile.color = DeserializeColor(serializedTile.color);
                    tile.transform = DeserializeMatrix4x4(serializedTile.transform);

                    grid.SetValue(x, y, new TileAsset(tile, (TileType)serializedTile.tileType));
                }
            }
            return grid;
        }

        public static IList<RoomAsset> DeserializeRooms(IList<SerializedRoomAsset> serializedRooms)
        {
            IList<RoomAsset> rooms = new List<RoomAsset>();
            for (int i = 0; i < serializedRooms.Count; i++)
            {
                SerializedRoomAsset room = serializedRooms[i];
                rooms.Add(new RoomAsset(room.roomName, room.difficultyLevel, DeserializeTileGrid(room.grid)));
            }
            return rooms;
        }



    }
}