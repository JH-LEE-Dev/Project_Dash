using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    private CommandInstaller commandInstaller;
    private SelectInstaller selectInstaller;
    private InputController inputController;
    private UnitSpawner unitSpawner;

    private void Awake()
    {
        commandInstaller = new CommandInstaller();
        selectInstaller = new SelectInstaller();
        inputController = new InputController();
        unitSpawner = new UnitSpawner();

        inputController.Initialize();
        unitSpawner.Initiallize(inputController.inputReader);
        selectInstaller.Initialize(inputController.inputReader);
        commandInstaller.Initialize(inputController.inputReader,selectInstaller.selectManager);
        selectInstaller.Initialize(inputController.inputReader);
    }
        
    private void OnDestroy()
    {
        commandInstaller.Release();
    }
}
