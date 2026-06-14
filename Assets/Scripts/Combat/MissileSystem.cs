using UnityEngine;

public class MissileSystem : MonoBehaviour
{
    [Header("Missile Stats")]
    [SerializeField] private float speed = 3000f;
    [SerializeField] private float acceleration = 100f;
    [SerializeField] private float maxTurnRate = 360f;
    [SerializeField] private float lifetime = 60f;
    [SerializeField] private float detectionRange = 5000f;
    [SerializeField] private float damage = 50f;
    [SerializeField] private float explosionRadius = 200f;
    
    private Rigidbody rb;
    private float launchTime;
    private Transform target;
    private bool isLaunched = false;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        launchTime = Time.time;
    }
    
    private void FixedUpdate()
    {
        if (!isLaunched) return;
        
        if (Time.time - launchTime > lifetime)
        {
            Destroy(gameObject);
            return;
        }
        
        UpdateTarget();
        ApplyThrust();
        GuideMissile();
    }
    
    private void UpdateTarget()
    {
        if (target == null)
        {
            // Search for closest enemy
            Collider[] hits = Physics.OverlapSphere(transform.position, detectionRange);
            float closestDistance = detectionRange;
            
            foreach (var hit in hits)
            {
                if (hit.CompareTag("Enemy"))
                {
                    float distance = Vector3.Distance(transform.position, hit.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        target = hit.transform;
                    }
                }
            }
        }
    }
    
    private void ApplyThrust()
    {
        Vector3 thrustForce = transform.forward * speed;
        rb.velocity = thrustForce;
    }
    
    private void GuideMissile()
    {
        if (target == null) return;
        
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);
        
        if (angleToTarget > 0)
        {
            Vector3 axis = Vector3.Cross(transform.forward, directionToTarget);
            float turnAngle = Mathf.Min(maxTurnRate * Time.deltaTime, angleToTarget);
            rb.rotation = Quaternion.AngleAxis(turnAngle, axis) * rb.rotation;
        }
    }
    
    public void Launch(Transform launchPoint, Transform targetAircraft = null)
    {
        isLaunched = true;
        transform.position = launchPoint.position;
        transform.rotation = launchPoint.rotation;
        target = targetAircraft;
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if (!isLaunched) return;
        
        if (collision.CompareTag("Enemy") || collision.CompareTag("Aircraft"))
        {
            Explode();
        }
    }
    
    private void Explode()
    {
        // Deal damage
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hit in hitColliders)
        {
            if (hit.CompareTag("Aircraft") || hit.CompareTag("Enemy"))
            {
                if (hit.TryGetComponent<AircraftController>(out var aircraft))
                {
                    aircraft.TakeDamage(damage);
                }
            }
        }
        
        // Create explosion effect
        Debug.Log("Missile exploded!");
        // TODO: Instantiate explosion particle effect
        
        Destroy(gameObject);
    }
}
