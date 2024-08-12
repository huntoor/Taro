using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    private Animator myAnimator;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Player.getHit += PlayHitAnimation;
    }

    private void OnDestroy()
    {
        Player.getHit -= PlayHitAnimation;
    }

    private void PlayHitAnimation()
    {
        float cameraShakeIntenisty = 1f;
        float cameraShakeTimer = 0.1f;
        CameraShake.Instance.ShakeCamera(cameraShakeIntenisty, cameraShakeTimer);

        myAnimator.SetTrigger("OnHit");
    }
}
