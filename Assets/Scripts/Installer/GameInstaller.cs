using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    private CommandInstaller commandInstaller;
    private SelectInstaller selectInstaller;
    private InputController inputController;
    private UnitSpawner unitSpawner;
    private DIServiceLocator diServiceLocator;
    private UIManager uiManager;

    private void Awake()
    {
        commandInstaller = new CommandInstaller();
        selectInstaller = new SelectInstaller();
        inputController = new InputController();
        unitSpawner = GetComponent<UnitSpawner>();
        uiManager = GetComponent<UIManager>();
        diServiceLocator = GetComponent<DIServiceLocator>();

        if(commandInstaller == null || selectInstaller == null || inputController == null ||  unitSpawner == null ||
            diServiceLocator == null || uiManager == null)
        {
            Debug.Log("Something is null -> GameInstaller::Awake");
            return;
        }

        inputController.Initialize(diServiceLocator);
        selectInstaller.Initialize(inputController.inputReader);
        commandInstaller.Initialize(inputController.inputReader,selectInstaller.selectManager);
        unitSpawner.Initiallize(inputController.inputReader, commandInstaller.GetCommandController());

        diServiceLocator.Register<UnitSpawner>(unitSpawner);

        //Sound.PlayBGM("BGM",1f);
    }
        
    private void OnDestroy()
    {
        commandInstaller.Release();
        selectInstaller.Release();
    }
}
