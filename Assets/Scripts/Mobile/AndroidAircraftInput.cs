using UnityEngine;

public class AndroidAircraftInput : MonoBehaviour
{
    [Header("Android Input")]
    [SerializeField] private AndroidJoystickLayout joystickLayout;
    
    private AircraftController aircraft;
    private MobileJoystick leftJoystick;
    private MobileJoystick rightJoystick;
    
    private void Start()
    {
#if UNITY_ANDROID || UNITY_IOS
        aircraft = GetComponent<AircraftController>();
        
        if (joystickLayout != null)
        {
            leftJoystick = joystickLayout.GetLeftJoystick();
            rightJoystick = joystickLayout.GetRightJoystick();
        }
#endif
    }
    
    private void Update()
    {
#if UNITY_ANDROID || UNITY_IOS
        HandleAndroidInput();
#endif
    }
    
    private void HandleAndroidInput()
    {
        if (aircraft == null) return;
        
        if (leftJoystick != null)
        {
            Vector2 leftInput = leftJoystick.GetInput();
        }
        
        if (rightJoystick != null)
        {
            Vector2 rightInput = rightJoystick.GetInput();
        }
    }
}
