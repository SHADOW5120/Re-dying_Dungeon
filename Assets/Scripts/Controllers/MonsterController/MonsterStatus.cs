using Photon.Pun;
using UnityEngine;

public class MonsterStatus : MonoBehaviour
{
    [SerializeField] private CharacterSO character;
    private string monsterName => character.Name;
    private float monsterHealth => character.Health;
    private float monsterAttackDamage => character.AttackDamage;
    private float monsterDefense => character.Defense;

    private Animator anim;
    private Rigidbody2D body;

    public float currentHealth { get; private set; }

    [SerializeField] private Behaviour[] components;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip dieSound;
    [SerializeField] private GameObject reward;

    private void Awake()
    {
        currentHealth = monsterHealth;
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    public float GetMonsterDamage()
    {
        return monsterAttackDamage;
    }

    public void GetDamage(float _damage)
    {
        float realDamage = Mathf.Clamp(_damage - (int)Random.Range(0, monsterDefense + 1), 0, _damage);
        currentHealth = Mathf.Clamp(currentHealth - realDamage, 0, monsterHealth);
        if (currentHealth > 0) 
        { 
            anim.SetTrigger("take_hit"); 
            SoundManager.instance.PlaySound(hurtSound);
        }
        else 
        {
            anim.SetTrigger("die");
            SoundManager.instance.PlaySound(dieSound);
            gameObject.layer = LayerMask.NameToLayer("DeadBody");
            body.linearVelocity = new Vector2(0, 0);
            foreach (Behaviour component in components)
            {
                component.enabled = false;
            }
            if(reward != null)
            {
                PhotonNetwork.Instantiate(reward.name, gameObject.transform.position, Quaternion.identity);
            }
        }
    }
}
