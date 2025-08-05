using Newtonsoft.Json;
using Photon.Pun;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Text;
using System;
using UnityEngine;
using UnityEngine.Events;

public class SyncMap : MonoBehaviourPun
{
    private DungeonData dungeonData;
    [SerializeField] private TilemapVisualizer visualizer;
    public UnityEvent OnFinishedToSpawnPlayer;

    private void Awake()
    {
        dungeonData = FindAnyObjectByType<DungeonData>();
    }

    public void SyncDungeonData()
    {
            var mapData = new MapData
            {
                Rooms = new List<RoomData>(),
                Path = new List<Vector2IntData>()
            };

            foreach (var room in dungeonData.Rooms)
            {
                RoomData roomData = new RoomData
                {
                    Center = new Vector2Data(room.RoomCenterPos),
                    FloorTiles = new List<Vector2IntData>()
                };

                foreach (var tile in room.FloorTiles)
                {
                    roomData.FloorTiles.Add(new Vector2IntData(tile));
                }

                mapData.Rooms.Add(roomData);
            }

            foreach (var pathTile in dungeonData.Path)
            {
                mapData.Path.Add(new Vector2IntData(pathTile));
            }

            string jsonData = JsonConvert.SerializeObject(mapData, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            byte[] compressedData = Compress(jsonData);
            photonView.RPC("RPC_SyncMap", RpcTarget.Others, compressedData);
            Debug.Log("Map synchronized successfully.");
        if (PhotonNetwork.IsMasterClient)
        {
            OnFinishedToSpawnPlayer?.Invoke();
        }
    }

    [PunRPC]
    public void RPC_SyncMap(byte[] compressedData)
    {
        try
        {
            string jsonData = Decompress(compressedData);
            var mapData = JsonConvert.DeserializeObject<MapData>(jsonData);
            BuildMapFromData(mapData);
            Debug.Log("Map received and built.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error receiving map: " + ex.Message);
        }
    }

    private void BuildMapFromData(MapData mapData)
    {
        visualizer.Clear();
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        foreach (var room in mapData.Rooms)
        {
            foreach (var tile in room.FloorTiles)
            {
                floor.Add(new Vector2Int(tile.x, tile.y));
            }
        }

        foreach (var pathTile in mapData.Path)
        {
            floor.Add(new Vector2Int(pathTile.x, pathTile.y));
        }

        visualizer.PaintFloorTiles(floor);
        WallGenerator.CreateWall(floor, visualizer);

        if (dungeonData != null)
        {
            dungeonData.Rooms = new List<Room>();

            foreach (var roomData in mapData.Rooms)
            {
                HashSet<Vector2Int> roomFloor = new HashSet<Vector2Int>();

                foreach (var tile in roomData.FloorTiles)
                {
                    roomFloor.Add(new Vector2Int(tile.x, tile.y));
                }

                Vector2 center = new Vector2(roomData.Center.x, roomData.Center.y);
                Room newRoom = new Room(center, roomFloor);

                dungeonData.Rooms.Add(newRoom);
            }

            dungeonData.Path = new HashSet<Vector2Int>();
            foreach (var pathTile in mapData.Path)
            {
                dungeonData.Path.Add(new Vector2Int(pathTile.x, pathTile.y));
            }

            Debug.Log($"[SyncMap] Dungeon data reconstructed on client. Rooms count: {dungeonData.Rooms.Count}");
        }
        else
        {
            Debug.LogWarning("[SyncMap] dungeonData is null on client.");
        }

        OnFinishedToSpawnPlayer?.Invoke();
    }

    private byte[] Compress(string str)
    {
        byte[] data = Encoding.UTF8.GetBytes(str);
        using (var memoryStream = new MemoryStream())
        {
            using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress))
            {
                gZipStream.Write(data, 0, data.Length);
            }
            return memoryStream.ToArray();
        }
    }

    private string Decompress(byte[] compressedData)
    {
        using (var memoryStream = new MemoryStream(compressedData))
        {
            using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
            {
                using (var resultStream = new MemoryStream())
                {
                    gZipStream.CopyTo(resultStream);
                    return Encoding.UTF8.GetString(resultStream.ToArray());
                }
            }
        }
    }

    [Serializable]
    private class MapData
    {
        public List<RoomData> Rooms;
        public List<Vector2IntData> Path;
    }

    [Serializable]
    private class RoomData
    {
        public Vector2Data Center;
        public List<Vector2IntData> FloorTiles;
    }

    [Serializable]
    private class Vector2Data
    {
        public float x, y;

        public Vector2Data(Vector2 vector)
        {
            x = vector.x;
            y = vector.y;
        }
    }

    [Serializable]
    private class Vector2IntData
    {
        public int x, y;

        public Vector2IntData(Vector2Int vector)
        {
            x = vector.x;
            y = vector.y;
        }
    }
}
