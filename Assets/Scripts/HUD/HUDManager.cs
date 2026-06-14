using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [Header("HUD Elements")]
    [SerializeField] private Text speedText;
    [SerializeField] private Text altitudeText;
    [SerializeField] private Text fuelText;
    [SerializeField] private Text missilesText;
    [SerializeField] private Text gForceText;
    [SerializeField] private Text healthText;
    [SerializeField] private Image radarImage;
    [SerializeField] private Image crosshair;
    
    private AircraftController playerAircraft;
    private float updateRate = 0.1f;
    private float updateTimer = 0f;
    
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            playerAircraft = player.GetComponent<AircraftController>();
    }
    
    private void Update()
    {
        if (playerAircraft == null) return;
        
        updateTimer -= Time.deltaTime;
        if (updateTimer <= 0)
        {
            UpdateHUD();
            updateTimer = updateRate;
        }
    }
    
    private void UpdateHUD()
    {
        speedText.text = $"SPD: {playerAircraft.Speed:F0} km/h";
        altitudeText.text = $"ALT: {playerAircraft.Altitude:F0} m";
        fuelText.text = $"FUEL: {playerAircraft.Fuel:F0} L";
        missilesText.text = $"MISSILES: {playerAircraft.MissileCount}/8";
        gForceText.text = $"G: {playerAircraft.GForce:F1}G";
        
        // Color based on status
        if (playerAircraft.Fuel < 1000)
            fuelText.color = Color.red;
        else
            fuelText.color = Color.white;
    }
}
