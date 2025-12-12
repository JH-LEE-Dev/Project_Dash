using NUnit.Framework.Internal.Commands;
using System;
using UnityEngine;

public class MoveComponent : EntityComponent
{
    public event Action DashFinishedEvent;

    ParabolaComponent parabolaComponent;

    bool bDash = false;

    protected override void Awake()
    {
        base.Awake();

        parabolaComponent = new ParabolaComponent();

        if (parabolaComponent == null)
        {
            Debug.Log("parabolaComponent is null -> PMoveComponent::Awake");
            return;
        }

        parabolaComponent.DashFinishedEvent += DashFinished;
    }

    protected override void Update()
    {
        base.Update();

        if (bDash)
        {
            parabolaComponent.Update();
            ctx.SetUnitTransform(parabolaComponent.GetCurrentPos());
            ctx.SetShadowEffectPos(parabolaComponent.GetCurrentLinearPos());
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        parabolaComponent.DashFinishedEvent -= DashFinished;
    }

    public virtual void Dash(Vector2 start, Vector2 end, float height, float duration)
    {
        if (parabolaComponent == null)
        {
            Debug.Log("parabolaComponent is null -> PMoveComponent::Dash");
            return;
        }

        parabolaComponent.Reset(start, end, height, duration);
        bDash = true;
    }

    public virtual void DashFinished()
    {
        if (ctx == null)
        {
            Debug.Log("ctx is null -> PMoveComponent::DashFinished");
            return;
        }

        bDash = false;

        DashFinishedEvent?.Invoke();
    }

    public virtual void KnockBack(Vector2 attackPos,float power)
    {
        Vector2 knockBackDir = (Vector2)transform.position - attackPos;
        knockBackDir.Normalize();

        knockBackDir *= power;

        Vector2 landingPos = (Vector2)transform.position + knockBackDir;

        Dash(transform.position, landingPos, 1f, 0.2f);
    }
}
