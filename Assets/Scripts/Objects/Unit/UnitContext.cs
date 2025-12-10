using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

//Component간의 결합을 줄이기 위한 Mediator 역할 + UnitContext 내부의 메서드에서 사용하는 객체들을 최대한 추상화하여
//의존성 역전도 실현
public class UnitContext
{
    public Unit unit;
    public MoveComponent moveComponent { get; private set; }
    public EffectComponent effectComponent { get; private set; }
    public CombatComponent combatComponent { get; private set; }
    public StateMachine fsm { get; private set; }
    public Animator animator { get; private set; }

    public void Initialize(Unit unit, StateMachine fsm, MoveComponent moveComponent,EffectComponent effectComponent,
        Animator animator,CombatComponent combatComponent)
    {
        this.unit = unit;
        this.fsm = fsm;
        this.moveComponent = moveComponent;
        this.effectComponent = effectComponent;
        this.animator = animator;
        this.combatComponent = combatComponent;
    }

    public void Update()
    {

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

    public void ApplyKnockBack()
    {
        combatComponent.ApplyKnockBack(unit.GetCollider());
    }
}
