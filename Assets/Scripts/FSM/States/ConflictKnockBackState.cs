using UnityEngine;

public class ConflictKnockBackState : InAirState
{
    public ConflictKnockBackState(Entity entity)
    {
        this.entity = entity;
    }

    public override void Enter()
    {
        base.Enter();

        Unit unit = entity as Unit;

        if(unit != null )
        {
            unit.ConflictKnockBack();
        }
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
