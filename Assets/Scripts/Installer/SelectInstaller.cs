using UnityEngine;

public class SelectInstaller
{
    [Header("Input")]
    private InputReader inputReader;
    public SelectManager selectManager { get; private set; }

    public void Initialize(InputReader inputReader)
    {
        this.inputReader = inputReader; 
        selectManager = new SelectManager();

        if(selectManager == null)
        {
            Debug.Log("selectManager is null -> SelectInstaller::Initialize");
            return;
        }

        selectManager.Initialize(inputReader);
    }

    public void Release()
    {
        selectManager.Release();
    }
}
