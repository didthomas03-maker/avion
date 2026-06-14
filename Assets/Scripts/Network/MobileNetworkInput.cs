using UnityEngine;
using Mirror;

public class MobileNetworkInput : NetworkBehaviour
{
    [Header("Network Input")]
    [SerializeField] private MobileJoystick leftJoystick;
    [SerializeField] private MobileJoystick rightJoystick;
    [SerializeField] private AndroidJoystickLayout joystickLayout;
    
    private AircraftController aircraft;
    private MobileNetworkAircraft networkAircraft;
    private float inputSendRate = 0.05f; // 20 updates per second
    private float inputSendTimer = 0f;
    
    private void Start()
    {
        aircraft = GetComponent<AircraftController>();
        networkAircraft = GetComponent<MobileNetworkAircraft>();
        
        if (joystickLayout != null)
        {
            leftJoystick = joystickLayout.GetLeftJoystick();
            rightJoystick = joystickLayout.GetRightJoystick();
        }
    }
    
    private void Update()
    {
        if (!isLocalPlayer || !isOwned)
            return;
        
#if UNITY_ANDROID || UNITY_IOS
        HandleMobileNetworkInput();
#endif
    }
    
    private void HandleMobileNetworkInput()
    {
        if (leftJoystick == null || rightJoystick == null)
            return;
        
        Vector2 leftInput = leftJoystick.GetInput();
        Vector2 rightInput = rightJoystick.GetInput();
        
        inputSendTimer += Time.deltaTime;
        
        if (inputSendTimer >= inputSendRate)
        {
            CmdSendNetworkInput(leftInput, rightInput);
            inputSendTimer = 0f;
        }
    }
    
    [Command]
    private void CmdSendNetworkInput(Vector2 leftInput, Vector2 rightInput)
    {
        // Applier les inputs aux joueurs locaux
        if (aircraft != null)
        {
            // Appliquer commandes de vol
            Debug.Log($"[NETWORK INPUT] L:{leftInput} R:{rightInput}");
        }
    }
}
