using UnityEngine;
using UnityEngine.UI;

public class HUDDisplay : MonoBehaviour
{
    [SerializeField] private FighterJet playerAircraft;
    [SerializeField] private Text speedText;
    [SerializeField] private Text altitudeText;
    [SerializeField] private Text healthText;
    [SerializeField] private Text missileText;
    [SerializeField] private Text fuelText;
    [SerializeField] private Image radarImage;
    [SerializeField] private Image healthBar;
    [SerializeField] private Canvas hudCanvas;

    private void Update()
    {
        if (playerAircraft == null) return;

        UpdateDisplays();
    }

    private void UpdateDisplays()
    {
        float speed = playerAircraft.GetSpeed();
        float altitude = playerAircraft.GetAltitude();
        float health = playerAircraft.GetHealth();
        int missiles = playerAircraft.GetMissileCount();
        float fuel = playerAircraft.GetFuel();

        if (speedText != null)
            speedText.text = $"SPEED: {speed:F0} m/s";

        if (altitudeText != null)
            altitudeText.text = $"ALT: {altitude:F0} m";

        if (healthText != null)
            healthText.text = $"HEALTH: {health:F0}%";

        if (missileText != null)
            missileText.text = $"MISSILES: {missiles}";

        if (fuelText != null)
            fuelText.text = $"FUEL: {fuel:F1}%";

        if (healthBar != null)
            healthBar.fillAmount = health / 100f;
    }
}
