using UnityEngine;
using UnityEngine.UI;

public class SelectedState : GroundedState
{
    public SelectedState(Entity entity)
    {
        this.entity = entity;
    }

    public override void Enter()
    {
        base.Enter();

        entity.animator.SetInteger("UnitState", (int)UnitState.CombatStart);
        entity.ShowOutLine();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        entity.UpdateOutlineSprite();
    }
}
