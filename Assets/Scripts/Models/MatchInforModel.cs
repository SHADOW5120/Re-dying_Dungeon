public class MatchInforModel
{
    public string playerId;
    public int heroId;
    public string result;
    public int matchId;

    public MatchInforModel(string playerId, int heroId, string result, int matchId)
    {
        this.playerId = playerId;
        this.heroId = heroId;
        this.result = result;
        this.matchId = matchId;
    }
}
