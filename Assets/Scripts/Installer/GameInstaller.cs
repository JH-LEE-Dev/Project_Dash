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
        unitSpawner = GetComponent<UnitSpawner>();

        if(commandInstaller == null || selectInstaller == null || inputController == null ||  unitSpawner == null)
        {
            Debug.Log("Something is null -> GameInstaller::Awake");
            return;
        }

        inputController.Initialize();
        unitSpawner.Initiallize(inputController.inputReader);
        selectInstaller.Initialize(inputController.inputReader);
        commandInstaller.Initialize(inputController.inputReader,selectInstaller.selectManager);
    }
        
    private void OnDestroy()
    {
        commandInstaller.Release();
        selectInstaller.Release();
    }
}
