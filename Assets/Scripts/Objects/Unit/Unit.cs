using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Unit : Entity
{
    [Header("Common Attribute")]
    private UnitContext ctx;
    private StateMachine fsm;
    private PMoveComponent moveComponent;
    private PEffectComponent effectComponent;
    private PCombatComponent combatComponent;

    //Arrow 임시 코드
    private float growSpeed = 1f;   // 1초당 얼마나 길어지는가
    private float maxLength = 25f;   // 최대 길이

    private float defaultLength = 12.3125f; // 초기 길이
    private float defaultWidth = 6.375f;
    private float curLength = 12.3125f;
    private float curWidth = 6.375f;

    private bool isHolding = false;

    private float targetSpeed = 6f;
    private float jumpLength = 1f;

    protected override void Awake()
    {
        base.Awake();

        ctx = new UnitContext();
        fsm = new StateMachine();
        moveComponent = GetComponent<PMoveComponent>();
        effectComponent = GetComponent<PEffectComponent>();
        combatComponent = GetComponent<PCombatComponent>();
        moveComponent.Initialize(ctx);
        effectComponent.Initialize(ctx);
        combatComponent.Initialize(ctx);
        animator = GetComponentInChildren<Animator>();
       
        fsm.AddState(new IdleState(ctx));
        fsm.AddState(new WalkState(ctx));
        fsm.ChangeState<IdleState>();

        moveComponent.JumpFinishEvent += JumpFinished;
    }

    public override void Initialize(ICommandSystem commandSystem)
    {
        base.Initialize(commandSystem);

        ctx.Initialize(this, fsm, moveComponent, effectComponent, animator, combatComponent, commandSystem);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        moveComponent.JumpFinishEvent -= JumpFinished;
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

        if (bSelected)
            ShowArrow();
        else
        {
            curLength = defaultLength;
            curWidth = defaultWidth;

            jumpLength = 1f;
        }
    }

    protected override void PollInit()
    {
        base.PollInit();
    }

    private void ShowArrow()
    {
        if(arrowObject == null || targetPoint == null)
        {
            Debug.Log("Something is null -> Unit::ShowArrow");
            return;
        }

        arrowObject.gameObject.SetActive(true);
        targetPoint.gameObject.SetActive(true);
        float radius = 1f;

        // 3) 화살표 위치 = 캐릭터 + 방향 * 반지름
        arrowObject.gameObject.transform.position = (Vector2)transform.position + (-jumpVector) * radius;

        // 4) 화살표 회전 = 방향을 바라보게
        float angle = Mathf.Atan2(jumpVector.y, jumpVector.x) * Mathf.Rad2Deg;
        arrowObject.gameObject.transform.rotation = Quaternion.Euler(0, 0, angle + 90);

        // 마우스 좌클릭 눌렀을 때 시작
        if (bSelected)
        {
            isHolding = true;
        }

        // 마우스 좌클릭에서 손 뗐을 때 중지
        if (bSelected == false)
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

            //jumpLength += targetSpeed * Time.deltaTime;

            if (jumpLength > 10)
                jumpLength = 10;

            Vector2 direction = -jumpVector.normalized;
            //Vector2 displacement = direction * jumpLength;
            Vector2 displacement = direction * delta.magnitude;

            targetPoint.transform.position = (Vector2)transform.position + displacement;
        }
    }

    public override void Jump()
    {
        Vector2 startPoint = transform.position;
        Vector2 endPoint = targetPoint.transform.position;
        moveComponent.Jump(startPoint, endPoint, 2f, 0.5f);

        animator.SetBool("bJump", true);

        fsm.ChangeState<WalkState>();

        effectComponent.PlayJumpEffect(transform.position);
        Sound.Play("Jump", transform.position, 1f, false);

        //test code
        if (bTest)
            animator.SetInteger("UnitState", (int)UnitState.DashStart);
    }

    public override void JumpFinished()
    {
        base.JumpFinished();

        effectComponent.PlayLandEffect(transform.position);
        Sound.Play("Land", transform.position, 1f, false);

        //test code
        if (bTest)
            animator.SetInteger("UnitState", (int)UnitState.Attack);
    }

    public override void ApplyKnockBack(Vector2 attackPos, float power)
    {
        moveComponent.KnockBack(attackPos,power);
        CalcKnockBackDirection(attackPos);
        effectComponent.PlayKnockBackEffect(transform.position);
        Sound.Play("KnockBack", transform.position, 1f, false);
    }
}
