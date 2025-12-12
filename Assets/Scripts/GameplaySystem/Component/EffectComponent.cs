using UnityEngine;

public class EffectComponent : EntityComponent
{

    [Header("Effect System")]
    protected Animator dashAnimator;
    protected Animator knockBackAnimator;
    [SerializeField] protected GameObject dashEffectObject;
    [SerializeField] protected GameObject knockBackEffectObject;
    [SerializeField] protected GameObject shadowObject;
    protected Vector2 shadowPos;
    protected Vector2 dashPlayPosition;
    protected Vector2 knockBackPlayPosition;


    protected override void Awake()
    {
        base.Awake();

        dashAnimator = dashEffectObject.gameObject.GetComponentInChildren<Animator>();
        knockBackAnimator = knockBackEffectObject.gameObject.GetComponentInChildren<Animator>();
        dashEffectObject.SetActive(false);
        knockBackEffectObject.SetActive(false);
        knockBackEffectObject.transform.localScale = new Vector3(0.5f, 0.5f, 1f);

        shadowPos = transform.position;
        shadowPos.y -= 0.4f;
        shadowObject.transform.position = shadowPos;
    }

    protected override void Update()
    {
        base.Update();

        dashEffectObject.transform.position = dashPlayPosition;
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

    public virtual void PlayDashEffect(Vector2 position)
    {
        dashEffectObject.SetActive(true);
        dashEffectObject.gameObject.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
        dashPlayPosition = position;
        dashAnimator.SetInteger("DashEffectState", (int)DashEffectState.Dash);
    }

    public virtual void PlayLandEffect(Vector2 position)
    {
        dashEffectObject.SetActive(true);
        dashEffectObject.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        dashPlayPosition = position;
        dashAnimator.SetInteger("DashEffectState", (int)DashEffectState.Land);
    }

    public virtual void PlayKnockBackEffect(Vector2 position)
    {
        dashAnimator.SetInteger("DashEffectState", (int)DashEffectState.Default);

        knockBackPlayPosition = position;
        knockBackEffectObject.SetActive(true);
        knockBackAnimator.SetBool("bKnockBack",true);
    }

    public virtual void HandleShadowPos()
    {
        shadowObject.transform.position = shadowPos;
    }
}
