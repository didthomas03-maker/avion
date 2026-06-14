using UnityEngine;
using Mirror;

public class MobileMultiplayerManager : NetworkManager
{
    [Header("Mobile Multiplayer Settings")]
    [SerializeField] private int maxPlayers = 2;
    [SerializeField] private float gameStartDelay = 5f;
    [SerializeField] private bool isAndroidBuild = false;
    
    private int connectedPlayers = 0;
    private static MobileMultiplayerManager instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
#if UNITY_ANDROID || UNITY_IOS
        isAndroidBuild = true;
        Debug.Log("[MULTIPLAYER] Build Android/iOS détecté");
#endif
    }
    
    public override void OnStartHost()
    {
        base.OnStartHost();
        Debug.Log("[MULTIPLAYER] Serveur démarré (Hôte)");
        ShowMultiplayerUI();
    }
    
    public override void OnStartClient()
    {
        base.OnStartClient();
        Debug.Log("[MULTIPLAYER] Connecté au serveur");
        ShowWaitingUI();
    }
    
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);
        connectedPlayers++;
        Debug.Log($"[MULTIPLAYER] Joueur connecté. Total: {connectedPlayers}/{maxPlayers}");
        
        if (connectedPlayers >= maxPlayers)
        {
            Invoke("StartMultiplayerGame", gameStartDelay);
        }
    }
    
    public override void OnClientConnect()
    {
        base.OnClientConnect();
        Debug.Log("[MULTIPLAYER] Client connecté au serveur");
    }
    
    public override void OnClientDisconnect()
    {
        base.OnClientDisconnect();
        Debug.Log("[MULTIPLAYER] Déconnecté du serveur");
        ShowConnectionLostUI();
    }
    
    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
        connectedPlayers--;
        Debug.Log($"[MULTIPLAYER] Joueur déconnecté. Total: {connectedPlayers}");
    }
    
    private void StartMultiplayerGame()
    {
        Debug.Log("[MULTIPLAYER] Lancement du jeu multijoueur");
        ServerChangeScene("BattleScene");
    }
    
    private void ShowMultiplayerUI()
    {
        Debug.Log("[MULTIPLAYER] Affichage UI multijoueur");
    }
    
    private void ShowWaitingUI()
    {
        Debug.Log("[MULTIPLAYER] En attente d'autres joueurs...");
    }
    
    private void ShowConnectionLostUI()
    {
        Debug.Log("[MULTIPLAYER] Connexion perdue!");
    }
    
    public static bool IsMobileMultiplayer()
    {
        return instance != null && instance.isAndroidBuild;
    }
    
    public int GetConnectedPlayersCount()
    {
        return connectedPlayers;
    }
}
