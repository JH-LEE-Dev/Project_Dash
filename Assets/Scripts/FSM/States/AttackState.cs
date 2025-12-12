using UnityEngine;

public class AttackState : GroundedState
{
    public AttackState(Entity entity)
    {
        this.entity = entity;
    }

    public override void Enter()
    {
        base.Enter();

        entity.animator.SetInteger("UnitState", (int)UnitState.Attack);
        entity.Attack();
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
