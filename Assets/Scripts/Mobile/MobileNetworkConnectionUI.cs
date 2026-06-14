using UnityEngine;
using UnityEngine.UI;

public class MobileNetworkConnectionUI : MonoBehaviour
{
    [Header("Connection UI")]
    [SerializeField] private InputField ipAddressInput;
    [SerializeField] private InputField portInput;
    [SerializeField] private Button connectButton;
    [SerializeField] private Button disconnectButton;
    [SerializeField] private Text statusText;
    [SerializeField] private Image connectionIndicator;
    
    private MobileNetworkManager networkManager;
    
    private void Start()
    {
#if UNITY_ANDROID || UNITY_IOS
        EnableNetworkUI();
#else
        DisableNetworkUI();
#endif
        
        networkManager = FindObjectOfType<MobileNetworkManager>();
        
        if (connectButton != null)
            connectButton.onClick.AddListener(OnConnectPressed);
        
        if (disconnectButton != null)
            disconnectButton.onClick.AddListener(OnDisconnectPressed);
        
        // Default values
        if (ipAddressInput != null)
            ipAddressInput.text = "127.0.0.1";
        
        if (portInput != null)
            portInput.text = "7777";
    }
    
    private void Update()
    {
        UpdateConnectionStatus();
    }
    
    private void OnConnectPressed()
    {
        string ipAddress = ipAddressInput.text;
        
        Debug.Log($"[MOBILE MP] Tentative connexion à {ipAddress}");
        
        if (networkManager != null)
            networkManager.SetServerAddress(ipAddress);
        
        if (statusText != null)
            statusText.text = "Connexion en cours...";
    }
    
    private void OnDisconnectPressed()
    {
        Debug.Log("[MOBILE MP] Déconnexion");
        
        if (statusText != null)
            statusText.text = "Déconnecté";
    }
    
    private void UpdateConnectionStatus()
    {
        bool isConnected = Mirror.NetworkClient.isConnected;
        
        if (connectionIndicator != null)
            connectionIndicator.color = isConnected ? Color.green : Color.red;
    }
    
    private void EnableNetworkUI()
    {
        Debug.Log("[MOBILE MP] UI réseau activée");
        if (ipAddressInput != null)
            ipAddressInput.gameObject.SetActive(true);
        if (portInput != null)
            portInput.gameObject.SetActive(true);
        if (connectButton != null)
            connectButton.gameObject.SetActive(true);
    }
    
    private void DisableNetworkUI()
    {
        if (ipAddressInput != null)
            ipAddressInput.gameObject.SetActive(false);
        if (portInput != null)
            portInput.gameObject.SetActive(false);
        if (connectButton != null)
            connectButton.gameObject.SetActive(false);
    }
}
