using UnityEngine;

public class CommandFactory 
{
    //객체 풀 사용해야 함.
    public ICommand CreateMoveCommand()
    {
        ICommand cmd = new MoveCommand();

        return null;
    }

    public ICommand CreateSelectedCommand()
    {
        return null;
    }
}

