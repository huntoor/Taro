using UnityEngine;

public class PlayerPositionClamper : MonoBehaviour
{
    [SerializeField] private int worldWidth;
    [SerializeField] private int worldHight;

    private float lastX;
    private float backwardBuffer;

    private float halfPlayerSizeX;

    private void Start()
    {
        lastX = transform.position.x;
        backwardBuffer = 10;

        halfPlayerSizeX = GetComponent<SpriteRenderer>().bounds.size.x / 2;

    }

    void LateUpdate()
    {
        Vector3 playerPos = transform.position;

        ClampPlayerInScreen(playerPos);
    }

    private void ClampPlayerInWorld(Vector3 pos)
    {
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

    private void ClampPlayerInScreen(Vector3 pos)
    {
        float distance = pos.z - Camera.main.transform.position.z;

        float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + halfPlayerSizeX;
        float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance)).x - halfPlayerSizeX;

        float topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)).y + halfPlayerSizeX;
        float bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance)).y - halfPlayerSizeX;

        pos.x = Mathf.Clamp(pos.x, leftBorder, rightBorder);
        pos.y = Mathf.Clamp(pos.y, topBorder, bottomBorder);
        transform.position = pos;
    }
}
