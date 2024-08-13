using UnityEngine;
using UnityEngine.UI;

public class Shell : MonoBehaviour
{
    private Image image;
    private Color oldColor;

    private void Start()
    {
        image = GetComponent<Image>();
        oldColor = image.color;
    }

    private void OnEnable()
    {
        PlayerPowerUps.isShieldAvilable += ShellVisual;
    }

    private void OnDestroy()
    {
        PlayerPowerUps.isShieldAvilable -= ShellVisual;
    }

    private void ShellVisual(bool isSheidlAvilable)
    {
        if (isSheidlAvilable)
        {
            image.color = oldColor;
        }
        else
        {
            image.color = Color.gray;
        }
    }
}
