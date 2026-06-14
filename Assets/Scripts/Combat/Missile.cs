using UnityEngine;
using Unity.Netcode;

public class Missile : NetworkBehaviour
{
    [SerializeField] private float speed = 500f;
    [SerializeField] private float lifeTime = 30f;
    [SerializeField] private float damage = 50f;
    [SerializeField] private float explosionRadius = 100f;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private Light warheadLight;

    private Vector3 direction;
    private float launchTime;
    private bool hasExploded = false;
    private Transform target;

    private void Start()
    {
        launchTime = Time.time;
        if (trailRenderer != null)
            trailRenderer.Clear();
    }

    private void Update()
    {
        if (hasExploded) return;

        // Mouvement
        transform.position += direction * speed * Time.deltaTime;

        // Rotation vers la direction
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction);

        // Vérifier la durée de vie
        if (Time.time - launchTime > lifeTime)
            Explode();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Aircraft"))
        {
            AircraftBase aircraft = collision.GetComponent<AircraftBase>();
            if (aircraft != null)
            {
                aircraft.TakeDamage(damage);
                Explode();
            }
        }
        else if (collision.CompareTag("Terrain") || collision.CompareTag("Ground"))
        {
            Explode();
        }
    }

    public void Launch(Vector3 launchVelocity)
    {
        direction = launchVelocity.normalized;
        speed = launchVelocity.magnitude > 0 ? launchVelocity.magnitude : speed;
    }

    private void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;

        // Instantiate explosion
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        // Damage nearby aircraft
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            AircraftBase aircraft = collider.GetComponent<AircraftBase>();
            if (aircraft != null)
            {
                float distance = Vector3.Distance(transform.position, aircraft.transform.position);
                float damageAmount = damage * (1 - distance / explosionRadius);
                aircraft.TakeDamage(damageAmount);
            }
        }

        // Destroy missile
        Destroy(gameObject);
    }
}
