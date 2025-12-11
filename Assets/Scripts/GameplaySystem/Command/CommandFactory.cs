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

    public ICommand CreateKnockBackCommand(Vector2 attackPos,float power)
    {
        KnockBackCommand command = new KnockBackCommand();
        command.Initialize(attackPos, power);

        return command;
    }
}

