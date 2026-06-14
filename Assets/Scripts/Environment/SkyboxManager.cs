using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    [Header("Time of Day")]
    [SerializeField] private float timeOfDay = 0.5f; // 0 = night, 1 = day
    [SerializeField] private Light sunLight;
    [SerializeField] private Color dayColor = Color.white;
    [SerializeField] private Color nightColor = Color.blue;
    
    private void Update()
    {
        UpdateLighting();
    }
    
    private void UpdateLighting()
    {
        if (sunLight != null)
        {
            // Rotate sun based on time
            sunLight.transform.rotation = Quaternion.Euler((timeOfDay - 0.25f) * 360f, 170f, 0f);
            sunLight.color = Color.Lerp(nightColor, dayColor, timeOfDay);
            sunLight.intensity = Mathf.Lerp(0.3f, 1f, timeOfDay);
        }
    }
}
