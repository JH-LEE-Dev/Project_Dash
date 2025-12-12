using UnityEngine;

public class DashState : InAirState
{
    public DashState(Entity entity)
    {
        this.entity = entity;
    }

    public override void Enter()
    {
        base.Enter();

        entity.SetCharging(false);
        entity.Dash();

        entity.animator.SetInteger("UnitState", (int)UnitState.DashStart);
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
