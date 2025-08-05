using Photon.Pun;
using Unity.Cinemachine;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    DungeonData dungeonData;
    public GameObject[] playerPrefabs;

    [SerializeField]
    private CinemachineCamera vCamera;

    private void Awake()
    {
        dungeonData = FindAnyObjectByType<DungeonData>();
    }

    public void SpawnPlayer()
    {
        if (dungeonData == null)
        {
            Debug.Log("null in spawner");
            return;
        }

        Debug.Log("come in spawner");

        Room room = dungeonData.Rooms[(int)Random.Range(0, dungeonData.Rooms.Count / 3)];

        Debug.Log((int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]);

        try
        {
            GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
            GameObject player = PhotonNetwork.Instantiate(playerToSpawn.name, room.RoomCenterPos + Vector2.one * 0.5f, Quaternion.identity);

            //Make the camera follow the player
            vCamera.Follow = player.transform;
            vCamera.LookAt = player.transform;
            dungeonData.PlayerReference = player;
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
            
        //GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
        //GameObject player = PhotonNetwork.Instantiate(playerToSpawn.name, dungeonData.Rooms[(int)Random.Range(0, dungeonData.Rooms.Count / 3)].RoomCenterPos + Vector2.one * 0.5f, Quaternion.identity);

        //if(GetComponent<PhotonView>().IsMine == true)
        //{
        //    //Make the camera follow the player
        //    vCamera.Follow = player.transform;
        //    vCamera.LookAt = player.transform;
        //    dungeonData.PlayerReference = player;
        //}
        

        //PhotonNetwork.Instantiate("Player", new Vector3(Random.Range(-8, +8), -1.5f, 0), Quaternion.identity);
    }
}
