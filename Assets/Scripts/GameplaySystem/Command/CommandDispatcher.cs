using UnityEngine;

public class CommandDispatcher
{
    public void Dispatch(Entity target,ICommand command)
    {
        if (target == null || command == null)
            return;

        target.EnqueueCommand(command);
    }
}

