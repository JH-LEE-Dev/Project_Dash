using UnityEngine;

public class ChargingState : GroundedState
{
    public ChargingState(Entity entity)
    {
        this.entity = entity;
    }

    public override void Enter()
    {
        base.Enter();

        entity.animator.SetInteger("UnitState", (int)UnitState.Charging);
    }

    public override void Exit()
    {
        base.Exit();

        entity.animator.SetInteger("UnitState", (int)UnitState.DashStart);
        entity.HideOutLine();
    }

    public override void Update()
    {
        base.Update();

        entity.HandleChargingState();
    }
}
