using UnityEngine;

public class CombatComponent : EntityComponent
{
    [SerializeField] protected LayerMask enemyLayerMask;

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

    public virtual void ApplyKnockBack(Collider2D collider)
    {
        Collider2D[] unitBuffer = new Collider2D[50];
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;
        filter.SetLayerMask(enemyLayerMask);

        int count = Physics2D.OverlapCollider(collider, filter, unitBuffer);

        for (int i = 0; i < count; i++)
        {
            Entity u = unitBuffer[i].GetComponent<Entity>();

            if (u != null)
            {
                ctx.ApplyKnockBackCommand(u, transform.position, 2f);
            }
        }
    }
}
