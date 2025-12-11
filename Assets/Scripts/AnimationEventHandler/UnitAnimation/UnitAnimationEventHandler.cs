using UnityEngine;

public class UnitAnimationEventHandler : MonoBehaviour
{
    private Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void CombatStartFinished()
    {
        animator.SetInteger("UnitState", (int)UnitState.CombatIdle);
    }

    public void DashStartFinished()
    {
        animator.SetInteger("UnitState", (int)UnitState.Dash);
    }

    public void AttackFinished()
    {
        animator.SetInteger("UnitState", (int)UnitState.Idle);
    }
}
