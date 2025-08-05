public class MapModel
{
    public int id;
    public string mapName;
    public string difficulty;
    public float price;
    public bool isBought;

    public MapModel(int id, string mapName, string difficulty, float price, bool isBought)
    {
        this.id = id;
        this.mapName = mapName;
        this.difficulty = difficulty;
        this.price = price;
        this.isBought = isBought;
    }
}
