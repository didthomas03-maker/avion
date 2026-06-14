using UnityEngine;
using Mirror;

public class MultiplayerManager : NetworkManager
{
    [Header("Multiplayer Settings")]
    [SerializeField] private int maxPlayers = 2;
    [SerializeField] private float gameStartDelay = 5f;
    
    private int connectedPlayers = 0;
    
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);
        connectedPlayers++;
        Debug.Log($"Player connected. Total: {connectedPlayers}/{maxPlayers}");
        
        if (connectedPlayers == maxPlayers)
        {
            Invoke("StartGameOnServer", gameStartDelay);
        }
    }
    
    private void StartGameOnServer()
    {
        Debug.Log("Starting multiplayer game...");
        // TODO: Load battle scene with both players
    }
    
    public override void OnClientConnect()
    {
        base.OnClientConnect();
        Debug.Log("Connected to server!");
    }
    
    public override void OnClientDisconnect()
    {
        base.OnClientDisconnect();
        Debug.Log("Disconnected from server!");
    }
}
