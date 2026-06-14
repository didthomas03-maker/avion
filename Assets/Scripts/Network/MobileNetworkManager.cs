using UnityEngine;
using Mirror;

public class MobileNetworkManager : MonoBehaviour
{
    [Header("Network Configuration")]
    [SerializeField] private string networkAddress = "127.0.0.1";
    [SerializeField] private ushort networkPort = 7777;
    [SerializeField] private int maxConnections = 2;
    
    private static MobileNetworkManager instance;
    private NetworkManager networkManager;
    private bool isNetworkInitialized = false;
    
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
#if UNITY_ANDROID || UNITY_IOS
        InitializeNetworkForMobile();
#endif
    }
    
    private void InitializeNetworkForMobile()
    {
        Debug.Log("[NETWORK] Initialisation réseau mobile");
        
        networkManager = FindObjectOfType<NetworkManager>();
        if (networkManager != null)
        {
            networkManager.networkAddress = networkAddress;
            networkManager.maxConnections = maxConnections;
            isNetworkInitialized = true;
            
            Debug.Log($"[NETWORK] Adresse: {networkAddress}:{networkPort}");
            Debug.Log($"[NETWORK] Connexions max: {maxConnections}");
        }
    }
    
    public void SetServerAddress(string address)
    {
        networkAddress = address;
        if (networkManager != null)
            networkManager.networkAddress = address;
        
        Debug.Log($"[NETWORK] Adresse serveur mise à jour: {address}");
    }
    
    public bool IsNetworkInitialized()
    {
        return isNetworkInitialized;
    }
    
    public string GetServerAddress()
    {
        return networkAddress;
    }
}
