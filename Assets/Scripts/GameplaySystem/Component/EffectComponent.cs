using UnityEngine;

public class EffectComponent : EntityComponent
{

    [Header("Effect System")]
    protected Animator animator;
    [SerializeField] protected GameObject effectObject;
    [SerializeField] protected GameObject shadowObject;
    protected Vector2 shadowPos;
    protected Vector2 playPosition;

    protected override void Awake()
    {
        base.Awake();

        animator = effectObject.gameObject.GetComponentInChildren<Animator>();
        effectObject.SetActive(false);

        shadowPos = transform.position;
        shadowPos.y -= 0.4f;
        shadowObject.transform.position = shadowPos;
    }

    protected override void Update()
    {
        base.Update();

        effectObject.transform.position = playPosition;
        HandleShadowPos();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    public void SetShadowPos(Vector2 Pos)
    {
        shadowPos = Pos;
        shadowPos.y -= 0.4f;
    }

    public virtual void PlayJumpEffect(Vector2 position)
    {
        effectObject.gameObject.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
        effectObject.SetActive(true);
        playPosition = position;
        animator.SetInteger("JumpState", (int)JumpState.Jump);
    }

    public virtual void PlayLandEffect(Vector2 position)
    {
        effectObject.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        effectObject.SetActive(true);
        playPosition = position;
        animator.SetInteger("JumpState", (int)JumpState.Land);
    }

    public virtual void HandleShadowPos()
    {
        shadowObject.transform.position = shadowPos;
    }
}
