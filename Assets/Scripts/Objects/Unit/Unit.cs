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

    //Arrow 임시 코드
    private float growSpeed = 1f;   // 1초당 얼마나 길어지는가
    private float maxLength = 25f;   // 최대 길이

    private float defaultLength = 12.3125f; // 초기 길이
    private float defaultWidth = 6.375f;
    private float curLength = 12.3125f;
    private float curWidth = 6.375f;

    private bool isHolding = false;

    private float targetSpeed = 3f;
    private float jumpLength = 1f;

    protected override void Awake()
    {
        ctx = new UnitContext();
        fsm = new StateMachine();
        moveComponent = GetComponent<PMoveComponent>();
        moveComponent.Initialize(ctx);

        ctx.Initialize(this, fsm, moveComponent, animator);

        fsm.AddState(new IdleState(ctx));
        fsm.AddState(new WalkState(ctx));
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        fsm.ChangeState<IdleState>();
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

            jumpLength += targetSpeed * Time.deltaTime;

            if (jumpLength > 10)
                jumpLength = 10;

            Vector2 direction = -jumpVector.normalized;
            Vector2 displacement = direction * jumpLength;

            targetPoint.transform.position = (Vector2)transform.position + displacement;
        }
    }

    public void Jump()
    {
       // moveComponent
    }
}
