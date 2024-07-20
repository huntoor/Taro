using UnityEngine;

public class PlayerPositionClamper : MonoBehaviour
{
    [SerializeField] private int worldWidth;
    [SerializeField] private int worldHight;

    void LateUpdate()
    {
        Vector2 pos = transform.position;

        pos.x = Mathf.Max(Mathf.Min(pos.x, worldWidth), -20);
        pos.y = Mathf.Max(Mathf.Min(pos.y, worldHight), -10);

        transform.position = pos;
    }
}
