using UnityEngine;

public class HeartController : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayLoseHeartAnimation()
    {
        animator.SetBool("GainHealth", false);
        animator.SetBool("LoseHealth", true);
    }

    public void PlayGainHealthAnimation()
    {
        animator.SetBool("LoseHealth", false);
        animator.SetBool("GainHealth", true);
    }
}
