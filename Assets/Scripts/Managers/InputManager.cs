using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static float throttle { get; private set; }
    public static float pitch { get; private set; }
    public static float roll { get; private set; }
    public static float yaw { get; private set; }
    public static bool fireWeapon { get; private set; }
    public static bool fireMissile { get; private set; }
    
    private void Update()
    {
        throttle = Input.GetAxis("Vertical");
        pitch = Input.GetAxis("Vertical") * -1;
        roll = Input.GetAxis("Horizontal");
        yaw = Input.GetAxis("Horizontal");
        
        fireWeapon = Input.GetMouseButton(0);
        fireMissile = Input.GetKeyDown(KeyCode.Space);
    }
}
