using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.CanvasScaler;

public class Entity : MonoBehaviour
{
    //test code
    [SerializeField] protected bool bTest = false;

    protected ICommandSystem commandSystem;

    [Header("Components")]
    protected Rigidbody2D rb;
    protected Collider2D col;
    protected SpriteRenderer sr;
    public Animator animator { get; private set; }
    public StateMachine fsm { get; private set; }
    protected UnitContext ctx;

    [Header("Command System")]
    protected readonly Queue<ICommand> commandQueue = new Queue<ICommand>();

    [Header("Dash Attribute")]
    [SerializeField] protected GameObject arrowObject;
    protected Dir8 dashDirection;
    protected Vector2 dashVector;
    protected bool bSelected;
    protected bool bCharging;
    [SerializeField] protected GameObject targetPoint;

    [Header("Miscellaneous")]
    [SerializeField] protected GameObject outlineObject;
    protected OutLine outline;

    public virtual void Initialize(ICommandSystem commandSystem)
    {
        this.commandSystem = commandSystem;
    }

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        col = GetComponentInChildren<Collider2D>();
        outline = outlineObject.gameObject.GetComponent<OutLine>();
        fsm = new StateMachine();

        arrowObject.gameObject.SetActive(false);
    }

    protected virtual void OnDestroy()
    {

    }

    protected virtual void Start()
    {

    }

    protected virtual void OnEnable()
    {

    }


    protected virtual void Update()
    {
        ProcessNextCommand();
    }

    protected virtual void PollInit()
    {

    }

    public void ProcessNextCommand()
    {
        if (commandQueue == null)
        {
            Debug.Log("commandQueue is null -> Entity::ProcessNextCommand");
            return;
        }

        if (commandQueue.Count == 0)
            return;

        ICommand command = commandQueue.Dequeue();

        if (command == null)
        {
            Debug.Log("command is null -> Entity::ProcessNextCommand");
            return;
        }

        command.Execute(this);
    }

    public void EnqueueCommand(ICommand command)
    {
        if (command == null)
        {
            Debug.Log("command is null -> Entity::EnqueueCommand");
            return;
        }

        commandQueue.Enqueue(command);
    }

    public void ShowOutLine()
    {
        bSelected = true;

        if (outline == null)
        {
            Debug.Log("outline is null");

            return;
        }

        outline.ShowOutLine(sr.sprite);
    }

    public void HideOutLine()
    {
        outline.HideOutLine();
        arrowObject.gameObject.SetActive(false);
        targetPoint.gameObject.SetActive(false);
    }

    protected Vector2 CalcMousePos()
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();
        Vector3 sp = screenPos;
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(sp);

        return worldPos;
    }

    public void CalcDashDirection()
    {
        Vector2 mousePos = CalcMousePos();
        Vector2 curPos = new Vector2(transform.position.x, transform.position.y);

        Vector2 dir = mousePos - curPos;   // 방향 벡터
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        angle += 180;

        // -180~180 → 0~360 보정
        if (angle < 0)
            angle += 360f;

        // 8방향은 45° 단위 → 360/8 = 45
        int index = Mathf.RoundToInt(angle / 45f) % 8;

        dashDirection = (Dir8)index;
        dashVector = dir.normalized;

        if (((int)dashDirection >= 0 && (int)dashDirection <= 2) ||
                   ((int)dashDirection >= 6 && (int)dashDirection <= 7))
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    protected void CalcKnockBackDirection(Vector2 attackPos)
    {
        Vector2 dir = (Vector2)transform.position - attackPos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        //마우스 방향과 반대 방향으로 점프.
        angle += 180f;

        // -180~180 → 0~360 보정
        if (angle < 0)
            angle += 360f;

        int index = Mathf.RoundToInt(angle / 45f) % 8;

        dashDirection = (Dir8)index;
        dashVector = dir.normalized;
    }

    public void SetSelected(bool _bSelected)
    {
        bSelected = _bSelected;

        if (bSelected)
        {
            fsm.ChangeState<SelectedState>();
        }
        else
        {
            fsm.ChangeState<IdleState>();
        }
    }

    public Dir8 GetFacingDir()
    {
        return dashDirection;
    }

    public virtual void Dash()
    {

    }

    public virtual void DashFinished()
    {

    }

    public Collider2D GetCollider()
    {
        return col;
    }

    public virtual void ApplyKnockBack(Vector2 attackPos, float power)
    {

    }

    public virtual void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public bool IsSelected()
    {
        return bSelected;
    }

    public bool IsCharging()
    {
        return bCharging;
    }

    public virtual void SetCharging(bool bCharging)
    {
        this.bCharging = bCharging;
    }

    public virtual void HandleChargingState()
    {

    }

    public Vector2 GetDashVector()
    {
        return dashVector;
    }

    public void UpdateOutlineSprite()
    {
        outline.SetCurrentSprite(sr.sprite);
    }

    public virtual void Attack()
    {

    }
}
