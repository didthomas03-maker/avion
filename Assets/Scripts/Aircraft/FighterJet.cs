using UnityEngine;

public class FighterJet : AircraftBase
{
    public enum JetType
    {
        Rafale,
        F35,
        F22,
        F15,
        F16
    }

    [SerializeField] private JetType jetType = JetType.F16;
    [SerializeField] private float afterburnerMultiplier = 1.5f;
    [SerializeField] private float fuelCapacity = 100f;

    private float currentFuel;
    private bool afterburnerActive = false;

    protected override void Start()
    {
        base.Start();
        currentFuel = fuelCapacity;
        ApplyJetStats();
    }

    protected override void Update()
    {
        base.Update();
        
        if (!IsOwner) return;
        
        UpdateFuel();
        HandleAfterburner();
    }

    private void ApplyJetStats()
    {
        switch (jetType)
        {
            case JetType.Rafale:
                maxSpeed = 340f;
                turnSpeed = 120f;
                acceleration = 80f;
                health = 90f;
                break;
            case JetType.F35:
                maxSpeed = 325f;
                turnSpeed = 110f;
                acceleration = 75f;
                health = 100f;
                break;
            case JetType.F22:
                maxSpeed = 350f;
                turnSpeed = 125f;
                acceleration = 90f;
                health = 110f;
                break;
            case JetType.F15:
                maxSpeed = 320f;
                turnSpeed = 100f;
                acceleration = 70f;
                health = 120f;
                break;
            case JetType.F16:
                maxSpeed = 315f;
                turnSpeed = 115f;
                acceleration = 85f;
                health = 85f;
                break;
        }
    }

    private void HandleAfterburner()
    {
        if (Input.GetKey(KeyCode.LeftControl) && currentFuel > 0)
        {
            afterburnerActive = true;
            maxSpeed = maxSpeed * afterburnerMultiplier;
            currentFuel -= Time.deltaTime * 20f;
        }
        else
        {
            afterburnerActive = false;
            ApplyJetStats();
        }
    }

    private void UpdateFuel()
    {
        if (engineRunning && !isGrounded)
            currentFuel -= Time.deltaTime * 5f;

        currentFuel = Mathf.Max(0, currentFuel);

        if (currentFuel <= 0)
            currentSpeed = 0f;
    }

    public float GetFuel() => currentFuel;
    public string GetJetName() => jetType.ToString();
    public bool IsAfterburnerActive() => afterburnerActive;
}
