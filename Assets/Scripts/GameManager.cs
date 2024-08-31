using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameFinishedPanel;
    
    private PlayerStatus playerStatus;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else 
        {
            Instance = this;
        }
    }

    private void Start()
    {
        playerStatus = PlayerSaveSystem.Instance.CurrentPlayerStatus;
    }

    private void OnEnable()
    {
        Player.playerDead += StopGame;
        RoomManager.onLastRoomFinished += GoToNextLevel;
    }

    private void OnDestroy()
    {
        Player.playerDead -= StopGame;
        RoomManager.onLastRoomFinished -= GoToNextLevel;
    }

    private void GameStarted()
    {
        
    }
    
    public void PauseButtonPressed()
    {
        Time.timeScale = 0f;
    }

    public void ResumeButtonPressed()
    {
        Time.timeScale = 1f;
    }

    public void QuitButtonPressed()
    {
        // playerSaveSystem.SaveData();

        Application.Quit();
    }

    public void RestartButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Time.timeScale = 1f;
    }

    private void StopGame()
    {
        gameOverPanel.SetActive(true);
        
        Time.timeScale = 0f;
    }

    private void GoToNextLevel()
    {
        PlayerSaveSystem.Instance.SaveData(playerStatus);

        gameFinishedPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void SetPlayerData(PlayerStatus newPlayerStatus)
    {
        playerStatus = newPlayerStatus;
    }
}
