using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField]private CharacterSO character;
    [SerializeField]private HealthBar healthBar;
    [SerializeField] private HealthBar sanityBar;
    private string playerName => character.Name;
    private float playerHealth => character.Health;
    private float playerSanity => character.Sanity;
    private float playerAttackDamage => character.AttackDamage;
    private float playerDefense => character.Defense;
    [SerializeField] private Behaviour[] components;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip dieSound;

    public float currentHealth { get; private set; }
    public float currentSanity { get; set; }
    private Animator anim;
    public bool isAlive = true;

    private void Awake()
    {
        currentHealth = playerHealth;
        currentSanity = playerSanity;
        healthBar.UpdateBar(currentHealth, playerHealth);
        sanityBar.UpdateBar(currentSanity, currentSanity);
        anim = GetComponent<Animator>();
    }

    public float GetPlayerDamage()
    {
        return playerAttackDamage;
    }

    public void GetDamage(float _damage)
    {
        float realDamage = Mathf.Clamp(_damage - (int)Random.Range(0, playerDefense + 1), 0, _damage);
        currentHealth = Mathf.Clamp(currentHealth - realDamage, 0, playerHealth);
        healthBar.UpdateBar(currentHealth, playerHealth);

        if (currentHealth > 0) 
        { 
            anim.SetTrigger("take_hit"); 
            SoundManager.instance.PlaySound(hurtSound); 
        }
        else
        {
            isAlive = false;
            anim.SetTrigger("die");
            SoundManager.instance.PlaySound(dieSound);
            gameObject.layer = LayerMask.NameToLayer("DeadBody");
            foreach (Behaviour component in components)
            {
                component.enabled = false;
            }
        }

        if (currentHealth <= 0)
        {
            isAlive = false;
            foreach (Behaviour component in components)
            {
                component.enabled = false;
            }
        }
    }
    
    public void HealthCollect(float _health)
    {
        currentHealth = Mathf.Clamp(currentHealth + _health, 0, playerHealth);
        healthBar.UpdateBar(currentHealth, playerHealth);
    }

    public void SanityDecrease(float _sanity)
    {
        currentSanity = Mathf.Clamp(currentSanity - _sanity, 0, playerSanity);
        sanityBar.UpdateBar(currentSanity, playerSanity);
    }

    public void Resurrect()
    {
        if (currentSanity > 0) isAlive = true;
        else return;
            currentHealth = playerHealth * 0.5f;
        float sanityDamage = (int)Random.Range(0, playerSanity);
        SanityDecrease(sanityDamage);
        healthBar.UpdateBar(currentHealth, playerHealth);
        anim.ResetTrigger("die");
        anim.Play("Idle");
        gameObject.layer = LayerMask.NameToLayer("Player");
        foreach (Behaviour component in components)
        {
            component.enabled = true;
        }
    }
}
