using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerSaveSystem playerSaveSystem;
    
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
        playerSaveSystem.SaveData();

        Application.Quit();
    }
}
