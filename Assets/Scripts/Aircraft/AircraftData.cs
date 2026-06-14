using UnityEngine;

[System.Serializable]
public class AircraftData
{
    public string aircraftName;
    public string nation;
    public float maxSpeed;
    public float acceleration;
    public float turnRate;
    public int missileCapacity;
    public int cannonAmmo;
    public float fuelCapacity;
    public float armor;
    public Sprite thumbnail;
    public GameObject prefab;
}

public class AircraftDatabase : MonoBehaviour
{
    [SerializeField] private AircraftData[] aircraftList;
    
    private static AircraftDatabase instance;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    
    public static AircraftData GetAircraft(int index)
    {
        return instance.aircraftList[index];
    }
    
    public static AircraftData GetAircraftByName(string name)
    {
        foreach (var aircraft in instance.aircraftList)
        {
            if (aircraft.aircraftName == name)
                return aircraft;
        }
        return null;
    }
    
    public static int GetAircraftCount()
    {
        return instance.aircraftList.Length;
    }
}
