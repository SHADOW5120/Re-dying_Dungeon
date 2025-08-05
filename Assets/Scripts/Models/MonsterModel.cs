public class MonsterModel
{
    public int id;
    public string monsterName;
    public float damage;
    public float denfense;
    public float health;
    public float sanity;
    public string infor;
    public int mapId;

    public MonsterModel(int id, string monsterName, float damage, float denfense, float health, float sanity, string infor, int mapId)
    {
        this.id = id;
        this.monsterName = monsterName;
        this.damage = damage;
        this.denfense = denfense;
        this.health = health;
        this.sanity = sanity;
        this.infor = infor;
        this.mapId = mapId;
    }
}
