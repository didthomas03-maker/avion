using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SurrenderSystem : MonoBehaviour
{
    [Header("Surrender UI")]
    [SerializeField] private Button surrenderButton;
    [SerializeField] private GameObject surrenderPanel;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Text confirmText;
    
    private bool isDefeated = false;
    
    private void Start()
    {
        if (surrenderButton != null)
            surrenderButton.onClick.AddListener(OnSurrenderPressed);
        
        if (confirmButton != null)
            confirmButton.onClick.AddListener(OnConfirmSurrender);
        
        if (cancelButton != null)
            cancelButton.onClick.AddListener(OnCancelSurrender);
        
        // ESC key also triggers surrender
        InvokeRepeating("CheckEscapeKey", 0.1f, 0.1f);
    }
    
    private void CheckEscapeKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnSurrenderPressed();
        }
    }
    
    private void OnSurrenderPressed()
    {
        if (surrenderPanel != null)
        {
            surrenderPanel.SetActive(true);
            if (confirmText != null)
                confirmText.text = "Êtes-vous sûr de vouloir abandonner la partie?\n\n🏳️ Vous perdrez cette bataille!";
        }
    }
    
    private void OnConfirmSurrender()
    {
        Debug.Log("Joueur a abandonné la partie - DRAPEAU BLANC 🏳️");
        isDefeated = true;
        
        // Show defeat screen
        ShowDefeatScreen();
        
        // Log surrender
        PlayerPrefs.SetInt("LastDefeat", 1);
        PlayerPrefs.SetInt("SurrenderCount", PlayerPrefs.GetInt("SurrenderCount", 0) + 1);
        
        // Disable all controls
        Time.timeScale = 0.5f;
    }
    
    private void OnCancelSurrender()
    {
        if (surrenderPanel != null)
            surrenderPanel.SetActive(false);
    }
    
    private void ShowDefeatScreen()
    {
        Debug.Log("Affichage écran de défaite");
        Invoke("ReturnToMenu", 3f);
    }
    
    private void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
