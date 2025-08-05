using UnityEngine;

[CreateAssetMenu]
public class CharacterSO : ScriptableObject
{
    [Header("Character Data: ")]

    public int Id;
    public string Name;
    public float Health;
    public float Sanity;
    public float AttackDamage;
    public float Defense;
    public string Infor;
    public float Price;
    public bool IsBought;

    public HeroModel ToModel()
    {
        return new HeroModel(Id, Name, AttackDamage, Defense, Health, Sanity, Infor, Price, IsBought);
    }
}
