using UnityEngine;

public class PlayerPositionClamper : MonoBehaviour
{
    [SerializeField] private int worldWidth;
    [SerializeField] private int worldHight;

    private float lastX;
    private float backwardBuffer;

    private void Start()
    {
        lastX = transform.position.x;

        backwardBuffer = 10;
    }

    void LateUpdate()
    {
        Vector2 pos = transform.position;
        
        // Ensure the x position only increases
        if (pos.x < lastX - backwardBuffer)
        {
            pos.x = lastX - backwardBuffer;
        }
        else if (pos.x > lastX)
        {
            lastX = pos.x;
        }

        //pos.x = Mathf.Max(Mathf.Min(pos.x, worldWidth), -20);

        pos.x = Mathf.Min(pos.x, worldWidth);

        pos.y = Mathf.Max(Mathf.Min(pos.y, worldHight), -10);

        transform.position = pos;
    }
}
