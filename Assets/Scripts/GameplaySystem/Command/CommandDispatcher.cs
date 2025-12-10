using UnityEngine;

public class CommandDispatcher
{
    public void Dispatch(Entity target, ICommand command)
    {
        if (target == null || command == null)
        {
            Debug.Log("Something is null -> CommandDispatcher::Dispatch");
            return;
        }

        target.EnqueueCommand(command);
    }
}

