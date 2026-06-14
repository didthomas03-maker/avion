using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class MobileMultiplayerUI : MonoBehaviour
{
    [Header("Multiplayer UI Elements")]
    [SerializeField] private Button hostButton;
    [SerializeField] private Button joinButton;
    [SerializeField] private InputField serverAddressInput;
    [SerializeField] private Text statusText;
    [SerializeField] private Panel waitingPanel;
    [SerializeField] private Text playerCountText;
    [SerializeField] private Slider signalStrengthSlider;
    
    private MobileMultiplayerManager multiplayerManager;
    private bool isHosting = false;
    
    private void Start()
    {
#if UNITY_ANDROID || UNITY_IOS
        EnableMobileMultiplayer();
#else
        DisableMobileMultiplayer();
#endif
        
        multiplayerManager = FindObjectOfType<MobileMultiplayerManager>();
        
        if (hostButton != null)
            hostButton.onClick.AddListener(OnHostPressed);
        
        if (joinButton != null)
            joinButton.onClick.AddListener(OnJoinPressed);
        
        if (serverAddressInput != null)
            serverAddressInput.text = "localhost";
    }
    
    private void Update()
    {
#if UNITY_ANDROID || UNITY_IOS
        UpdateMultiplayerStatus();
#endif
    }
    
    private void OnHostPressed()
    {
        Debug.Log("[MOBILE MP] Création d'une partie (Hôte)");
        isHosting = true;
        
        if (statusText != null)
            statusText.text = "🔴 HÔTE - En attente de joueur...";
        
        if (NetworkManager.singleton != null)
        {
            NetworkManager.singleton.StartHost();
        }
        
        if (waitingPanel != null)
            waitingPanel.SetActive(true);
    }
    
    private void OnJoinPressed()
    {
        Debug.Log("[MOBILE MP] Connexion à une partie");
        isHosting = false;
        
        string serverAddress = "localhost";
        if (serverAddressInput != null && !serverAddressInput.text.IsNullOrEmpty())
            serverAddress = serverAddressInput.text;
        
        if (statusText != null)
            statusText.text = $"🔵 CLIENT - Connexion à {serverAddress}...";
        
        if (NetworkManager.singleton != null)
        {
            NetworkManager.singleton.networkAddress = serverAddress;
            NetworkManager.singleton.StartClient();
        }
        
        if (waitingPanel != null)
            waitingPanel.SetActive(true);
    }
    
    private void UpdateMultiplayerStatus()
    {
        if (multiplayerManager == null) return;
        
        int playerCount = multiplayerManager.GetConnectedPlayersCount();
        
        if (playerCountText != null)
            playerCountText.text = $"Joueurs: {playerCount}/2";
        
        // Simuler la force du signal
        if (signalStrengthSlider != null)
        {
            signalStrengthSlider.value = Random.Range(0.7f, 1f);
        }
    }
    
    private void EnableMobileMultiplayer()
    {
        Debug.Log("[MOBILE MP] Interface multijoueur mobile activée");
        if (hostButton != null)
            hostButton.gameObject.SetActive(true);
        if (joinButton != null)
            joinButton.gameObject.SetActive(true);
        if (serverAddressInput != null)
            serverAddressInput.gameObject.SetActive(true);
    }
    
    private void DisableMobileMultiplayer()
    {
        if (hostButton != null)
            hostButton.gameObject.SetActive(false);
        if (joinButton != null)
            joinButton.gameObject.SetActive(false);
        if (serverAddressInput != null)
            serverAddressInput.gameObject.SetActive(false);
    }
}

public class Panel : MonoBehaviour
{
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
