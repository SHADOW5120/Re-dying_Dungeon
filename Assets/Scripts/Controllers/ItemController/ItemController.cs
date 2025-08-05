using Photon.Pun;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private ItemSO item;
    [SerializeField] private GameObject[] innerItem;
    private Animator anim;
    private bool isOpened = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public (string, float) ItemEffect()
    {
        return (item.Effect, item.EffectNumber);
    }

    public void PickedUp()
    {
        anim.SetTrigger("interact");
        Destroy(gameObject);
    }

    public void Open()
    {
        if (isOpened) return;
        isOpened = true;

        anim.SetTrigger("interact");
        gameObject.layer = LayerMask.NameToLayer("Default");

        GameObject spawnedItem = PhotonNetwork.Instantiate(innerItem[Random.Range(0, innerItem.Length)].name, gameObject.transform.position, Quaternion.identity);

        Rigidbody2D rb = spawnedItem.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.gravityScale = 0;           
            rb.linearVelocity = Vector2.up * 1.5f;
            rb.linearDamping = 8f;
        }
    }
}
