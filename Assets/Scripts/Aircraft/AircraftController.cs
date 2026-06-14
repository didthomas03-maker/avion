using UnityEngine;
using UnityEngine.InputSystem;

public class AircraftController : MonoBehaviour
{
    [Header("Aircraft Stats")]
    [SerializeField] private float maxSpeed = 2500f; // km/h
    [SerializeField] private float acceleration = 50f;
    [SerializeField] private float maxAltitude = 15000f; // meters
    [SerializeField] private float fuelCapacity = 5000f;
    [SerializeField] private float fuelConsumption = 2f; // per second at full throttle
    
    [Header("Aerodynamics")]
    [SerializeField] private float lift = 15f;
    [SerializeField] private float drag = 0.1f;
    [SerializeField] private float rollSpeed = 180f;
    [SerializeField] private float pitchSpeed = 90f;
    [SerializeField] private float yawSpeed = 60f;
    [SerializeField] private float maxGForce = 9f;
    
    [Header("Weapons")]
    [SerializeField] private int maxMissiles = 8;
    [SerializeField] private int maxCannon = 500;
    
    private Rigidbody rb;
    private float currentSpeed = 0f;
    private float currentAltitude = 0f;
    private float currentFuel;
    private float throttle = 0f;
    private float roll = 0f;
    private float pitch = 0f;
    private float yaw = 0f;
    
    private int missileCount;
    private int cannonAmmo;
    private float gForce = 1f;
    
    private InputAction moveAction;
    private InputAction throttleAction;
    private InputAction fireAction;
    
    public string aircraftName { get; set; }
    public float Fuel { get => currentFuel; }
    public float Speed { get => currentSpeed; }
    public float Altitude { get => currentAltitude; }
    public float GForce { get => gForce; }
    public int MissileCount { get => missileCount; }
    public int CannonAmmo { get => cannonAmmo; }
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody>();
            
        currentFuel = fuelCapacity;
        missileCount = maxMissiles;
        cannonAmmo = maxCannon;
    }
    
    private void OnEnable()
    {
        var inputMap = InputSystem.actions;
        moveAction = inputMap.FindAction("Look");
        throttleAction = inputMap.FindAction("Move");
        fireAction = inputMap.FindAction("Fire");
    }
    
    private void Update()
    {
        HandleInput();
        UpdateAltitude();
        UpdateFuel();
        UpdateGForce();
    }
    
    private void FixedUpdate()
    {
        ApplyPhysics();
        UpdateSpeed();
    }
    
    private void HandleInput()
    {
        // Throttle input (W/S or triggers)
        throttle = Mathf.Clamp01(throttle + Input.GetAxis("Vertical") * Time.deltaTime);
        
        // Rotation input (Mouse/Gamepad)
        pitch = Input.GetAxis("Vertical") * pitchSpeed;
        roll = Input.GetAxis("Horizontal") * rollSpeed;
        yaw = Input.GetAxis("Horizontal") * yawSpeed * 0.5f;
        
        // Fire missiles (Space)
        if (Input.GetKeyDown(KeyCode.Space) && missileCount > 0)
        {
            FireMissile();
        }
        
        // Fire cannon (Mouse click)
        if (Input.GetMouseButton(0) && cannonAmmo > 0)
        {
            FireCannon();
        }
    }
    
    private void ApplyPhysics()
    {
        // Apply rotation
        Vector3 rotation = new Vector3(pitch, yaw, roll) * Time.deltaTime;
        rb.AddTorque(rotation, ForceMode.VelocityChange);
        
        // Apply forward thrust
        Vector3 forwardForce = transform.forward * (throttle * acceleration * 100f);
        rb.AddForce(forwardForce, ForceMode.Force);
        
        // Apply drag
        rb.velocity *= (1f - drag * Time.fixedDeltaTime);
        
        // Speed limit
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
    
    private void UpdateSpeed()
    {
        currentSpeed = rb.velocity.magnitude * 3.6f; // Convert to km/h
    }
    
    private void UpdateAltitude()
    {
        currentAltitude = transform.position.y;
        
        // Prevent going below ground
        if (currentAltitude < 50f)
        {
            transform.position = new Vector3(transform.position.x, 50f, transform.position.z);
        }
        
        // Prevent going above max altitude
        if (currentAltitude > maxAltitude)
        {
            transform.position = new Vector3(transform.position.x, maxAltitude, transform.position.z);
        }
    }
    
    private void UpdateFuel()
    {
        currentFuel -= throttle * fuelConsumption * Time.deltaTime;
        currentFuel = Mathf.Clamp(currentFuel, 0, fuelCapacity);
        
        if (currentFuel <= 0)
        {
            throttle = 0;
        }
    }
    
    private void UpdateGForce()
    {
        // Calculate G-force based on acceleration
        float acceleration_magnitude = (rb.velocity - rb.velocity) / Time.deltaTime;
        gForce = (rb.acceleration.magnitude + 9.81f) / 9.81f;
        gForce = Mathf.Clamp(gForce, 1f, maxGForce);
    }
    
    private void FireMissile()
    {
        if (missileCount > 0)
        {
            Debug.Log("Missile fired!");
            missileCount--;
            // TODO: Instantiate missile prefab
        }
    }
    
    private void FireCannon()
    {
        if (cannonAmmo > 0)
        {
            cannonAmmo--;
            // TODO: Raycast or particle effect
        }
    }
    
    public void TakeDamage(float damage)
    {
        Debug.Log($"Aircraft took {damage} damage!");
        // TODO: Health system
    }
    
    public void Refuel(float amount)
    {
        currentFuel = Mathf.Min(currentFuel + amount, fuelCapacity);
    }
}
