using UnityEngine;

public class CommandFactory 
{
    //객체 풀 사용해야 함.
    public ICommand CreateMoveCommand()
    {
        ICommand cmd = new MoveCommand();

        if(cmd == null)
        {
            Debug.Log("Command is null -> CommandFactory::CreateMoveCommand");
        }

        return cmd;
    }

    public ICommand CreateSelectedCommand()
    {
        return null;
    }
}

