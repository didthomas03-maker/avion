using UnityEngine;
using Unity.Netcode;

public class CarrierManager : NetworkBehaviour
{
    [SerializeField] private Transform runwayStart;
    [SerializeField] private Transform runwayEnd;
    [SerializeField] private float runwayLength = 300f;
    [SerializeField] private GameObject aircraftSpawnPrefab;

    public Transform GetRunwayStart() => runwayStart;
    public Transform GetRunwayEnd() => runwayEnd;
    public float GetRunwayLength() => runwayLength;

    public void SpawnAircraftAtCarrier(Vector3 offset = default)
    {
        if (runwayStart == null)
        {
            Debug.LogError("Runway start point not assigned!");
            return;
        }

        Vector3 spawnPosition = runwayStart.position + offset;
        GameObject aircraft = Instantiate(
            aircraftSpawnPrefab,
            spawnPosition,
            runwayStart.rotation
        );

        NetworkObject networkObject = aircraft.GetComponent<NetworkObject>();
        if (networkObject != null)
            networkObject.Spawn();

        AircraftBase aircraftBase = aircraft.GetComponent<AircraftBase>();
        if (aircraftBase != null)
            aircraftBase.ToggleEngine();
    }
}
