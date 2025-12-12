using UnityEngine;

public class Swordman : Unit
{
   public override void Dash()
    {
        Vector2 startPoint = transform.position;
        Vector2 endPoint = targetPoint.transform.position;
        moveComponent.Dash(startPoint, endPoint,0.5f, 0.5f);

        fsm.ChangeState<WalkState>();

        effectComponent.PlayDashEffect(transform.position);
        Sound.Play("Dash", transform.position, 1f, false);

        //test code
        if (bTest)
            animator.SetInteger("UnitState", (int)UnitState.DashStart);
    }
}
