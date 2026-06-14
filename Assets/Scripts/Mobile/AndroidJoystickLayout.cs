using UnityEngine;
using UnityEngine.UI;

public class AndroidJoystickLayout : MonoBehaviour
{
    [Header("Left Joystick (Mouvement)")]
    [SerializeField] private GameObject leftJoystickPrefab;
    [SerializeField] private RectTransform leftJoystickContainer;
    
    [Header("Right Joystick (Visée)")]
    [SerializeField] private GameObject rightJoystickPrefab;
    [SerializeField] private RectTransform rightJoystickContainer;
    
    [Header("Boutons d'action")]
    [SerializeField] private RectTransform buttonContainer;
    
    private MobileJoystick leftJoystick;
    private MobileJoystick rightJoystick;
    private Button fireButton;
    private Button missileButton;
    
    private void Start()
    {
#if UNITY_ANDROID || UNITY_IOS
        CreateAndroidLayout();
#endif
    }
    
    private void CreateAndroidLayout()
    {
        Debug.Log("Création de la mise en page Android/iOS");
        
        if (leftJoystickContainer != null && leftJoystickPrefab != null)
        {
            GameObject leftObj = Instantiate(leftJoystickPrefab, leftJoystickContainer);
            leftJoystick = leftObj.GetComponent<MobileJoystick>();
            
            RectTransform leftRect = leftObj.GetComponent<RectTransform>();
            leftRect.anchoredPosition = new Vector2(150, 150);
        }
        
        if (rightJoystickContainer != null && rightJoystickPrefab != null)
        {
            GameObject rightObj = Instantiate(rightJoystickPrefab, rightJoystickContainer);
            rightJoystick = rightObj.GetComponent<MobileJoystick>();
            
            RectTransform rightRect = rightObj.GetComponent<RectTransform>();
            rightRect.anchoredPosition = new Vector2(-150, 150);
        }
        
        CreateActionButtons();
    }
    
    private void CreateActionButtons()
    {
        if (buttonContainer == null) return;
        
        GameObject fireButtonObj = new GameObject("FireButton");
        fireButtonObj.transform.SetParent(buttonContainer);
        RectTransform fireRect = fireButtonObj.AddComponent<RectTransform>();
        fireRect.sizeDelta = new Vector2(100, 100);
        fireRect.anchoredPosition = new Vector2(-150, -100);
        
        Image fireImage = fireButtonObj.AddComponent<Image>();
        fireImage.color = Color.red;
        fireButton = fireButtonObj.AddComponent<Button>();
        fireButton.targetGraphic = fireImage;
        
        Text fireText = fireButtonObj.AddComponent<Text>();
        fireText.text = "FIRE";
        fireText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        fireText.fontSize = 20;
        fireText.alignment = TextAnchor.MiddleCenter;
        
        GameObject missileButtonObj = new GameObject("MissileButton");
        missileButtonObj.transform.SetParent(buttonContainer);
        RectTransform missileRect = missileButtonObj.AddComponent<RectTransform>();
        missileRect.sizeDelta = new Vector2(100, 100);
        missileRect.anchoredPosition = new Vector2(-250, -100);
        
        Image missileImage = missileButtonObj.AddComponent<Image>();
        missileImage.color = Color.yellow;
        missileButton = missileButtonObj.AddComponent<Button>();
        missileButton.targetGraphic = missileImage;
        
        Text missileText = missileButtonObj.AddComponent<Text>();
        missileText.text = "MISSILE";
        missileText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        missileText.fontSize = 16;
        missileText.alignment = TextAnchor.MiddleCenter;
    }
    
    public MobileJoystick GetLeftJoystick()
    {
        return leftJoystick;
    }
    
    public MobileJoystick GetRightJoystick()
    {
        return rightJoystick;
    }
    
    public Button GetFireButton()
    {
        return fireButton;
    }
    
    public Button GetMissileButton()
    {
        return missileButton;
    }
}
