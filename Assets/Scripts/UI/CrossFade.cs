using UnityEngine;
using UnityEngine.Events;

public class CrossFade : MonoBehaviour
{
    private Animator myAnimator;
    
    [SerializeField] private bool useFadeIn;

    [Space]
    [Header("On Animation Finished Events")]
    [SerializeField] private UnityEvent OnFadeInFinished;
    [SerializeField] private UnityEvent OnFadeOutFinished;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (useFadeIn)
        {
            StartFadeIn();
        }
    }

    public void StartFadeIn()
    {
        Time.timeScale = 0f;

        myAnimator.SetTrigger("FadeIn");
    }

    public void StartFadeOut()
    {
        myAnimator.SetTrigger("FadeOut");
    }

    protected void FadeInFinished()
    {
        OnFadeInFinished?.Invoke();
    }

    protected void FadeOutFinished()
    {
        OnFadeOutFinished?.Invoke();
    }
}
