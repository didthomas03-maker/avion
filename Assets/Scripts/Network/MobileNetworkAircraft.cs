using UnityEngine;
using Mirror;

[RequireComponent(typeof(NetworkIdentity))]
[RequireComponent(typeof(NetworkTransform))]
public class MobileNetworkAircraft : NetworkBehaviour
{
    [Header("Network Aircraft Settings")]
    [SerializeField] private float syncRate = 0.1f;
    [SerializeField] private float speed = 100f;
    [SerializeField] private float rotationSpeed = 5f;
    
    private AircraftController localAircraft;
    private Rigidbody rb;
    private float syncTimer = 0f;
    
    [SyncVar] private Vector3 networkPosition;
    [SyncVar] private Quaternion networkRotation;
    [SyncVar] private int networkMissileCount;
    [SyncVar] private float networkHealth = 100f;
    
    private void Start()
    {
        localAircraft = GetComponent<AircraftController>();
        rb = GetComponent<Rigidbody>();
        
        if (isLocalPlayer)
        {
            Debug.Log("[NETWORK] Avion local du joueur configuré");
        }
    }
    
    private void Update()
    {
        if (!isLocalPlayer)
            return;
        
        syncTimer += Time.deltaTime;
        
        if (syncTimer >= syncRate)
        {
            CmdUpdateAircraftState(
                transform.position,
                transform.rotation,
                localAircraft.MissileCount,
                100f
            );
            syncTimer = 0f;
        }
    }
    
    private void LateUpdate()
    {
        if (isLocalPlayer)
            return;
        
        // Synchroniser position/rotation pour les autres joueurs
        transform.position = Vector3.Lerp(transform.position, networkPosition, Time.deltaTime * rotationSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, networkRotation, Time.deltaTime * rotationSpeed);
    }
    
    [Command]
    private void CmdUpdateAircraftState(Vector3 pos, Quaternion rot, int missiles, float health)
    {
        networkPosition = pos;
        networkRotation = rot;
        networkMissileCount = missiles;
        networkHealth = health;
    }
    
    [Command]
    public void CmdFireMissile(Vector3 targetPos)
    {
        Debug.Log($"[NETWORK] Missile tiré par joueur sur {targetPos}");
        RpcFireMissileEffect(targetPos);
    }
    
    [ClientRpc]
    private void RpcFireMissileEffect(Vector3 targetPos)
    {
        Debug.Log("[NETWORK] Effet missile synchronisé");
    }
    
    [Command]
    public void CmdTakeDamage(float damage)
    {
        networkHealth -= damage;
        if (networkHealth <= 0)
        {
            RpcAircraftDestroyed();
        }
    }
    
    [ClientRpc]
    private void RpcAircraftDestroyed()
    {
        Debug.Log("[NETWORK] Avion détruit!");
        Destroy(gameObject);
    }
    
    public int GetNetworkMissileCount()
    {
        return networkMissileCount;
    }
    
    public float GetNetworkHealth()
    {
        return networkHealth;
    }
}
