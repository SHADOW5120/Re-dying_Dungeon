public class ItemModel
{
    public int id;
    public string itemName;
    public string effect;
    public float effectNum;
    public float price;
    public bool isBought;

    public ItemModel(int id, string itemName, string effect, float effectNum, float price, bool isBought)
    {
        this.id = id;
        this.itemName = itemName;
        this.effect = effect;
        this.effectNum = effectNum;
        this.price = price;
        this.isBought = isBought;
    }
}
