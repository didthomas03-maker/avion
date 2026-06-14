using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AircraftShopUI : MonoBehaviour
{
    [Header("Shop UI")]
    [SerializeField] private Button closeButton;
    [SerializeField] private ScrollRect aircraftScroll;
    [SerializeField] private Text selectedAircraftText;
    [SerializeField] private Text priceText;
    [SerializeField] private Button buyButton;
    [SerializeField] private Text balanceText;
    
    [Header("Aircraft Prices")]
    [SerializeField] private int rafalPrice = 1000;
    [SerializeField] private int f35Price = 1500;
    [SerializeField] private int f22Price = 2000;
    [SerializeField] private int f15Price = 2500;
    [SerializeField] private int f16Price = 1200;
    
    [System.Serializable]
    public class AircraftShop
    {
        public string name;
        public int price;
        public string description;
    }
    
    private List<AircraftShop> aircraftList = new List<AircraftShop>();
    private CurrencyManager currencyManager;
    private string selectedAircraft = "";
    private int selectedPrice = 0;
    
    private void Start()
    {
        currencyManager = CurrencyManager.Instance;
        
        aircraftList.Add(new AircraftShop { name = "Rafale", price = rafalPrice, description = "Omni-rôle français" });
        aircraftList.Add(new AircraftShop { name = "F-35 Lightning II", price = f35Price, description = "Stealth américain" });
        aircraftList.Add(new AircraftShop { name = "F-22 Raptor", price = f22Price, description = "Supériorité aérienne" });
        aircraftList.Add(new AircraftShop { name = "F-15 Eagle", price = f15Price, description = "Lourd et rapide" });
        aircraftList.Add(new AircraftShop { name = "F-16 Fighting Falcon", price = f16Price, description = "Léger et agile" });
        
        if (closeButton != null)
            closeButton.onClick.AddListener(Close);
        
        if (buyButton != null)
            buyButton.onClick.AddListener(BuyAircraft);
        
        DisplayAircraftList();
    }
    
    private void DisplayAircraftList()
    {
        string listText = "";
        
        foreach (var aircraft in aircraftList)
        {
            listText += $"🛩️ {aircraft.name}\n";
            listText += $"   {aircraft.description}\n";
            listText += $"   Prix: ${aircraft.price}\n\n";
        }
        
        Debug.Log($"[SHOP AIRCRAFT]\n{listText}");
    }
    
    public void SelectAircraft(string aircraftName, int price)
    {
        selectedAircraft = aircraftName;
        selectedPrice = price;
        
        if (selectedAircraftText != null)
            selectedAircraftText.text = $"Sélectionné: {aircraftName}";
        
        if (priceText != null)
            priceText.text = $"Prix: ${price}";
        
        if (balanceText != null)
            balanceText.text = $"Solde: ${currencyManager.GetMoney()}";
    }
    
    private void BuyAircraft()
    {
        if (selectedAircraft.IsNullOrEmpty())
        {
            Debug.Log("[SHOP] Sélectionnez un avion!");
            return;
        }
        
        if (currencyManager.SpendMoney(selectedPrice))
        {
            Debug.Log($"[SHOP] Avion acheté: {selectedAircraft} pour ${selectedPrice}");
            PlayerPrefs.SetString("OwnedAircraft", selectedAircraft);
            
            if (priceText != null)
                priceText.text = "✅ Acheté!";
        }
        else
        {
            Debug.Log($"[SHOP] Pas assez d'argent! Besoin: ${selectedPrice}");
        }
    }
    
    private void Close()
    {
        gameObject.SetActive(false);
    }
}
