using UnityEngine;

public class IdleState : GroundedState
{
    public IdleState(Entity entity)
    {
        this.entity = entity;
    }

    public override void Enter()
    {
        base.Enter();

        entity.animator?.SetInteger("UnitState", (int)UnitState.Idle);
        entity.HideOutLine();
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
