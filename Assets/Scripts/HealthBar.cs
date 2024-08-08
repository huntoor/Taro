using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void OnEnable()
    {
        Player.setMaxHealth += SetMaxHealth;
        Player.updateHealth += SetHealth;
    }

    private void SetMaxHealth(int hp)
    {
        slider.maxValue = hp;
        slider.value = hp;
    }

    private void SetHealth(int hp)
    {
        slider.value = hp;
    }
}
