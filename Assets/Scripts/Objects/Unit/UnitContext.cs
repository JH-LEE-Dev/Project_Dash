using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.EventSystems.EventTrigger;

//Component간의 결합을 줄이기 위한 Mediator 역할 + UnitContext 내부의 메서드에서 사용하는 객체들을 최대한 추상화하여
//의존성 역전도 실현
public class UnitContext
{
    private Unit unit;
    private MoveComponent moveComponent;
    private EffectComponent effectComponent;
    private CombatComponent combatComponent;
    public StateMachine fsm { get; private set; }
    public Animator animator { get; private set; }

    private ICommandSystem commandSystem;

    public void Initialize(Unit unit, StateMachine fsm, MoveComponent moveComponent,EffectComponent effectComponent,
        Animator animator,CombatComponent combatComponent,ICommandSystem commandSystem)
    {
        this.unit = unit;
        this.fsm = fsm;
        this.moveComponent = moveComponent;
        this.effectComponent = effectComponent;
        this.animator = animator;
        this.combatComponent = combatComponent;
        this.commandSystem = commandSystem;
    }

    public void Update()
    {

    }

    public Unit GetUnit()
    {
        return unit;
    }

    public void SetUnitTransform(Vector2 pos)
    {
        if (unit == null)
        {
            Debug.Log("Unit is null -> UnitContext::SetUnitTransform");
            return;
        }

        unit.transform.position = pos;
    }

    public Dir8 GetEntityFacingDir()
    {
        if(unit == null)
        {
            Debug.Log("Unit is null -> UnitContext::GetEntityFacingDir");
            return 0;
        }

        return unit.GetFacingDir();
    }
    
    public void SetShadowEffectPos(Vector2 pos)
    {
        effectComponent.SetShadowPos(pos);
    }
}
