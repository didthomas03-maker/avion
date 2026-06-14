using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class MultiplayerManager : MonoBehaviour
{
    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private int maxPlayers = 4;

    private void Start()
    {
        if (networkManager == null)
            networkManager = FindObjectOfType<NetworkManager>();

        networkManager.ConnectionApprovalCallback += ApprovalCheck;
    }

    private void ApprovalCheck(
        NetworkManager.ConnectionApprovalRequest request,
        NetworkManager.ConnectionApprovalResponse response)
    {
        response.Approved = true;
        response.CreatePlayerObject = true;

        if (NetworkManager.Singleton.ConnectedClients.Count >= maxPlayers)
            response.Approved = false;
    }

    public void StartHostGame()
    {
        networkManager.StartHost();
        LoadGameScene();
    }

    public void StartClientGame(string serverAddress)
    {
        networkManager.GetComponent<NetworkConfig>();
        networkManager.StartClient();
        LoadGameScene();
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene("CarrierScene");
    }

    private void OnDestroy()
    {
        if (networkManager != null)
            networkManager.ConnectionApprovalCallback -= ApprovalCheck;
    }
}
