using UnityEngine;

public class InputController
{
    public InputReader inputReader { get; private set; }

    public void Initialize(DIServiceLocator diServiceLocator)
    {
        inputReader = new CombatInputReader();

        if (inputReader == null)
        {
            Debug.Log("inputReader is null -> InputController::Initialize");
            return;
        }

        inputReader.Initialize(diServiceLocator);
    }
}
