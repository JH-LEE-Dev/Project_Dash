using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [Header("Unit Prefabs")]
    bool bUnitType = false;
    private GameObject unitPrefab;
    [SerializeField] private GameObject playerUnitPrefab;
    [SerializeField] private GameObject enemyUnitPrefab;

    [Header("Common Attribute")]
    private InputReader inputReader;

    public void Initiallize(InputReader _inputReader)
    {
        inputReader = _inputReader;

        if(inputReader==null)
        {
            Debug.Log("inputReader is null -> UnitSpawner::Initialize");
            return;
        }

        inputReader.SwitchPrefabButtonPressedEvent += SwitchPrefab;
        inputReader.SpawnButtonPressedEvent += SpawnUnit;
    }

    public void OnDestroy()
    {
        inputReader.SwitchPrefabButtonPressedEvent -= SwitchPrefab;
        inputReader.SpawnButtonPressedEvent -= SpawnUnit;   
    }

    public void SpawnUnit(Vector2 screenPos)
    {
        Vector3 sp = screenPos;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(sp);
        worldPos.z = 0;

        GameObject Entity = Instantiate(unitPrefab, worldPos, Quaternion.identity);
    }

    public void SwitchPrefab()
    {
        bUnitType = !bUnitType;

        if(bUnitType)
        {
            unitPrefab = playerUnitPrefab;
        }
        else
        {
            unitPrefab = enemyUnitPrefab;
        }
    }
}
    