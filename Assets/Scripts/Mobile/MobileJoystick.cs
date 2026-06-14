using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [Header("Joystick Settings")]
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform handle;
    [SerializeField] private float joystickRange = 100f;
    [SerializeField] private Image joystickImage;
    
    private Vector2 joystickInput = Vector2.zero;
    private bool isPressed = false;
    private Vector2 startPos;
    
    public Vector2 GetInput()
    {
        return joystickInput;
    }
    
    public bool IsPressed()
    {
        return isPressed;
    }
    
    private void Start()
    {
        if (background == null) return;
        startPos = background.anchoredPosition;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        joystickInput = Vector2.zero;
        
        if (handle != null)
            handle.anchoredPosition = Vector2.zero;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 direction = eventData.delta;
        joystickInput = Vector2.ClampMagnitude(direction / joystickRange, 1f);
        
        if (handle != null)
            handle.anchoredPosition = joystickInput * joystickRange;
    }
}
