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
        selectManager.Initialize(inputReader);
    }
}
