using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public int Id;
    public string Name;
    public string Effect;
    public float EffectNumber;
    public float Price;
    public bool IsBought;

    public ItemModel ToModel()
    {
        return new ItemModel(Id, Name, Effect, EffectNumber, Price, IsBought);
    }
}
