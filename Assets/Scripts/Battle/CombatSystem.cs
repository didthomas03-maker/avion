using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    [Header("Combat Settings")]
    [SerializeField] private float lockOnRange = 3000f;
    [SerializeField] private float lockOnTime = 2f;
    
    private Transform currentTarget;
    private float lockOnTimer = 0f;
    private bool isLockedOn = false;
    private AircraftController playerAircraft;
    
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            playerAircraft = player.GetComponent<AircraftController>();
    }
    
    private void Update()
    {
        SearchForTarget();
        UpdateLockOn();
    }
    
    private void SearchForTarget()
    {
        if (currentTarget != null) return;
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, lockOnRange);
        float closestDistance = lockOnRange;
        
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    currentTarget = collider.transform;
                }
            }
        }
    }
    
    private void UpdateLockOn()
    {
        if (currentTarget == null)
        {
            isLockedOn = false;
            lockOnTimer = 0f;
            return;
        }
        
        Vector3 directionToTarget = (currentTarget.position - transform.position).normalized;
        float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);
        
        if (angleToTarget < 15f)
        {
            lockOnTimer += Time.deltaTime;
            if (lockOnTimer >= lockOnTime)
            {
                isLockedOn = true;
            }
        }
        else
        {
            lockOnTimer = 0f;
            isLockedOn = false;
        }
    }
    
    public Transform GetTarget()
    {
        return currentTarget;
    }
    
    public bool IsLockedOn()
    {
        return isLockedOn;
    }
}
