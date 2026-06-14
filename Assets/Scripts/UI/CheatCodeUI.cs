using UnityEngine;
using UnityEngine.UI;

public class CheatCodeUI : MonoBehaviour
{
    [Header("Cheat Code Panel")]
    [SerializeField] private GameObject cheatCodePanel;
    [SerializeField] private InputField codeInputField;
    [SerializeField] private Button submitButton;
    [SerializeField] private Button helpButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Text statusText;
    
    [SerializeField] private Text availableCodesText;
    
    private CheatCodeSystem cheatCodeSystem;
    private bool showCodes = false;
    
    private void Start()
    {
        cheatCodeSystem = FindObjectOfType<CheatCodeSystem>();
        
        if (submitButton != null)
            submitButton.onClick.AddListener(OnSubmitCode);
        
        if (helpButton != null)
            helpButton.onClick.AddListener(OnHelpPressed);
        
        if (closeButton != null)
            closeButton.onClick.AddListener(OnClosePanel);
        
        // Activer le panel par défaut
        if (cheatCodePanel != null)
            cheatCodePanel.SetActive(true);
    }
    
    private void OnSubmitCode()
    {
        if (codeInputField != null && !codeInputField.text.IsNullOrEmpty())
        {
            string code = codeInputField.text.ToUpper();
            
            // Vérifier si c'est un code valide
            var validCodes = new string[] 
            {
                "MONEY100", "MONEY500", "MONEY1000", "RICHFOREVER",
                "DIAMOND50", "DIAMOND100", "DIAMOND500", "DIAMONDRICH",
                "JACKPOT", "BILLIONAIRE",
                "SPEEDUP", "AGILITY", "FIREPOWER", "ARMOR",
                "ALLUPGRADES", "ALLMONEY",
                "GOD", "SKYWALKER", "TOPGUN", "MAVERICK"
            };
            
            bool isValid = false;
            foreach (string validCode in validCodes)
            {
                if (code == validCode)
                {
                    isValid = true;
                    break;
                }
            }
            
            if (isValid)
            {
                ShowStatus($"✅ Code accepté: {code}", Color.green);
            }
            else
            {
                ShowStatus($"❌ Code invalide: {code}", Color.red);
            }
            
            codeInputField.text = "";
        }
    }
    
    private void OnHelpPressed()
    {
        showCodes = !showCodes;
        
        if (showCodes)
        {
            string codesText = "💰 CODES ARGENT:\n";
            codesText += "MONEY100 = +100$\n";
            codesText += "MONEY500 = +500$\n";
            codesText += "MONEY1000 = +1000$\n";
            codesText += "RICHFOREVER = +5000$\n\n";
            
            codesText += "💎 CODES DIAMANTS:\n";
            codesText += "DIAMOND50 = +50 💎\n";
            codesText += "DIAMOND100 = +100 💎\n";
            codesText += "DIAMOND500 = +500 💎\n";
            codesText += "DIAMONDRICH = +1000 💎\n\n";
            
            codesText += "⚡ CODES UPGRADES:\n";
            codesText += "SPEEDUP = Vitesse +20%\n";
            codesText += "AGILITY = Maniabilité +20%\n";
            codesText += "FIREPOWER = Recharge -30%\n";
            codesText += "ARMOR = Armure +30%\n\n";
            
            codesText += "🎰 CODES SPÉCIAUX:\n";
            codesText += "JACKPOT = 2000$ + 200 💎\n";
            codesText += "GOD = Mode Dieu (TOUT!)\n";
            
            if (availableCodesText != null)
                availableCodesText.text = codesText;
        }
        else
        {
            if (availableCodesText != null)
                availableCodesText.text = "";
        }
    }
    
    private void OnClosePanel()
    {
        if (cheatCodePanel != null)
            cheatCodePanel.SetActive(false);
    }
    
    private void ShowStatus(string message, Color color)
    {
        if (statusText != null)
        {
            statusText.text = message;
            statusText.color = color;
            Invoke("ClearStatus", 3f);
        }
    }
    
    private void ClearStatus()
    {
        if (statusText != null)
            statusText.text = "";
    }
}
