using UnityEngine;

public class PCombatComponent : CombatComponent
{
    public override void Initialize(UnitContext _ctx)
    {
        ctx = _ctx;
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    protected override void Update()
    {
        base.Update();
    }
}
