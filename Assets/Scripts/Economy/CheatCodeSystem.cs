using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CheatCodeSystem : MonoBehaviour
{
    [Header("Cheat Code Settings")]
    [SerializeField] private InputField cheatCodeInput;
    [SerializeField] private Button submitButton;
    [SerializeField] private Text feedbackText;
    [SerializeField] private ScrollRect cheatHistoryScroll;
    [SerializeField] private Text cheatHistoryText;
    
    private Dictionary<string, CheatCode> cheatCodes = new Dictionary<string, CheatCode>();
    private List<string> cheatHistory = new List<string>();
    private CurrencyManager currencyManager;
    private AircraftUpgradeManager upgradeManager;
    
    [System.Serializable]
    public class CheatCode
    {
        public string code;
        public int moneyReward;
        public int diamondReward;
        public string description;
        public System.Action onExecute;
    }
    
    private void Start()
    {
        currencyManager = FindObjectOfType<CurrencyManager>();
        upgradeManager = FindObjectOfType<AircraftUpgradeManager>();
        
        InitializeCheatCodes();
        
        if (submitButton != null)
            submitButton.onClick.AddListener(OnSubmitCheatCode);
    }
    
    private void InitializeCheatCodes()
    {
        // Codes générateurs d'argent
        AddCheatCode("MONEY100", 100, 0, "Gagne 100$ 💵", () =>
        {
            currencyManager?.AddMoney(100);
        });
        
        AddCheatCode("MONEY500", 500, 0, "Gagne 500$ 💵💵💵", () =>
        {
            currencyManager?.AddMoney(500);
        });
        
        AddCheatCode("MONEY1000", 1000, 0, "Gagne 1000$ 💰💰💰", () =>
        {
            currencyManager?.AddMoney(1000);
        });
        
        AddCheatCode("RICHFOREVER", 5000, 0, "Gagne 5000$ 🤑🤑🤑", () =>
        {
            currencyManager?.AddMoney(5000);
        });
        
        // Codes générateurs de diamants
        AddCheatCode("DIAMOND50", 0, 50, "Gagne 50 diamants 💎", () =>
        {
            currencyManager?.AddDiamonds(50);
        });
        
        AddCheatCode("DIAMOND100", 0, 100, "Gagne 100 diamants 💎💎", () =>
        {
            currencyManager?.AddDiamonds(100);
        });
        
        AddCheatCode("DIAMOND500", 0, 500, "Gagne 500 diamants 💎💎💎", () =>
        {
            currencyManager?.AddDiamonds(500);
        });
        
        AddCheatCode("DIAMONDRICH", 0, 1000, "Gagne 1000 diamants 💎💎💎💎", () =>
        {
            currencyManager?.AddDiamonds(1000);
        });
        
        // Codes combinés
        AddCheatCode("JACKPOT", 2000, 200, "JACKPOT! 2000$ + 200 💎 🎰", () =>
        {
            currencyManager?.AddMoney(2000);
            currencyManager?.AddDiamonds(200);
        });
        
        AddCheatCode("BILLIONAIRE", 10000, 500, "MILLIONNAIRE! 10000$ + 500 💎 👑", () =>
        {
            currencyManager?.AddMoney(10000);
            currencyManager?.AddDiamonds(500);
        });
        
        // Codes améliorations
        AddCheatCode("SPEEDUP", 0, 100, "Boost vitesse +20% ⚡", () =>
        {
            upgradeManager?.UpgradeSpeed(0.2f);
            currencyManager?.AddDiamonds(100);
        });
        
        AddCheatCode("AGILITY", 0, 100, "Boost maniabilité +20% 🔄", () =>
        {
            upgradeManager?.UpgradeAgility(0.2f);
            currencyManager?.AddDiamonds(100);
        });
        
        AddCheatCode("FIREPOWER", 0, 150, "Recharge missiles -30% ⚡🚀", () =>
        {
            upgradeManager?.UpgradeFirerate(0.3f);
            currencyManager?.AddDiamonds(150);
        });
        
        AddCheatCode("ARMOR", 0, 150, "Armure +30% 🛡️", () =>
        {
            upgradeManager?.UpgradeArmor(0.3f);
            currencyManager?.AddDiamonds(150);
        });
        
        // Codes tout débloquer
        AddCheatCode("ALLUPGRADES", 0, 500, "Tous les upgrades MAX! 🚀💎", () =>
        {
            upgradeManager?.MaxAllUpgrades();
            currencyManager?.AddDiamonds(500);
        });
        
        AddCheatCode("ALLMONEY", 50000, 2000, "ULTRA RICHESSE! 50000$ + 2000 💎 💰👑", () =>
        {
            currencyManager?.AddMoney(50000);
            currencyManager?.AddDiamonds(2000);
        });
        
        // Codes secrets
        AddCheatCode("GOD", 0, 0, "Mode Dieu activé! ✨", () =>
        {
            currencyManager?.AddMoney(999999);
            currencyManager?.AddDiamonds(9999);
            upgradeManager?.MaxAllUpgrades();
        });
        
        AddCheatCode("SKYWALKER", 1000, 500, "Bienvenue SkyWalker! 🌟", () =>
        {
            currencyManager?.AddMoney(1000);
            currencyManager?.AddDiamonds(500);
        });
        
        AddCheatCode("TOPGUN", 1500, 300, "Vous êtes un TOP GUN! 🎖️", () =>
        {
            currencyManager?.AddMoney(1500);
            currencyManager?.AddDiamonds(300);
        });
        
        AddCheatCode("MAVERICK", 2000, 400, "Welcome MAVERICK! 🏆", () =>
        {
            currencyManager?.AddMoney(2000);
            currencyManager?.AddDiamonds(400);
        });
        
        Debug.Log($"[CHEAT CODE] {cheatCodes.Count} codes initialisés");
    }
    
    private void AddCheatCode(string code, int money, int diamonds, string description, System.Action action)
    {
        cheatCodes[code.ToUpper()] = new CheatCode
        {
            code = code.ToUpper(),
            moneyReward = money,
            diamondReward = diamonds,
            description = description,
            onExecute = action
        };
    }
    
    private void OnSubmitCheatCode()
    {
        if (cheatCodeInput == null || cheatCodeInput.text.IsNullOrEmpty())
            return;
        
        string inputCode = cheatCodeInput.text.ToUpper().Trim();
        
        if (cheatCodes.ContainsKey(inputCode))
        {
            CheatCode cheat = cheatCodes[inputCode];
            
            // Exécuter le code
            cheat.onExecute?.Invoke();
            
            // Afficher feedback
            ShowFeedback($"✅ {cheat.description}", Color.green);
            
            // Ajouter à l'historique
            AddToHistory($"[✓] {cheat.code}: +{cheat.moneyReward}$ +{cheat.diamondReward}💎");
            
            // Logs
            Debug.Log($"[CHEAT CODE] Activé: {cheat.code} - Money: +{cheat.moneyReward}, Diamonds: +{cheat.diamondReward}");
        }
        else
        {
            ShowFeedback("❌ Code invalide! Essayez: MONEY100, DIAMOND50, GOD", Color.red);
            AddToHistory($"[✗] Code invalide: {inputCode}");
        }
        
        cheatCodeInput.text = "";
    }
    
    private void ShowFeedback(string message, Color color)
    {
        if (feedbackText != null)
        {
            feedbackText.text = message;
            feedbackText.color = color;
            
            // Auto-reset après 3 secondes
            Invoke("ResetFeedback", 3f);
        }
    }
    
    private void ResetFeedback()
    {
        if (feedbackText != null)
            feedbackText.text = "";
    }
    
    private void AddToHistory(string entry)
    {
        cheatHistory.Add(System.DateTime.Now.ToString("HH:mm:ss") + " " + entry);
        
        // Garder seulement les 20 derniers
        if (cheatHistory.Count > 20)
            cheatHistory.RemoveAt(0);
        
        // Afficher l'historique
        if (cheatHistoryText != null)
        {
            cheatHistoryText.text = string.Join("\n", cheatHistory);
            
            // Scroll vers le bas
            if (cheatHistoryScroll != null)
                cheatHistoryScroll.verticalNormalizedPosition = 0f;
        }
    }
    
    public void ShowCheatCodesList()
    {
        Debug.Log("\n=== CODES DISPONIBLES ===");
        
        Debug.Log("\n💵 CODES ARGENT:");
        Debug.Log("MONEY100 - 100$");
        Debug.Log("MONEY500 - 500$");
        Debug.Log("MONEY1000 - 1000$");
        Debug.Log("RICHFOREVER - 5000$");
        
        Debug.Log("\n💎 CODES DIAMANTS:");
        Debug.Log("DIAMOND50 - 50 diamants");
        Debug.Log("DIAMOND100 - 100 diamants");
        Debug.Log("DIAMOND500 - 500 diamants");
        Debug.Log("DIAMONDRICH - 1000 diamants");
        
        Debug.Log("\n🚀 CODES AMÉLIORATIONS:");
        Debug.Log("SPEEDUP - Vitesse +20%");
        Debug.Log("AGILITY - Maniabilité +20%");
        Debug.Log("FIREPOWER - Recharge -30%");
        Debug.Log("ARMOR - Armure +30%");
        
        Debug.Log("\n🎰 CODES SPÉCIAUX:");
        Debug.Log("JACKPOT - 2000$ + 200 diamants");
        Debug.Log("BILLIONAIRE - 10000$ + 500 diamants");
        Debug.Log("ALLUPGRADES - Tous les upgrades MAX");
        Debug.Log("ALLMONEY - 50000$ + 2000 diamants");
        
        Debug.Log("\n✨ CODES SECRETS:");
        Debug.Log("GOD - Mode Dieu total");
        Debug.Log("SKYWALKER - 1000$ + 500 diamants");
        Debug.Log("TOPGUN - 1500$ + 300 diamants");
        Debug.Log("MAVERICK - 2000$ + 400 diamants");
    }
}
