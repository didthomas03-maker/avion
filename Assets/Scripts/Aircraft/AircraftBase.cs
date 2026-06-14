using UnityEngine;
using Unity.Netcode;

public class AircraftBase : NetworkBehaviour
{
    [Header("Aircraft Stats")]
    [SerializeField] protected float maxSpeed = 300f;
    [SerializeField] protected float acceleration = 50f;
    [SerializeField] protected float turnSpeed = 100f;
    [SerializeField] protected float rollSpeed = 150f;
    [SerializeField] protected float health = 100f;
    [SerializeField] protected float altitude = 0f;
    [SerializeField] protected float maxAltitude = 10000f;

    [Header("Weapons")]
    [SerializeField] protected int missileCount = 8;
    [SerializeField] protected float missileSpeed = 500f;
    [SerializeField] protected GameObject missilePrefab;
    [SerializeField] protected Transform[] missileSpawnPoints;

    [Header("References")]
    [SerializeField] protected Transform engineExhaust;
    [SerializeField] protected ParticleSystem engineTrail;

    protected float currentSpeed = 0f;
    protected float currentHealth;
    protected Vector3 velocity = Vector3.zero;
    protected Vector3 inputDirection = Vector3.zero;
    protected int remainingMissiles;
    protected bool isGrounded = true;
    protected bool engineRunning = false;

    protected virtual void Start()
    {
        currentHealth = health;
        remainingMissiles = missileCount;
        
        if (missilePrefab == null)
            Debug.LogWarning("Missile prefab not assigned!");
    }

    protected virtual void Update()
    {
        if (!IsOwner) return;

        HandleInput();
        UpdatePhysics();
        UpdateAltitude();
        HandleWeapons();
    }

    protected virtual void HandleInput()
    {
        float pitch = Input.GetAxis("Vertical");
        float yaw = Input.GetAxis("Horizontal");
        float roll = 0f;
        
        if (Input.GetKey(KeyCode.Q)) roll = 1f;
        if (Input.GetKey(KeyCode.D)) roll = -1f;

        inputDirection = new Vector3(pitch, yaw, roll);

        if (Input.GetKey(KeyCode.Space))
            currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, maxSpeed);
        else if (Input.GetKey(KeyCode.LeftShift))
            currentSpeed = Mathf.Max(currentSpeed - acceleration * Time.deltaTime, 0f);

        if (Input.GetKeyDown(KeyCode.R))
            ToggleEngine();
    }

    protected virtual void UpdatePhysics()
    {
        if (!engineRunning || isGrounded) return;

        // Rotation
        float pitchRotation = inputDirection.x * turnSpeed * Time.deltaTime;
        float yawRotation = inputDirection.y * turnSpeed * Time.deltaTime;
        float rollRotation = inputDirection.z * rollSpeed * Time.deltaTime;

        transform.Rotate(pitchRotation, yawRotation, rollRotation, Space.Self);

        // Mouvement vers l'avant
        velocity = transform.forward * currentSpeed;
        transform.position += velocity * Time.deltaTime;

        // Applique la gravité (sauf en vol)
        if (altitude > 0)
        {
            velocity.y -= 9.81f * Time.deltaTime;
        }
    }

    protected virtual void UpdateAltitude()
    {
        altitude = Mathf.Clamp(transform.position.y, 0, maxAltitude);
        
        if (altitude <= 0)
        {
            altitude = 0;
            isGrounded = true;
            currentSpeed = 0f;
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        else
        {
            isGrounded = false;
        }
    }

    protected virtual void HandleWeapons()
    {
        if (Input.GetKeyDown(KeyCode.F) && remainingMissiles > 0 && !isGrounded)
        {
            FireMissileServerRpc();
        }
    }

    [Rpc(SendTo.Server)]
    protected void FireMissileServerRpc()
    {
        if (remainingMissiles <= 0) return;

        foreach (Transform spawnPoint in missileSpawnPoints)
        {
            if (spawnPoint != null)
            {
                GameObject missile = Instantiate(
                    missilePrefab,
                    spawnPoint.position,
                    spawnPoint.rotation
                );

                Missile missileScript = missile.GetComponent<Missile>();
                if (missileScript != null)
                {
                    missileScript.Launch(velocity + transform.forward * missileSpeed);
                }

                remainingMissiles--;
                break;
            }
        }
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Destroy(gameObject);
    }

    public virtual void ToggleEngine()
    {
        if (!isGrounded) return;
        
        engineRunning = !engineRunning;
        
        if (engineTrail != null)
            if (engineRunning) engineTrail.Play();
            else engineTrail.Stop();
    }

    public float GetSpeed() => currentSpeed;
    public float GetAltitude() => altitude;
    public float GetHealth() => currentHealth;
    public int GetMissileCount() => remainingMissiles;
    public bool IsEngineRunning() => engineRunning;
}
