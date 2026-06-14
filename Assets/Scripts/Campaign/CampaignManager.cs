using UnityEngine;
using UnityEngine.UI;

public class CampaignManager : MonoBehaviour
{
    [Header("Campaign Settings")]
    [SerializeField] private int totalMissions = 10;
    [SerializeField] private Text missionCountText;
    [SerializeField] private Image progressBar;
    
    private int completedMissions = 0;
    
    private static CampaignManager instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void CompleteMission()
    {
        completedMissions++;
        UpdateCampaignProgress();
        
        if (completedMissions >= totalMissions)
        {
            CompleteCampaign();
        }
    }
    
    private void UpdateCampaignProgress()
    {
        if (missionCountText != null)
            missionCountText.text = $"Mission {completedMissions}/{totalMissions}";
        
        if (progressBar != null)
            progressBar.fillAmount = (float)completedMissions / totalMissions;
    }
    
    private void CompleteCampaign()
    {
        Debug.Log("Campagne terminée!");
    }
    
    public int GetCompletedMissions()
    {
        return completedMissions;
    }
}
