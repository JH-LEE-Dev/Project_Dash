using UnityEngine;

//For KnockBack Effect
public class KnockBackAnimationEventHandler : MonoBehaviour
{
    private Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();    
    }

    public void KnockBackFinished()
    {
        transform.parent.gameObject.SetActive(false);
        animator.SetBool("bKnockBack", false);
    }
}
