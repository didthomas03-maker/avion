using UnityEngine;

public class AircraftUpgradeManager : MonoBehaviour
{
    [Header("Upgrade Settings")]
    [SerializeField] private float speedUpgradeMultiplier = 1.2f;
    [SerializeField] private float agilityUpgradeMultiplier = 1.2f;
    [SerializeField] private float firerateUpgradeMultiplier = 0.8f; // Réduction du temps
    [SerializeField] private float armorUpgradeMultiplier = 1.3f;
    
    private float currentSpeedBoost = 1f;
    private float currentAgilityBoost = 1f;
    private float currentFirerateBoost = 1f;
    private float currentArmorBoost = 1f;
    
    private static AircraftUpgradeManager instance;
    
    [System.Serializable]
    public class AircraftStats
    {
        public float speed = 2200f;
        public float agility = 90f;
        public float firerate = 1f; // Secondes entre les tirs
        public float armor = 100f;
    }
    
    private AircraftStats baseStats = new AircraftStats();
    
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
        LoadUpgrades();
    }
    
    public void UpgradeSpeed(float multiplier)
    {
        currentSpeedBoost += multiplier;
        currentSpeedBoost = Mathf.Clamp(currentSpeedBoost, 1f, 5f);
        SaveUpgrades();
        
        Debug.Log($"[UPGRADE] Vitesse: +{multiplier * 100}% (Total: {currentSpeedBoost}x)");
    }
    
    public void UpgradeAgility(float multiplier)
    {
        currentAgilityBoost += multiplier;
        currentAgilityBoost = Mathf.Clamp(currentAgilityBoost, 1f, 5f);
        SaveUpgrades();
        
        Debug.Log($"[UPGRADE] Maniabilité: +{multiplier * 100}% (Total: {currentAgilityBoost}x)");
    }
    
    public void UpgradeFirerate(float reduction)
    {
        currentFirerateBoost -= reduction;
        currentFirerateBoost = Mathf.Clamp(currentFirerateBoost, 0.2f, 1f);
        SaveUpgrades();
        
        Debug.Log($"[UPGRADE] Recharge missiles: -{reduction * 100}% (Temps: {currentFirerateBoost}s)");
    }
    
    public void UpgradeArmor(float multiplier)
    {
        currentArmorBoost += multiplier;
        currentArmorBoost = Mathf.Clamp(currentArmorBoost, 1f, 5f);
        SaveUpgrades();
        
        Debug.Log($"[UPGRADE] Armure: +{multiplier * 100}% (Total: {currentArmorBoost}x)");
    }
    
    public void MaxAllUpgrades()
    {
        currentSpeedBoost = 5f;
        currentAgilityBoost = 5f;
        currentFirerateBoost = 0.2f;
        currentArmorBoost = 5f;
        SaveUpgrades();
        
        Debug.Log("[UPGRADE] Tous les upgrades maximisés!");
    }
    
    public AircraftStats GetUpgradedStats()
    {
        return new AircraftStats
        {
            speed = baseStats.speed * currentSpeedBoost,
            agility = baseStats.agility * currentAgilityBoost,
            firerate = baseStats.firerate * currentFirerateBoost,
            armor = baseStats.armor * currentArmorBoost
        };
    }
    
    private void SaveUpgrades()
    {
        PlayerPrefs.SetFloat("SpeedBoost", currentSpeedBoost);
        PlayerPrefs.SetFloat("AgilityBoost", currentAgilityBoost);
        PlayerPrefs.SetFloat("FirerateBoost", currentFirerateBoost);
        PlayerPrefs.SetFloat("ArmorBoost", currentArmorBoost);
        PlayerPrefs.Save();
    }
    
    private void LoadUpgrades()
    {
        currentSpeedBoost = PlayerPrefs.GetFloat("SpeedBoost", 1f);
        currentAgilityBoost = PlayerPrefs.GetFloat("AgilityBoost", 1f);
        currentFirerateBoost = PlayerPrefs.GetFloat("FirerateBoost", 1f);
        currentArmorBoost = PlayerPrefs.GetFloat("ArmorBoost", 1f);
    }
    
    public float GetSpeedBoost() => currentSpeedBoost;
    public float GetAgilityBoost() => currentAgilityBoost;
    public float GetFirerateBoost() => currentFirerateBoost;
    public float GetArmorBoost() => currentArmorBoost;
    
    public static AircraftUpgradeManager Instance => instance;
}
