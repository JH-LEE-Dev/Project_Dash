using UnityEngine;

public class KnockBackState : InAirState
{
    public KnockBackState(Entity entity)
    {
        this.entity = entity;
    }

    public override void Enter()
    {
        base.Enter();

        entity.animator.SetInteger("UnitState", (int)UnitState.Landing);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}
