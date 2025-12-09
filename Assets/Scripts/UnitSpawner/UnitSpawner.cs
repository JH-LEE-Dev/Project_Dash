using UnityEngine;

public class UnitSpawner
{
    [Header("Unit Prefabs")]
    [SerializeField] private GameObject unitPrefab;

    [Header("Common Attribute")]
    private InputReader inputReader;

    public void Initiallize(InputReader _inputReader)
    {
        inputReader = _inputReader;
    }

    public void SpawnUnit()
    {

    }
}
