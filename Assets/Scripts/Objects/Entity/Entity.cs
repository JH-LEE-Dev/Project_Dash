using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Entity : MonoBehaviour
{
    //test code
    [SerializeField] protected bool bTest = false;

    protected ICommandSystem commandSystem;

    [Header("Components")]
    protected Rigidbody2D rb;
    protected Collider2D col;
    protected SpriteRenderer sr;
    protected Animator animator;

    [Header("Command System")]
    protected readonly Queue<ICommand> commandQueue = new Queue<ICommand>();

    [Header("Jump Attribute")]
    [SerializeField] protected GameObject arrowObject;
    protected Dir8 jumpDirection;
    protected Vector2 jumpVector;
    protected bool bSelected;
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
        if (bSelected)
        {
            outline.SetCurrentSprite(sr.sprite);
            CalcJumpDirection();
        }

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

        //test code
        if (bTest)
            animator.SetInteger("UnitState", (int)UnitState.CombatStart);
    }

    public void HideOutLine()
    {
        outline.HideOutLine();
        arrowObject.gameObject.SetActive(false);
        targetPoint.gameObject.SetActive(false);

        //test code
        if (bTest)
            animator.SetInteger("UnitState", (int)UnitState.DashStart);
    }

    protected Vector2 CalcMousePos()
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();
        Vector3 sp = screenPos;
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(sp);

        return worldPos;
    }

    protected void CalcJumpDirection()
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

        jumpDirection = (Dir8)index;
        jumpVector = dir.normalized;

        animator.SetFloat("FacingDir", (float)jumpDirection);
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

        jumpDirection = (Dir8)index;
        jumpVector = dir.normalized;

        animator.SetFloat("FacingDir", (float)jumpDirection);
    }

    public void SetSelected(bool _bSelected)
    {
        bSelected = _bSelected;
        HideOutLine();
    }

    public Dir8 GetFacingDir()
    {
        return jumpDirection;
    }

    public virtual void Jump()
    {

    }

    public virtual void JumpFinished()
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
}
