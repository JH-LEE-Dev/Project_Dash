using UnityEngine;

public class AttackCommand : ICommand
{
    public void Execute(Entity entity)
    {
        Unit unitObject = entity.GetComponent<Unit>();

        if (unitObject == null)
            return;

        unitObject.fsm.ChangeState<AttackState>();
    }
}
