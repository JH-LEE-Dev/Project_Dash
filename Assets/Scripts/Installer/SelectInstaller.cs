using UnityEngine;

public class SelectInstaller
{
    [Header("Input")]
    private InputReader inputReader;
    public SelectManager selectManager { get; private set; }
    private CameraController cameraController;

    public void Initialize(InputReader inputReader, CameraController cameraController)
    {
        this.inputReader = inputReader;
        this.cameraController = cameraController; 
        selectManager = new SelectManager();

        if(selectManager == null)
        {
            Debug.Log("selectManager is null -> SelectInstaller::Initialize");
            return;
        }

        selectManager.Initialize(inputReader,cameraController);
    }

    public void Release()
    {
        selectManager.Release();
    }
}
