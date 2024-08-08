using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameFinishedPanel;
    
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
        gameFinishedPanel.SetActive(true);

        Time.timeScale = 0f;
    }
}
