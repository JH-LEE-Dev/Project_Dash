using System.Drawing;
using UnityEngine;

public class CommandController
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
        selectManager.HandleLeftClick(point);

    }

    public void HandleLeftClickReleased()
    {
        Debug.Log("LeftClick Released");
        ICommand cmd = factory.CreateMoveCommand();

        Entity selectedUnit = selectManager.GetSelectedObject();
        dispatcher.Dispatch(selectedUnit, cmd);
    }
}
