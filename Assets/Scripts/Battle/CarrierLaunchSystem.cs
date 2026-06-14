using UnityEngine;

public class CarrierLaunchSystem : MonoBehaviour
{
    [Header("Catapult Settings")]
    [SerializeField] private float catapultForce = 5000f;
    [SerializeField] private float catapultDuration = 3f;
    [SerializeField] private Transform launchPoint;
    [SerializeField] private Transform holdPoint;
    
    [Header("Carrier Movement")]
    [SerializeField] private Vector3 wind = new Vector3(0, 0, 20f); // Wind direction for takeoff
    
    private Rigidbody playerAircraft;
    private float launchTimer = 0f;
    private bool isLaunching = false;
    
    private void Start()
    {
        // Position player on carrier
        GameObject playerAircraftObj = GameObject.FindGameObjectWithTag("Player");
        if (playerAircraftObj)
        {
            playerAircraft = playerAircraftObj.GetComponent<Rigidbody>();
            playerAircraft.isKinematic = true;
            playerAircraft.transform.position = holdPoint.position;
            playerAircraft.transform.rotation = holdPoint.rotation;
        }
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            BeginLaunch();
        }
        
        if (isLaunching)
        {
            CatapultLaunch();
        }
    }
    
    private void BeginLaunch()
    {
        if (!isLaunching && playerAircraft != null)
        {
            isLaunching = true;
            launchTimer = 0f;
            Debug.Log("Beginning carrier launch sequence...");
        }
    }
    
    private void CatapultLaunch()
    {
        launchTimer += Time.deltaTime;
        float progress = launchTimer / catapultDuration;
        
        if (progress < 1f)
        {
            // Move aircraft along catapult
            playerAircraft.transform.position = Vector3.Lerp(holdPoint.position, launchPoint.position, progress);
            playerAircraft.transform.rotation = holdPoint.rotation;
        }
        else
        {
            // Release from catapult
            playerAircraft.isKinematic = false;
            playerAircraft.velocity = launchPoint.forward * 100f + wind;
            isLaunching = false;
            Debug.Log("Aircraft launched!");
        }
    }
}
