using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    
    [SerializeField] private UnityEvent<Collider2D> OnDetectionZoneEntered;

    private void OnTriggerEnter2D(Collider2D body)
    {
        if (body.CompareTag("Player"))
        {
            OnDetectionZoneEntered?.Invoke(body);
        }
    }
}
