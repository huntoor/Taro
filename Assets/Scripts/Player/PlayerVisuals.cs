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
        PlayerPowerUps.isInShield += PlayShieldAnimation;
    }

    private void OnDestroy()
    {
        Player.getHit -= PlayHitAnimation;
        PlayerPowerUps.isInShield -= PlayShieldAnimation;
    }

    private void PlayHitAnimation()
    {
        myAnimator.SetTrigger("OnHit");
    }

    private void PlayShieldAnimation(bool isInShield)
    {
        if (isInShield)
        {
            myAnimator.SetTrigger("IsInShield");
        }
        else
        {
            myAnimator.SetTrigger("IsOutShield");
        }
    }
}
