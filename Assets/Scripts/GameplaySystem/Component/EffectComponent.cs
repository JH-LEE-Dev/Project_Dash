using UnityEngine;

public class EffectComponent : EntityComponent
{

    [Header("Effect System")]
    protected Animator jumpAnimator;
    protected Animator knockBackAnimator;
    [SerializeField] protected GameObject jumpEffectObject;
    [SerializeField] protected GameObject knockBackEffectObject;
    [SerializeField] protected GameObject shadowObject;
    protected Vector2 shadowPos;
    protected Vector2 jumpPlayPosition;
    protected Vector2 knockBackPlayPosition;


    protected override void Awake()
    {
        base.Awake();

        jumpAnimator = jumpEffectObject.gameObject.GetComponentInChildren<Animator>();
        knockBackAnimator = knockBackEffectObject.gameObject.GetComponentInChildren<Animator>();
        jumpEffectObject.SetActive(false);
        knockBackEffectObject.SetActive(false);
        knockBackEffectObject.transform.localScale = new Vector3(0.5f, 0.5f, 1f);

        shadowPos = transform.position;
        shadowPos.y -= 0.4f;
        shadowObject.transform.position = shadowPos;
    }

    protected override void Update()
    {
        base.Update();

        jumpEffectObject.transform.position = jumpPlayPosition;
        knockBackEffectObject.transform.position = knockBackPlayPosition;
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
        jumpEffectObject.SetActive(true);
        jumpEffectObject.gameObject.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
        jumpPlayPosition = position;
        jumpAnimator.SetInteger("JumpState", (int)JumpState.Jump);
    }

    public virtual void PlayLandEffect(Vector2 position)
    {
        jumpEffectObject.SetActive(true);
        jumpEffectObject.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        jumpPlayPosition = position;
        jumpAnimator.SetInteger("JumpState", (int)JumpState.Land);
    }

    public virtual void PlayKnockBackEffect(Vector2 position)
    {
        jumpAnimator.SetInteger("JumpState", (int)JumpState.Default);

        knockBackPlayPosition = position;
        knockBackEffectObject.SetActive(true);
        knockBackAnimator.SetBool("bKnockBack",true);
    }

    public virtual void HandleShadowPos()
    {
        shadowObject.transform.position = shadowPos;
    }
}
