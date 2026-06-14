using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    [Header("Currency Settings")]
    [SerializeField] private Text moneyText;
    [SerializeField] private Text diamondText;
    [SerializeField] private Slider moneySlider;
    [SerializeField] private Slider diamondSlider;
    
    private int totalMoney = 0;
    private int totalDiamonds = 0;
    private int maxMoney = 999999;
    private int maxDiamonds = 9999;
    
    private static CurrencyManager instance;
    
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
        LoadCurrency();
        UpdateUI();
    }
    
    public void AddMoney(int amount)
    {
        totalMoney += amount;
        totalMoney = Mathf.Min(totalMoney, maxMoney);
        SaveCurrency();
        UpdateUI();
        
        Debug.Log($"[CURRENCY] +${amount} → Total: ${totalMoney}");
    }
    
    public void AddDiamonds(int amount)
    {
        totalDiamonds += amount;
        totalDiamonds = Mathf.Min(totalDiamonds, maxDiamonds);
        SaveCurrency();
        UpdateUI();
        
        Debug.Log($"[CURRENCY] +{amount} 💎 → Total: {totalDiamonds}💎");
    }
    
    public bool SpendMoney(int amount)
    {
        if (totalMoney >= amount)
        {
            totalMoney -= amount;
            SaveCurrency();
            UpdateUI();
            
            Debug.Log($"[CURRENCY] -{amount}$ → Total: ${totalMoney}");
            return true;
        }
        
        Debug.Log($"[CURRENCY] Pas assez d'argent! Besoin: ${amount}, Disponible: ${totalMoney}");
        return false;
    }
    
    public bool SpendDiamonds(int amount)
    {
        if (totalDiamonds >= amount)
        {
            totalDiamonds -= amount;
            SaveCurrency();
            UpdateUI();
            
            Debug.Log($"[CURRENCY] -{amount} 💎 → Total: {totalDiamonds}💎");
            return true;
        }
        
        Debug.Log($"[CURRENCY] Pas assez de diamants! Besoin: {amount}💎, Disponible: {totalDiamonds}💎");
        return false;
    }
    
    private void UpdateUI()
    {
        if (moneyText != null)
            moneyText.text = $"${totalMoney}";
        
        if (diamondText != null)
            diamondText.text = $"{totalDiamonds}💎";
        
        if (moneySlider != null)
            moneySlider.value = (float)totalMoney / maxMoney;
        
        if (diamondSlider != null)
            diamondSlider.value = (float)totalDiamonds / maxDiamonds;
    }
    
    private void SaveCurrency()
    {
        PlayerPrefs.SetInt("TotalMoney", totalMoney);
        PlayerPrefs.SetInt("TotalDiamonds", totalDiamonds);
        PlayerPrefs.Save();
    }
    
    private void LoadCurrency()
    {
        totalMoney = PlayerPrefs.GetInt("TotalMoney", 1000);
        totalDiamonds = PlayerPrefs.GetInt("TotalDiamonds", 100);
    }
    
    public int GetMoney() => totalMoney;
    public int GetDiamonds() => totalDiamonds;
    
    public static CurrencyManager Instance => instance;
}
