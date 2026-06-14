using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameModeSelectionUI : MonoBehaviour
{
    [Header("Mode Selection UI")]
    [SerializeField] private Button offlineButton;
    [SerializeField] private Button onlineButton;
    [SerializeField] private Button campaignButton;
    [SerializeField] private Button backButton;
    
    private void Start()
    {
        if (offlineButton != null)
            offlineButton.onClick.AddListener(SelectOfflineMode);
        
        if (onlineButton != null)
            onlineButton.onClick.AddListener(SelectOnlineMode);
        
        if (campaignButton != null)
            campaignButton.onClick.AddListener(SelectCampaignMode);
        
        if (backButton != null)
            backButton.onClick.AddListener(GoBack);
    }
    
    private void SelectOfflineMode()
    {
        Debug.Log("Offline Mode Selected");
        PlayerPrefs.SetString("GameMode", "Offline");
        SceneManager.LoadScene("AircraftSelection");
    }
    
    private void SelectOnlineMode()
    {
        Debug.Log("Online Mode Selected");
        PlayerPrefs.SetString("GameMode", "Online");
        SceneManager.LoadScene("MultiplayerMenu");
    }
    
    private void SelectCampaignMode()
    {
        Debug.Log("Campaign Mode Selected");
        PlayerPrefs.SetString("GameMode", "Campaign");
        SceneManager.LoadScene("CampaignSelection");
    }
    
    private void GoBack()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
