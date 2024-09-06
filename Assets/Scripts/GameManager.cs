using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameFinishedPanel;
    [SerializeField] private GameObject countdownPanel;

    private TextMeshProUGUI counterTimerText;
    private float countdownTimer;
    
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

        // Time.timeScale = 0f;
    }

    private void Start()
    {
        playerStatus = PlayerSaveSystem.Instance.CurrentPlayerStatus;

        counterTimerText = countdownPanel.GetComponentInChildren<TextMeshProUGUI>();
        countdownTimer = 3f;
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

    public void StartGame()
    {
        StartCoroutine(GameCounterEnum());
    }

    private IEnumerator GameCounterEnum()
    {
        while (countdownTimer > 0f)
        {
            counterTimerText.text = countdownTimer.ToString();

            countdownTimer--;

            yield return new WaitForSecondsRealtime(1f);
        }
        countdownPanel.SetActive(false);

        Time.timeScale = 1f;

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
