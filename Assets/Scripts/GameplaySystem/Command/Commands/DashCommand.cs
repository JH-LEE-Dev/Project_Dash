using UnityEngine;

public class DashCommand : ICommand
{
    public void Execute(Entity entity)
    {
        Unit unitObject = entity.GetComponent<Unit>();

        if (unitObject == null)
            return;

        unitObject.fsm.ChangeState<DashState>();
    }
}
