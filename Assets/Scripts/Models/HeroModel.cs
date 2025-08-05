public class HeroModel
{
    public int id;
    public string heroName;
    public float damage;
    public float defense;
    public float health;
    public float sanity;
    public float price;
    public string infor;
    public bool isBought;

    public HeroModel(int id, string heroName, float damage, float denfense, float health, float sanity, string infor, float price, bool isBought)
    {
        this.id = id;
        this.heroName = heroName;
        this.damage = damage;
        this.defense = denfense;
        this.health = health;
        this.sanity = sanity;
        this.infor = infor;
        this.price = price;
        this.isBought = isBought;
    }
}
