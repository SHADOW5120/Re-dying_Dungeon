using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private LayerMask playerLayer;
    private PlayerStatus playerStatus;
    [SerializeField]private BoxCollider2D boxCollider;
    private MonsterStatus monsterStatus;
    private Animator anim;
    [SerializeField] AudioClip attackSound;

    private void Awake()
    {
        monsterStatus = GetComponent<MonsterStatus>();
        anim = GetComponent<Animator>();
    }
    private bool PlayerInsight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);

        if(hit.collider != null)
        {
            playerStatus = hit.transform.GetComponent<PlayerStatus>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    public void DamagePlayer()
    {
        SoundManager.instance.PlaySound(attackSound);
        if (PlayerInsight())
        {
            playerStatus.GetDamage(monsterStatus.GetMonsterDamage());
            Debug.Log("Monster damage: " + playerStatus.currentHealth);
        }
    }
}
