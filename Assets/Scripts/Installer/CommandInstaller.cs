using UnityEngine;

public class CommandInstaller
{
    private InputReader inputReader;
    private CommandController commandController;
    private CommandFactory commandFactory;
    private CommandDispatcher commandDispatcher;

    [Header("Selected Object")]
    private SelectManager selectManager;

    public void Initialize(InputReader _inputReader,SelectManager _selectManager)
    {
        this.inputReader = _inputReader;
        selectManager = _selectManager;
        commandController = new CommandController();
        commandDispatcher = new CommandDispatcher();
        commandFactory = new CommandFactory();

        if(commandController == null || commandDispatcher == null || commandFactory == null)
        {
            Debug.Log("Something is null -> CommandInstaller::Initialize");
            return;
        }

        inputReader.LeftClickEvent += commandController.HandleLeftClick;
        inputReader.LeftClickReleasedEvent += commandController.HandleLeftClickReleased;
        commandController.Initialize(commandFactory, commandDispatcher, selectManager);
    }

    public void Release()
    {
        inputReader.LeftClickEvent -= commandController.HandleLeftClick;
        inputReader.LeftClickReleasedEvent -= commandController.HandleLeftClickReleased;
    }

    public ICommandSystem GetCommandController()
    {
        return commandController;
    }
}
