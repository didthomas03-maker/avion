using UnityEngine;
using UnityEngine.UI;

public class UpgradeShopUI : MonoBehaviour
{
    [Header("Shop UI Elements")]
    [SerializeField] private Button speedUpgradeButton;
    [SerializeField] private Button agilityUpgradeButton;
    [SerializeField] private Button firerateUpgradeButton;
    [SerializeField] private Button armorUpgradeButton;
    [SerializeField] private Button resetButton;
    
    [SerializeField] private Text speedLevelText;
    [SerializeField] private Text agilityLevelText;
    [SerializeField] private Text firerevelText;
    [SerializeField] private Text armorLevelText;
    
    [SerializeField] private int upgradeCost = 100;
    
    private CurrencyManager currencyManager;
    private AircraftUpgradeManager upgradeManager;
    
    private void Start()
    {
        currencyManager = CurrencyManager.Instance;
        upgradeManager = AircraftUpgradeManager.Instance;
        
        if (speedUpgradeButton != null)
            speedUpgradeButton.onClick.AddListener(() => BuyUpgrade("speed", upgradeCost));
        
        if (agilityUpgradeButton != null)
            agilityUpgradeButton.onClick.AddListener(() => BuyUpgrade("agility", upgradeCost));
        
        if (firerateUpgradeButton != null)
            firerateUpgradeButton.onClick.AddListener(() => BuyUpgrade("firerate", upgradeCost));
        
        if (armorUpgradeButton != null)
            armorUpgradeButton.onClick.AddListener(() => BuyUpgrade("armor", upgradeCost));
        
        if (resetButton != null)
            resetButton.onClick.AddListener(ResetUpgrades);
        
        UpdateUpgradeUI();
    }
    
    private void BuyUpgrade(string upgradeType, int diamondCost)
    {
        if (currencyManager.SpendDiamonds(diamondCost))
        {
            switch (upgradeType.ToLower())
            {
                case "speed":
                    upgradeManager.UpgradeSpeed(0.2f);
                    Debug.Log($"[SHOP] Vitesse améliorée! Coût: {diamondCost}💎");
                    break;
                case "agility":
                    upgradeManager.UpgradeAgility(0.2f);
                    Debug.Log($"[SHOP] Maniabilité améliorée! Coût: {diamondCost}💎");
                    break;
                case "firerate":
                    upgradeManager.UpgradeFirerate(0.15f);
                    Debug.Log($"[SHOP] Recharge améliorée! Coût: {diamondCost}💎");
                    break;
                case "armor":
                    upgradeManager.UpgradeArmor(0.25f);
                    Debug.Log($"[SHOP] Armure améliorée! Coût: {diamondCost}💎");
                    break;
            }
            
            UpdateUpgradeUI();
        }
        else
        {
            Debug.Log($"[SHOP] Pas assez de diamants! Besoin: {diamondCost}💎");
        }
    }
    
    private void UpdateUpgradeUI()
    {
        if (upgradeManager == null) return;
        
        if (speedLevelText != null)
            speedLevelText.text = $"Vitesse: {upgradeManager.GetSpeedBoost():F1}x";
        
        if (agilityLevelText != null)
            agilityLevelText.text = $"Maniabilité: {upgradeManager.GetAgilityBoost():F1}x";
        
        if (firerevelText != null)
            firerevelText.text = $"Recharge: {upgradeManager.GetFirerateBoost():F2}s";
        
        if (armorLevelText != null)
            armorLevelText.text = $"Armure: {upgradeManager.GetArmorBoost():F1}x";
    }
    
    private void ResetUpgrades()
    {
        if (EditorUtility.DisplayDialog("Reset Upgrades", "Êtes-vous sûr?", "Oui", "Non"))
        {
            PlayerPrefs.DeleteKey("SpeedBoost");
            PlayerPrefs.DeleteKey("AgilityBoost");
            PlayerPrefs.DeleteKey("FirerateBoost");
            PlayerPrefs.DeleteKey("ArmorBoost");
            Debug.Log("[SHOP] Upgrades réinitialisés");
            UpdateUpgradeUI();
        }
    }
}
