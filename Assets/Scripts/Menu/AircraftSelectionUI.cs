using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AircraftSelectionUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform aircraftGrid;
    [SerializeField] private Button aircraftButtonPrefab;
    [SerializeField] private Image aircraftPreview;
    [SerializeField] private Text aircraftName;
    [SerializeField] private Text aircraftStats;
    [SerializeField] private Button selectButton;
    [SerializeField] private Button backButton;
    
    private AircraftData selectedAircraft;
    
    private void Start()
    {
        PopulateAircraftSelection();
        selectButton.onClick.AddListener(OnSelectClicked);
        backButton.onClick.AddListener(OnBackClicked);
    }
    
    private void PopulateAircraftSelection()
    {
        int aircraftCount = AircraftDatabase.GetAircraftCount();
        
        for (int i = 0; i < aircraftCount; i++)
        {
            AircraftData aircraft = AircraftDatabase.GetAircraft(i);
            Button button = Instantiate(aircraftButtonPrefab, aircraftGrid);
            
            button.GetComponentInChildren<Text>().text = aircraft.aircraftName;
            button.onClick.AddListener(() => OnAircraftSelected(aircraft));
        }
    }
    
    private void OnAircraftSelected(AircraftData aircraft)
    {
        selectedAircraft = aircraft;
        aircraftPreview.sprite = aircraft.thumbnail;
        aircraftName.text = aircraft.aircraftName + " (" + aircraft.nation + ")";
        
        string stats = $"Max Speed: {aircraft.maxSpeed} km/h\n" +
                      $"Missiles: {aircraft.missileCapacity}\n" +
                      $"Ammo: {aircraft.cannonAmmo}\n" +
                      $"Fuel: {aircraft.fuelCapacity}L";
        aircraftStats.text = stats;
    }
    
    private void OnSelectClicked()
    {
        if (selectedAircraft != null)
        {
            PlayerPrefs.SetString("SelectedAircraft", selectedAircraft.aircraftName);
            SceneManager.LoadScene("BattleScene");
        }
    }
    
    private void OnBackClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
