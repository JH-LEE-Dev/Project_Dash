using NUnit.Framework.Internal.Commands;
using System;
using UnityEngine;

public class MoveComponent : EntityComponent
{
    public event Action JumpFinishEvent;

    ParabolaComponent parabolaComponent;

    bool bJump = false;

    protected override void Awake()
    {
        base.Awake();

        parabolaComponent = new ParabolaComponent();

        if (parabolaComponent == null)
        {
            Debug.Log("parabolaComponent is null -> PMoveComponent::Awake");
            return;
        }

        parabolaComponent.JumpFinishEvent += JumpFinished;
    }

    protected override void Update()
    {
        base.Update();

        if (bJump)
        {
            parabolaComponent.Update();
            ctx.SetUnitTransform(parabolaComponent.GetCurrentPos());
            ctx.SetShadowEffectPos(parabolaComponent.GetCurrentLinearPos());
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        parabolaComponent.JumpFinishEvent -= JumpFinished;
    }

    public virtual void ApplyKnockBack()
    {
        ctx.ApplyKnockBack();
    }

    public virtual void Jump(Vector2 start, Vector2 end, float height, float duration)
    {
        if (parabolaComponent == null)
        {
            Debug.Log("parabolaComponent is null -> PMoveComponent::Jump");
            return;
        }

        parabolaComponent.Reset(start, end, height, duration);
        bJump = true;
    }

    public virtual void JumpFinished()
    {
        if (ctx == null)
        {
            Debug.Log("ctx is null -> PMoveComponent::JumpFinished");
            return;
        }

        bJump = false;

        ctx.fsm.ChangeState<IdleState>();
        ctx.animator.SetBool("bJump", false);

        JumpFinishEvent?.Invoke();

        ApplyKnockBack();
    }

    public virtual void KnockBack(Vector2 attackPos,float power)
    {
        Vector2 knockBackDir = (Vector2)transform.position - attackPos;
        knockBackDir.Normalize();

        knockBackDir *= power;

        Vector2 landingPos = (Vector2)transform.position + knockBackDir;

        Jump(transform.position, landingPos, 1f, 0.2f);
    }
}
