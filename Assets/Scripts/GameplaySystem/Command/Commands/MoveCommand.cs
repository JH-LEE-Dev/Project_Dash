using UnityEngine;

public class MoveCommand : ICommand
{
    public void Execute(Entity entity)
    {
        Unit unitObject = entity.GetComponent<Unit>();

        if (unitObject == null)
            return;

        unitObject.Jump();
    }
}
