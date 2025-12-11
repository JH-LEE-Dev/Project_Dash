using System.Drawing;
using UnityEngine;

public class CommandController : ICommandSystem
{
    [SerializeField] private CommandFactory factory;
    [SerializeField] private CommandDispatcher dispatcher;
    [SerializeField] private SelectManager selectManager;

    public void Initialize(CommandFactory factory, CommandDispatcher dispatcher, SelectManager selectManager)
    {
        this.factory = factory;
        this.dispatcher = dispatcher;
        this.selectManager = selectManager;
    }

    public void HandleLeftClick(Vector2 point)
    {
        if (selectManager == null)
        {
            Debug.Log("select Manager is null! -> CommandController::HandleLeftClick");
            return;
        }

        selectManager.HandleLeftClick(point);
    }

    public void HandleLeftClickReleased()
    {
        if(factory == null || selectManager == null || dispatcher == null)
        {
            Debug.Log("Something is null -> CommandController::HandleLeftClickReleased");
            return;
        }

        ICommand cmd = factory.CreateMoveCommand();

        Entity selectedUnit = selectManager.GetSelectedObject();

        dispatcher.Dispatch(selectedUnit, cmd);
    }

    public void ApplyKnockBackCommand(Entity entity, Vector2 attackPos, float power)
    {
        if (factory == null || dispatcher == null)
        {
            Debug.Log("Something is null -> CommandController::ApplyKnockBackCommand");
            return;
        }

        ICommand cmd = factory.CreateKnockBackCommand(attackPos, power);

        dispatcher.Dispatch(entity, cmd);
    }
}
