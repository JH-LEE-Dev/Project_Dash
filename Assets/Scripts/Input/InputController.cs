using UnityEngine;

public class InputController
{
    public InputReader inputReader { get; private set; }

    public void Initialize()
    {
        inputReader = new CombatInputReader();
        inputReader.Initialize();
    }
}
