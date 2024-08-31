using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private CrossFade crossFade;
    
    public void PlayButton()
    {
        crossFade.StartFadeOut();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("LevelOne");
    }

    public void InfiniteModeButton()
    {
        SceneManager.LoadScene("InfiniteMode");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
