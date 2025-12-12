using System.Drawing;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

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

    public void HandleLeftClickReleased(Vector2 screenPos)
    {
        if(factory == null || selectManager == null || dispatcher == null)
        {
            Debug.Log("Something is null -> CommandController::HandleLeftClickReleased");
            return;
        }

        selectManager.HandleLeftClickReleased(screenPos);

        Entity selectedUnit = selectManager.GetSelectedObject();

        if(selectedUnit && selectedUnit.IsCharging())
        {
            ICommand cmd = factory.CreateMoveCommand();
            dispatcher.Dispatch(selectedUnit, cmd);

            selectManager.ResetSelectedUnit();
        }
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

    public void ApplyAttackCommand(Entity entity)
    {
        if (factory == null || dispatcher == null)
        {
            Debug.Log("Something is null -> CommandController::ApplyAttackCommand");
            return;
        }

        ICommand cmd = factory.CreateAttackCommand();

        dispatcher.Dispatch(entity, cmd);
    }

    public void ApplyConflictKnockBackCommand(Entity entity)
    {
        if (factory == null || dispatcher == null)
        {
            Debug.Log("Something is null -> CommandController::ApplyAttackCommand");
            return;
        }

        ICommand cmd = factory.CreateConflictKnockBackCommand();

        dispatcher.Dispatch(entity, cmd);
    }
}
