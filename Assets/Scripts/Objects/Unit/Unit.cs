using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

public class Unit : Entity
{
    [SerializeField] private float conflictKnockBackPower = 2f;

    [Header("Common Attribute")]
    protected PMoveComponent moveComponent;
    protected PEffectComponent effectComponent;
    protected PCombatComponent combatComponent;

    //Arrow 임시 코드
    protected float growSpeed = 1f;   // 1초당 얼마나 길어지는가
    protected float maxLength = 25f;   // 최대 길이

    protected float defaultLength = 12.3125f; // 초기 길이
    protected float defaultWidth = 6.375f;
    protected float curLength = 12.3125f;
    protected float curWidth = 6.375f;

    protected bool isHolding = false;

    protected float targetSpeed = 6f;
    protected float dashLength = 1f;

    protected override void Awake()
    {
        base.Awake();

        ctx = new UnitContext();
        moveComponent = GetComponent<PMoveComponent>();
        effectComponent = GetComponent<PEffectComponent>();
        combatComponent = GetComponent<PCombatComponent>();
        moveComponent.Initialize(ctx);
        effectComponent.Initialize(ctx);
        combatComponent.Initialize(ctx);

        fsm.AddState(new IdleState(this));
        fsm.AddState(new ChargingState(this));
        fsm.AddState(new LandingState(this));
        fsm.AddState(new KnockBackState(this));
        fsm.AddState(new WalkState(this));
        fsm.AddState(new SelectedState(this));
        fsm.AddState(new AttackState(this));
        fsm.AddState(new ConflictKnockBackState(this));
        fsm.AddState(new DashState(this));
        fsm.ChangeState<IdleState>();

        moveComponent.DashFinishedEvent += DashFinished;
    }

    public override void Initialize(ICommandSystem commandSystem)
    {
        base.Initialize(commandSystem);

        ctx.Initialize(this, fsm, moveComponent, effectComponent, animator, combatComponent, commandSystem);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        moveComponent.DashFinishedEvent -= DashFinished;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Update()
    {
        base.Update();

        fsm.Update();
    }

    protected override void PollInit()
    {
        base.PollInit();
    }

    public void ShowArrow()
    {
        if (arrowObject == null || targetPoint == null)
        {
            Debug.Log("Something is null -> Unit::ShowArrow");
            return;
        }

        float radius = 1f;

        // 3) 화살표 위치 = 캐릭터 + 방향 * 반지름
        arrowObject.gameObject.transform.position = (Vector2)transform.position + (-dashVector) * radius;

        // 4) 화살표 회전 = 방향을 바라보게
        float angle = Mathf.Atan2(dashVector.y, dashVector.x) * Mathf.Rad2Deg;
        arrowObject.gameObject.transform.rotation = Quaternion.Euler(0, 0, angle + 90);

        // 마우스 좌클릭 눌렀을 때 시작
        if (bCharging)
        {
            isHolding = true;
        }

        // 마우스 좌클릭에서 손 뗐을 때 중지
        if (bCharging == false)
        {
            isHolding = false;
        }

        // 화살표 길이 증가
        if (isHolding)
        {
            curLength += growSpeed * Time.deltaTime;

            if (curLength > maxLength)
                curLength = maxLength;

            arrowObject.GetComponent<SpriteRenderer>().size = new Vector2(curWidth, curLength);

            Vector2 mousePos = CalcMousePos();
            Vector2 delta = mousePos - (Vector2)transform.position;

            //dashLength += targetSpeed * Time.deltaTime;

            if (dashLength > 10)
                dashLength = 10;

            Vector2 direction = -dashVector.normalized;
            //Vector2 displacement = direction * dashLength;
            Vector2 displacement = direction * delta.magnitude;

            targetPoint.transform.position = (Vector2)transform.position + displacement;
        }
    }

    public override void Dash()
    {
        Vector2 startPoint = transform.position;
        Vector2 endPoint = targetPoint.transform.position;
        moveComponent.Dash(startPoint, endPoint, 2f, 0.5f);

        effectComponent.PlayDashEffect(transform.position);
        Sound.Play("Dash", transform.position, 1f, false);

        fsm.ChangeState<DashState>();
    }

    public override void DashFinished()
    {
        base.DashFinished();

        effectComponent.PlayLandEffect(transform.position);
        Sound.Play("Land", transform.position, 1f, false);
        ResetDashVariable();

        if (fsm.IsState<ConflictKnockBackState>() || fsm.IsState<DashState>())
        {
            fsm.ChangeState<IdleState>();
            return;
        }

        fsm.ChangeState<AttackState>();
    }

    public override void ApplyKnockBack(Vector2 attackPos, float power)
    {
        moveComponent.KnockBack(attackPos, power);
        CalcKnockBackDirection(attackPos);
        effectComponent.PlayKnockBackEffect(transform.position);
        Sound.Play("KnockBack", transform.position, 1f, false);

        fsm.ChangeState<KnockBackState>();
    }

    public override void SetCharging(bool bCharging)
    {
        base.SetCharging(bCharging);

        if (bCharging == true)
        {
            arrowObject.gameObject.SetActive(true);
            targetPoint.gameObject.SetActive(true);

            fsm.ChangeState<ChargingState>();
        }
    }

    public void ResetDashVariable()
    {
        curLength = defaultLength;
        curWidth = defaultWidth;

        dashLength = 1f;
    }

    public virtual void ConflictKnockBack()
    {
        Vector2 conflictPower = dashVector * conflictKnockBackPower;
        Vector2 startPoint = transform.position;
        Vector2 endPoint = (Vector2)transform.position + conflictPower;

        moveComponent.Dash(startPoint, endPoint, 2f, 0.5f);

        effectComponent.PlayDashEffect(transform.position);
        Sound.Play("Dash", transform.position, 1f, false);
    }

    public override void HandleChargingState()
    {
        CalcDashDirection();
        ShowArrow();
    }

    public override void Attack()
    {
        combatComponent.Attack();
    }
}
