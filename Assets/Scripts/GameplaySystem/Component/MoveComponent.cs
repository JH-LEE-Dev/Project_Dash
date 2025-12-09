using UnityEngine;

public class MoveComponent : EntityComponent
{
    protected Dir8 jumpDir;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
    }

    public virtual void ApplyKnockBack()
    {

    }

    public void SetJumpDir(Dir8 _jumpDir)
    {
        jumpDir = _jumpDir;
    }
}
