using Photon.Pun;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private GameObject reward;
    private MonsterStatus monsterStatus;

    private void Awake()
    {
        monsterStatus = GetComponent<MonsterStatus>();
    }

    private void Update()
    {
        if(monsterStatus.currentHealth == 0)
        {
            PhotonNetwork.Instantiate(reward.name, gameObject.transform.position, Quaternion.identity);
        }
    }
}
