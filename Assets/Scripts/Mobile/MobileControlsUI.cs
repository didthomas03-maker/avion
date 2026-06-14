using UnityEngine;
using UnityEngine.UI;

public class MobileControlsUI : MonoBehaviour
{
    [Header("Mobile UI Elements")]
    [SerializeField] private MobileJoystick leftJoystick;
    [SerializeField] private MobileJoystick rightJoystick;
    [SerializeField] private Button fireButton;
    [SerializeField] private Button missileButton;
    [SerializeField] private Button surrenderButton;
    [SerializeField] private Slider throttleSlider;
    
    private AircraftController playerAircraft;
    private bool showMobileUI = false;
    
    private void Start()
    {
#if UNITY_ANDROID || UNITY_IOS
        showMobileUI = true;
        EnableMobileControls();
#else
        DisableMobileControls();
#endif
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            playerAircraft = player.GetComponent<AircraftController>();
        
        if (fireButton != null)
            fireButton.onClick.AddListener(FireCannon);
        
        if (missileButton != null)
            missileButton.onClick.AddListener(FireMissile);
        
        if (surrenderButton != null)
            surrenderButton.onClick.AddListener(ShowSurrenderConfirm);
    }
    
    private void Update()
    {
        if (!showMobileUI) return;
        UpdateMobileControls();
    }
    
    private void UpdateMobileControls()
    {
        if (playerAircraft == null) return;
        
        if (leftJoystick != null)
        {
            Vector2 leftInput = leftJoystick.GetInput();
            Debug.Log($"Left Joystick: {leftInput}");
        }
        
        if (rightJoystick != null)
        {
            Vector2 rightInput = rightJoystick.GetInput();
            Debug.Log($"Right Joystick: {rightInput}");
        }
        
        if (throttleSlider != null)
        {
            Debug.Log($"Throttle: {throttleSlider.value}");
        }
    }
    
    private void FireCannon()
    {
        if (playerAircraft != null)
            Debug.Log("Cannon fire - Mobile");
    }
    
    private void FireMissile()
    {
        if (playerAircraft != null)
            Debug.Log("Missile fired - Mobile");
    }
    
    private void ShowSurrenderConfirm()
    {
        Debug.Log("Surrender button pressed - Mobile");
    }
    
    private void EnableMobileControls()
    {
        Debug.Log("Enabling mobile controls for Android/iOS");
        if (leftJoystick != null)
            leftJoystick.gameObject.SetActive(true);
        if (rightJoystick != null)
            rightJoystick.gameObject.SetActive(true);
        if (fireButton != null)
            fireButton.gameObject.SetActive(true);
        if (missileButton != null)
            missileButton.gameObject.SetActive(true);
        if (throttleSlider != null)
            throttleSlider.gameObject.SetActive(true);
    }
    
    private void DisableMobileControls()
    {
        if (leftJoystick != null)
            leftJoystick.gameObject.SetActive(false);
        if (rightJoystick != null)
            rightJoystick.gameObject.SetActive(false);
        if (fireButton != null)
            fireButton.gameObject.SetActive(false);
        if (missileButton != null)
            missileButton.gameObject.SetActive(false);
        if (throttleSlider != null)
            throttleSlider.gameObject.SetActive(false);
    }
}
