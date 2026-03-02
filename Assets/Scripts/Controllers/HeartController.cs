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
        animator.SetBool("LoseHeart", true);
    }

    public void EnableHeart()
    {
        gameObject.SetActive(true);
    }
}
