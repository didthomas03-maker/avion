using UnityEngine;
using Unity.Netcode;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject aircraftPrefab;
    [SerializeField] private Transform carrierSpawnPoint;
    [SerializeField] private int maxPlayers = 4;

    private int playersReady = 0;
    private NetworkManager networkManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        networkManager = FindObjectOfType<NetworkManager>();
    }

    public void SpawnAircraft(Vector3 position)
    {
        if (aircraftPrefab == null)
        {
            Debug.LogError("Aircraft prefab not assigned!");
            return;
        }

        GameObject aircraft = Instantiate(
            aircraftPrefab,
            position,
            Quaternion.identity
        );

        NetworkObject networkObject = aircraft.GetComponent<NetworkObject>();
        if (networkObject != null)
            networkObject.Spawn();
    }

    public void PlayerReady()
    {
        playersReady++;
        if (playersReady >= maxPlayers)
            StartBattle();
    }

    private void StartBattle()
    {
        Debug.Log("Battle started!");
        // Implement battle start logic
    }

    public void EndGame()
    {
        if (networkManager != null && networkManager.IsServer)
            networkManager.Shutdown();
    }
}
