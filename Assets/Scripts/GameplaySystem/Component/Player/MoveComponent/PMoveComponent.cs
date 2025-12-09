using Unity.VisualScripting;
using UnityEngine;

public class PMoveComponent : MoveComponent 
{
    ParabolaComponent parabolaComponent;

    protected override void Awake()
    {
        base.Awake();

        parabolaComponent = new ParabolaComponent();
    }

    protected override void Update()
    {
        base.Update();

        ctx.SetUnitTransform(parabolaComponent.GetCurrentPos());
    }
}
