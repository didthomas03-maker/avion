using UnityEngine;

public class AerodynamicsEngine : MonoBehaviour
{
    [Header("Environmental Settings")]
    [SerializeField] private float seaLevelDensity = 1.225f; // kg/m³
    [SerializeField] private Vector3 wind = Vector3.zero;
    [SerializeField] private float weatherFactor = 1f;
    
    private Rigidbody rb;
    private AircraftController aircraftController;
    
    private float wingArea = 122.4f; // Rafale wing area
    private float dragCoefficient = 0.018f;
    private float liftCoefficient = 0.4f;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        aircraftController = GetComponent<AircraftController>();
    }
    
    private void FixedUpdate()
    {
        ApplyDrag();
        ApplyLift();
        ApplyWind();
    }
    
    private void ApplyDrag()
    {
        float airDensity = CalculateAirDensity(aircraftController.Altitude);
        float dragForce = 0.5f * airDensity * (rb.velocity.magnitude * rb.velocity.magnitude) * dragCoefficient * wingArea;
        
        Vector3 dragDirection = -rb.velocity.normalized;
        rb.AddForce(dragDirection * dragForce, ForceMode.Force);
    }
    
    private void ApplyLift()
    {
        float airDensity = CalculateAirDensity(aircraftController.Altitude);
        float velocitySquared = rb.velocity.magnitude * rb.velocity.magnitude;
        
        float liftForce = 0.5f * airDensity * velocitySquared * liftCoefficient * wingArea;
        
        // Lift acts upward
        rb.AddForce(Vector3.up * liftForce, ForceMode.Force);
    }
    
    private void ApplyWind()
    {
        rb.AddForce(wind * weatherFactor, ForceMode.Force);
    }
    
    private float CalculateAirDensity(float altitude)
    {
        // Simplified atmospheric model
        // Density decreases exponentially with altitude
        return seaLevelDensity * Mathf.Exp(-altitude / 8500f);
    }
}
