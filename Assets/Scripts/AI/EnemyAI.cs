using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("AI Settings")]
    [SerializeField] private float detectionRange = 3000f;
    [SerializeField] private float attackRange = 1500f;
    [SerializeField] private float patrolSpeed = 0.5f;
    
    private AircraftController aircraftController;
    private Transform playerTarget;
    private float shootCooldown = 0f;
    private float shootCooldownMax = 2f;
    
    public enum AIState { Patrol, Chase, Attack, Evade }
    private AIState currentState = AIState.Patrol;
    
    private void Start()
    {
        aircraftController = GetComponent<AircraftController>();
        gameObject.tag = "Enemy";
    }
    
    private void Update()
    {
        SearchForTarget();
        UpdateAIState();
        ExecuteAIBehavior();
        
        shootCooldown -= Time.deltaTime;
    }
    
    private void SearchForTarget()
    {
        if (playerTarget != null) return;
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < detectionRange)
            {
                playerTarget = player.transform;
            }
        }
    }
    
    private void UpdateAIState()
    {
        if (playerTarget == null)
        {
            currentState = AIState.Patrol;
            return;
        }
        
        float distanceToTarget = Vector3.Distance(transform.position, playerTarget.position);
        
        if (distanceToTarget > detectionRange)
        {
            playerTarget = null;
            currentState = AIState.Patrol;
        }
        else if (distanceToTarget < attackRange)
        {
            currentState = AIState.Attack;
        }
        else
        {
            currentState = AIState.Chase;
        }
    }
    
    private void ExecuteAIBehavior()
    {
        switch (currentState)
        {
            case AIState.Patrol:
                Patrol();
                break;
            case AIState.Chase:
                Chase();
                break;
            case AIState.Attack:
                Attack();
                break;
            case AIState.Evade:
                Evade();
                break;
        }
    }
    
    private void Patrol()
    {
        // Move forward at patrol speed
        // TODO: Implement waypoint system
    }
    
    private void Chase()
    {
        if (playerTarget == null) return;
        
        Vector3 directionToTarget = (playerTarget.position - transform.position).normalized;
        // Move towards target
    }
    
    private void Attack()
    {
        if (playerTarget == null) return;
        
        Vector3 directionToTarget = (playerTarget.position - transform.position).normalized;
        float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);
        
        // Turn towards target
        if (angleToTarget > 5f)
        {
            Vector3 crossProduct = Vector3.Cross(transform.forward, directionToTarget);
            transform.RotateAround(transform.position, crossProduct, angleToTarget * Time.deltaTime);
        }
        
        // Fire missiles
        if (shootCooldown <= 0)
        {
            // TODO: Fire missile
            shootCooldown = shootCooldownMax;
        }
    }
    
    private void Evade()
    {
        // Perform evasive maneuvers
        // TODO: Implement barrel roll and climb
    }
}
