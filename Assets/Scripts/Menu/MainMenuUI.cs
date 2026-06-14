using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button multiplayerButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Text titleText;
    
    private void Start()
    {
        startButton.onClick.AddListener(OnStartClicked);
        multiplayerButton.onClick.AddListener(OnMultiplayerClicked);
        settingsButton.onClick.AddListener(OnSettingsClicked);
        exitButton.onClick.AddListener(OnExitClicked);
        
        // Play background music
        // TODO: AudioManager.PlayMusic("MainMenuMusic");
    }
    
    private void OnStartClicked()
    {
        SceneManager.LoadScene("AircraftSelection");
    }
    
    private void OnMultiplayerClicked()
    {
        SceneManager.LoadScene("MultiplayerMenu");
    }
    
    private void OnSettingsClicked()
    {
        // TODO: Open settings panel
    }
    
    private void OnExitClicked()
    {
        Application.Quit();
    }
}
