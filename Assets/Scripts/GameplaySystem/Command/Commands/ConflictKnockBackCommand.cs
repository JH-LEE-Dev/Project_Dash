using UnityEngine;

public class ConflictKnockBackCommand : ICommand
{
    public void Execute(Entity entity)
    {
        Unit unitObject = entity.GetComponent<Unit>();

        if (unitObject == null)
            return;

        unitObject.fsm.ChangeState<ConflictKnockBackState>();
    }
}
