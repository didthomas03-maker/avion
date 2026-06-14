using UnityEngine;
using UnityEngine.SceneManagement;

public class OfflineModeManager : MonoBehaviour
{
    public enum GameMode { Online, Offline, Campaign }
    
    [Header("Game Mode")]
    [SerializeField] private GameMode currentGameMode = GameMode.Offline;
    
    private static OfflineModeManager instance;
    
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
    }
    
    private void Start()
    {
        if (currentGameMode == GameMode.Offline)
        {
            InitializeOfflineMode();
        }
    }
    
    private void InitializeOfflineMode()
    {
        Debug.Log("[OFFLINE MODE] Initialisation...");
        DisableNetworkFeatures();
        EnableLocalAI();
        PlayerPrefs.SetString("GameMode", "Offline");
    }
    
    private void DisableNetworkFeatures()
    {
        MultiplayerManager[] networkManagers = FindObjectsOfType<MultiplayerManager>();
        foreach (var manager in networkManagers)
        {
            manager.enabled = false;
        }
        Debug.Log("[OFFLINE MODE] Réseau désactivé");
    }
    
    private void EnableLocalAI()
    {
        BattleManager battleManager = FindObjectOfType<BattleManager>();
        if (battleManager != null)
        {
            Debug.Log("[OFFLINE MODE] Ennemis IA locaux activés");
        }
    }
    
    public static GameMode GetCurrentMode()
    {
        return instance.currentGameMode;
    }
    
    public static bool IsOfflineMode()
    {
        return instance.currentGameMode == GameMode.Offline;
    }
    
    public static bool IsCampaignMode()
    {
        return instance.currentGameMode == GameMode.Campaign;
    }
}
