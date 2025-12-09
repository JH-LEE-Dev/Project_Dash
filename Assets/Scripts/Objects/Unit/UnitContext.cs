using Unity.VisualScripting;
using UnityEngine;

public class UnitContext
{
    public Unit unit;
    public MoveComponent moveComponent { get; private set; }
    public StateMachine fsm { get; private set; }
    public Animator animator { get; private set; }

    public void Initialize(Unit unit, StateMachine fsm, MoveComponent moveComponent,Animator animator)
    {
        this.unit = unit;
        this.fsm = fsm;
        this.moveComponent = moveComponent;
        this.animator = animator;
    }

    public void Update()
    {

    }

    public void SetUnitTransform(Vector2 pos)
    {
        unit.transform.position = pos;
    }
}
