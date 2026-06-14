using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup menuCanvas;
    [SerializeField] private Button soloButton;
    [SerializeField] private Button multiplayerButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Text titleText;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private AudioSource backgroundMusic;

    private void Start()
    {
        InitializeUI();
        SetupButtons();
    }

    private void InitializeUI()
    {
        if (titleText != null)
            titleText.text = "⚡ TONNERRE DE GUERRE ⚡";

        if (menuCanvas != null)
        {
            menuCanvas.alpha = 0f;
            StartCoroutine(FadeInMenu());
        }
    }

    private void SetupButtons()
    {
        if (soloButton != null)
            soloButton.onClick.AddListener(StartSolo);

        if (multiplayerButton != null)
            multiplayerButton.onClick.AddListener(StartMultiplayer);

        if (settingsButton != null)
            settingsButton.onClick.AddListener(OpenSettings);

        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
    }

    private void StartSolo()
    {
        SceneManager.LoadScene("CarrierScene");
    }

    private void StartMultiplayer()
    {
        SceneManager.LoadScene("MultiplayerLobby");
    }

    private void OpenSettings()
    {
        Debug.Log("Settings not implemented yet");
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private System.Collections.IEnumerator FadeInMenu()
    {
        for (float i = 0f; i <= 1f; i += Time.deltaTime)
        {
            if (menuCanvas != null)
                menuCanvas.alpha = i;
            yield return null;
        }
    }
}
