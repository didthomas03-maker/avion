using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class MobilePvPHUD : MonoBehaviour
{
    [Header("Local Player HUD")]
    [SerializeField] private Text playerNameText;
    [SerializeField] private Text healthText;
    [SerializeField] private Image healthBar;
    [SerializeField] private Text missileText;
    [SerializeField] private Text speedText;
    [SerializeField] private Text altitudeText;
    
    [Header("Enemy HUD")]
    [SerializeField] private Text enemyNameText;
    [SerializeField] private Image enemyHealthBar;
    [SerializeField] private Text enemyMissileText;
    [SerializeField] private Text radarText;
    
    [Header("Network Status")]
    [SerializeField] private Text connectionStatus;
    [SerializeField] private Image signalStrengthImage;
    [SerializeField] private Text pingText;
    
    private MobileNetworkAircraft localNetworkAircraft;
    private MobileNetworkAircraft enemyNetworkAircraft;
    private float updateRate = 0.1f;
    private float updateTimer = 0f;
    private float pingTimer = 0f;
    
    private void Start()
    {
        localNetworkAircraft = FindObjectOfType<MobileNetworkAircraft>();
        
        if (playerNameText != null)
            playerNameText.text = "YOU";
        
        if (enemyNameText != null)
            enemyNameText.text = "ENEMY";
    }
    
    private void Update()
    {
        updateTimer += Time.deltaTime;
        pingTimer += Time.deltaTime;
        
        if (updateTimer >= updateRate)
        {
            UpdateLocalPlayerHUD();
            UpdateEnemyHUD();
            updateTimer = 0f;
        }
        
        if (pingTimer >= 1f)
        {
            UpdateNetworkStatus();
            pingTimer = 0f;
        }
    }
    
    private void UpdateLocalPlayerHUD()
    {
        if (localNetworkAircraft == null) return;
        
        float health = localNetworkAircraft.GetNetworkHealth();
        if (healthText != null)
            healthText.text = $"HP: {health:F0}";
        
        if (healthBar != null)
            healthBar.fillAmount = health / 100f;
        
        int missiles = localNetworkAircraft.GetNetworkMissileCount();
        if (missileText != null)
            missileText.text = $"MISSILES: {missiles}/8";
    }
    
    private void UpdateEnemyHUD()
    {
        if (enemyNetworkAircraft == null) return;
        
        float health = enemyNetworkAircraft.GetNetworkHealth();
        if (enemyHealthBar != null)
            enemyHealthBar.fillAmount = health / 100f;
        
        int missiles = enemyNetworkAircraft.GetNetworkMissileCount();
        if (enemyMissileText != null)
            enemyMissileText.text = $"{missiles}/8";
    }
    
    private void UpdateNetworkStatus()
    {
        if (NetworkManager.singleton == null) return;
        
        if (NetworkClient.isConnected)
        {
            if (connectionStatus != null)
                connectionStatus.text = "🟢 CONNECTÉ";
            
            if (signalStrengthImage != null)
                signalStrengthImage.color = Color.green;
        }
        else
        {
            if (connectionStatus != null)
                connectionStatus.text = "🔴 DÉCONNECTÉ";
            
            if (signalStrengthImage != null)
                signalStrengthImage.color = Color.red;
        }
        
        // Simuler le ping
        int simulatedPing = Random.Range(20, 100);
        if (pingText != null)
            pingText.text = $"PING: {simulatedPing}ms";
    }
    
    public void SetEnemyAircraft(MobileNetworkAircraft enemy)
    {
        enemyNetworkAircraft = enemy;
    }
}
