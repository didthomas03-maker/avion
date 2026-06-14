using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    [Header("Battle Settings")]
    [SerializeField] private int initialEnemyCount = 8;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float battleTimeout = 1800f; // 30 minutes
    
    [Header("Prefabs")]
    [SerializeField] private GameObject enemyAircraftPrefab;
    [SerializeField] private GameObject allyAircraftPrefab;
    
    private GameObject playerAircraft;
    private int alliesDestroyed = 0;
    private int enemiesDestroyed = 0;
    private float battleTime = 0f;
    private bool battleActive = true;
    
    private void Start()
    {
        // Spawn player aircraft
        string selectedAircraft = PlayerPrefs.GetString("SelectedAircraft", "Rafale");
        AircraftData aircraftData = AircraftDatabase.GetAircraftByName(selectedAircraft);
        
        if (aircraftData != null)
        {
            playerAircraft = Instantiate(aircraftData.prefab);
            playerAircraft.tag = "Player";
            Debug.Log($"Player spawned as: {selectedAircraft}");
        }
        
        // Spawn enemy AI
        for (int i = 0; i < initialEnemyCount; i++)
        {
            SpawnEnemy();
        }
        
        Debug.Log("Battle started!");
    }
    
    private void Update()
    {
        if (!battleActive) return;
        
        battleTime += Time.deltaTime;
        
        if (battleTime > battleTimeout)
        {
            EndBattle(false); // Timeout
        }
        
        if (playerAircraft == null)
        {
            EndBattle(false); // Player destroyed
        }
        
        // Check win condition
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            EndBattle(true); // All enemies destroyed
        }
    }
    
    private void SpawnEnemy()
    {
        Vector3 randomPos = spawnPoint.position + Random.insideUnitSphere * 1000f;
        GameObject enemy = Instantiate(enemyAircraftPrefab, randomPos, Quaternion.identity);
        enemy.tag = "Enemy";
        // TODO: Attach AI controller
    }
    
    public void OnEnemyDestroyed()
    {
        enemiesDestroyed++;
        Debug.Log($"Enemy destroyed! Total: {enemiesDestroyed}");
        
        // Spawn replacement enemy
        if (battleActive)
        {
            SpawnEnemy();
        }
    }
    
    private void EndBattle(bool playerWon)
    {
        battleActive = false;
        
        if (playerWon)
        {
            Debug.Log("Victory! All enemies destroyed!");
            // TODO: Show victory screen
        }
        else
        {
            Debug.Log("Defeat!");
            // TODO: Show defeat screen
        }
        
        Invoke("ReturnToMenu", 5f);
    }
    
    private void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
