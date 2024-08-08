using UnityEngine;

// script tutorial: https://www.youtube.com/watch?v=wBol2xzxCOU

public class ParallexBackground : MonoBehaviour
{
    [SerializeField] private Vector2 parallexEffectMultiplier;
    [SerializeField] private bool infiniteHorizontal;
    [SerializeField] private bool infiniteVertical;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    private float textureUnitySizeX;
    private float textureUnitySizeY;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;

        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitySizeX = texture.width / sprite.pixelsPerUnit * transform.localScale.x;
        textureUnitySizeY = texture.height / sprite.pixelsPerUnit * transform.localScale.y;
    }

    private void LateUpdate()
    {
        MoveBG();
    }

    private void MoveBGWithCamera()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position -= new Vector3(deltaMovement.x * parallexEffectMultiplier.x, deltaMovement.y * parallexEffectMultiplier.y);

        lastCameraPosition = cameraTransform.position;

        if (infiniteHorizontal)
        {
            if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitySizeX)
            {
                float offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitySizeX;
                transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y);
            }
        }

        if (infiniteVertical)
        {
            if (Mathf.Abs(cameraTransform.position.y - transform.position.y) >= textureUnitySizeY)
            {
                float offsetPositionY = (cameraTransform.position.y - transform.position.y) % textureUnitySizeY;
                transform.position = new Vector3(cameraTransform.position.x, transform.position.y + offsetPositionY);
            }
        }
    }

    private void MoveBG(float xSpeed = 0.1f, float ySpeed = 0)
    {
        transform.position -= new Vector3(xSpeed * parallexEffectMultiplier.x, ySpeed * parallexEffectMultiplier.y);

        if (infiniteHorizontal)
        {
            if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitySizeX)
            {
                float offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitySizeX;
                transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y);
            }
        }

        if (infiniteVertical)
        {
            if (Mathf.Abs(cameraTransform.position.y - transform.position.y) >= textureUnitySizeY)
            {
                float offsetPositionY = (cameraTransform.position.y - transform.position.y) % textureUnitySizeY;
                transform.position = new Vector3(cameraTransform.position.x, transform.position.y + offsetPositionY);
            }
        }
    }
}
