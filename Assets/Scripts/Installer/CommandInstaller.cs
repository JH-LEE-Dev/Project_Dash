using UnityEngine;

public class CommandInstaller
{
    private InputReader inputReader;
    private CommandController commandController;
    private CommandFactory commandFactory;
    private CommandDispatcher commandDispatcher;

    [Header("Selected Object")]
    [SerializeField] private MonoBehaviour targetProviderBehaviour;
    private SelectManager selectManager;

    public void Initialize(InputReader _inputReader,SelectManager _selectManager)
    {
        this.inputReader = _inputReader;
        selectManager = _selectManager;
        commandController = new CommandController();
        commandDispatcher = new CommandDispatcher();
        commandFactory = new CommandFactory();

        inputReader.LeftClickEvent += commandController.HandleLeftClick;
        inputReader.LeftClickReleased += commandController.HandleLeftClickReleased;
        commandController.Initialize(commandFactory, commandDispatcher, selectManager);
    }

    public void Release()
    {
        inputReader.LeftClickEvent -= commandController.HandleLeftClick;
        inputReader.LeftClickReleased -= commandController.HandleLeftClickReleased;
    }
}
