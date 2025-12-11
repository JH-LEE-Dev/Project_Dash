using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [Header("Unit Prefabs")]
    bool bUnitType = true;
    private GameObject unitPrefab;
    [SerializeField] private GameObject playerUnitPrefab;
    [SerializeField] private GameObject enemyUnitPrefab;
    private ICommandSystem commandSystem;

    [Header("Common Attribute")]
    private InputReader inputReader;

    public void Initiallize(InputReader _inputReader,ICommandSystem commandSystem)
    {
        inputReader = _inputReader;

        if(inputReader==null)
        {
            Debug.Log("inputReader is null -> UnitSpawner::Initialize");
            return;
        }

        inputReader.SwitchPrefabButtonPressedEvent += SwitchPrefab;
        inputReader.SpawnButtonPressedEvent += SpawnUnit;
        
        this.commandSystem = commandSystem;

        unitPrefab = playerUnitPrefab;
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

        GameObject obj = Instantiate(unitPrefab, worldPos, Quaternion.identity);

        Entity entity = obj.GetComponent<Entity>();

        if(entity != null)
        {

            entity.Initialize(commandSystem);
        }
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
    