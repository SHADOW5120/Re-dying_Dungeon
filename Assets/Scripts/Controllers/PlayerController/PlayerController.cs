using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviourPunCallbacks
{
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [Header("Player Layer")]
    [SerializeField] private LayerMask monsterLayer;
    [SerializeField] private LayerMask itemLayer;
    private Rigidbody2D body;
    private Animator anim;
    private MonsterStatus monsterStatus;
    private ItemController itemStatus;
    private ICharacterInput inputHandler;
    [SerializeField] private BoxCollider2D boxCollider;
    private PlayerStatus playerStatus;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private InputActionReference moveJoyStick;
    [SerializeField] private InputActionReference attackButton;
    [SerializeField] private InputActionReference takeButton;
    [SerializeField] private GameObject takeButtonUI;
    [SerializeField] private GameObject statusScreen;

    private Vector3 savedRespawnPosition;
    private float respawnTimer = 0f;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //boxCollider = GetComponent<BoxCollider2D>();
        inputHandler = GetComponent<ICharacterInput>();
        playerStatus = GetComponent<PlayerStatus>();

        if (photonView.IsMine)
        {
            savedRespawnPosition = transform.position;
            respawnTimer = 0f;
            statusScreen.SetActive(true);
        }
    }

    private void Update()
    {
        if (GetComponent<PhotonView>().IsMine == true)
        {
            // Do all movements here after connecting to Photon
            // Get username text on the heaf of the player by getcomponent<PhotonView>.Controller.NickName (See ytb vid 7)
            if (playerStatus.isAlive)
            {
                Vector2 moveJoyStickInput = inputHandler.GetMoveJoyStick(moveJoyStick);

                body.linearVelocity = new Vector2(moveJoyStickInput.x * speed, moveJoyStickInput.y * speed);

                anim.SetBool("run", moveJoyStickInput.magnitude > 0.01f);

                if (moveJoyStickInput.x > 0.01f) transform.localScale = Vector3.one;
                else if (moveJoyStickInput.x < -0.01f) transform.localScale = new Vector3(-1, 1, 1);


                //Vector2 movement = moveJoyStickInput.normalized * speed * Time.fixedDeltaTime;
                //body.MovePosition(body.position + movement);

                //anim.SetBool("run", moveJoyStickInput.magnitude > 0.01f);

                //if (moveJoyStickInput.x > 0.01f) transform.localScale = Vector3.one;
                //else if (moveJoyStickInput.x < -0.01f) transform.localScale = new Vector3(-1, 1, 1);

                if (inputHandler.GetAttackButton(attackButton))
                {
                    anim.SetTrigger("attack");
                }
                if (ItemInsight())
                {
                    takeButtonUI.SetActive(true);
                    if (inputHandler.GetTakeButton(takeButton))
                    {
                        CollectItem();
                    }
                }
                else takeButtonUI.SetActive(false);

                respawnTimer += Time.deltaTime;
                if (respawnTimer >= 90f)
                {
                    savedRespawnPosition = transform.position;
                    respawnTimer = 0f;
                }
            }
            else
            {
                body.linearVelocity = new Vector2(0, 0);
                Respawn();


            }
        }
    }

    private void Respawn()
    {
        transform.position = savedRespawnPosition;
        playerStatus.Resurrect();
        respawnTimer = 0f;
    }

    private bool EnemyInsight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, monsterLayer);

        if (hit.collider != null)
            monsterStatus = hit.transform.GetComponent<MonsterStatus>();

        return hit.collider != null;
    }

    private bool ItemInsight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, itemLayer);

        if (hit.collider != null)
            itemStatus = hit.transform.GetComponent<ItemController>();

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamageEnemy()
    {
        SoundManager.instance.PlaySound(attackSound);
        if (EnemyInsight())
        {
            monsterStatus.GetDamage(playerStatus.GetPlayerDamage());
            
            Debug.Log("Player damage: " + monsterStatus.currentHealth);
        }
    }

    private void CollectItem()
    {

        (string effectType, float effectNumber) = itemStatus.ItemEffect();
        if(effectType == "chest" || effectType == "minichest")
        {
             itemStatus.Open();
        }
        else
        {
             if(effectType == "health")
             {
                playerStatus.HealthCollect(effectNumber);
             }
             else if(effectType == "win")
             {
                RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                SendOptions sendOptions = new SendOptions { Reliability = true };

                PhotonNetwork.RaiseEvent((byte)1, PhotonNetwork.LocalPlayer.ActorNumber, raiseEventOptions, sendOptions);
            }
            itemStatus.PickedUp();
        }
    }
}
