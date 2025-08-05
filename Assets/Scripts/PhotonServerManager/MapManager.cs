using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

public class MapManager : MonoBehaviourPun
{
    public AbtractDungeonGenerator generator;
    public UnityEvent OnFinishedToSpawnPlayer;
    public UnityEvent OnFinishedToSyncMap;
    public AudioClip battleMusic;

    private void Start()
    {
        ASyncLoader.Instance.LoadData("Generating map and props, please waiting a minute!", 3f);
        SoundManager.instance.ChangeMusic(battleMusic);
        if (PhotonNetwork.IsMasterClient)
        {
            GenerateMap();
            //OnFinishedToSpawnPlayer?.Invoke();
            OnFinishedToSyncMap?.Invoke();
        }
        //OnFinishedToSpawnPlayer?.Invoke();
    }

    private void GenerateMap()
    {
        generator.GenerateDungeon();
    }
}
