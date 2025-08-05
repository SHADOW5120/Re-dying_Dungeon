public class PlayerModel
{
    private static PlayerModel _instance;
    public static PlayerModel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PlayerModel();
            }
            return _instance;
        }
    }

    public string username;
    public string email;

    private PlayerModel() { }

    public static void Initialize(string username, string email)
    {
        Instance.username = username;
        Instance.email = email;
    }

    public static void Destroy()
    {
        _instance = null;
    }
}
