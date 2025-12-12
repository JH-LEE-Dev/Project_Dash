using UnityEngine;

public class CombatComponent : EntityComponent
{
    [SerializeField] protected LayerMask enemyLayerMask;
    [SerializeField] protected AttackStrategy attackStrategy;

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

    public virtual void Attack()
    {
        attackStrategy.ApplyAttack(ctx.GetUnit(), transform.position, enemyLayerMask);
    }
}
