using UnityEngine;
using Mirror;

public class MobileNetworkSpawner : NetworkBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject aircraftPrefab;
    [SerializeField] private Vector3[] spawnPoints;
    [SerializeField] private float spawnHeight = 100f;
    
    private int spawnIndex = 0;
    
    private void Start()
    {
        if (!isServer)
            return;
        
        // Initialiser points de spawn
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            spawnPoints = new Vector3[]
            {
                new Vector3(-50, spawnHeight, 0),
                new Vector3(50, spawnHeight, 0)
            };
        }
    }
    
    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("[NETWORK] Serveur spawn activé");
    }
    
    [Command]
    public void CmdSpawnAircraft(NetworkConnection conn)
    {
        if (spawnIndex >= spawnPoints.Length)
            spawnIndex = 0;
        
        Vector3 spawnPos = spawnPoints[spawnIndex];
        GameObject aircraft = Instantiate(aircraftPrefab, spawnPos, Quaternion.identity);
        
        NetworkServer.AddPlayerForConnection(conn, aircraft);
        
        Debug.Log($"[NETWORK] Avion spawné à {spawnPos} pour joueur");
        spawnIndex++;
    }
}
