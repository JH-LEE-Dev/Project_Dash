using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Entity : MonoBehaviour
{
    [Header("Common Attribute")]
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected bool bSelected;
    protected Animator animator;

    [Header("Command System")]
    protected readonly Queue<ICommand> commandQueue = new Queue<ICommand>();

    [Header("Miscellaneous")]
    [SerializeField] protected GameObject outlineObject;
    protected OutLine outline;
    [SerializeField] protected GameObject arrowObject;
    protected Dir8 jumpDirection;
    protected Vector2 jumpVector;

    [SerializeField] protected GameObject targetPoint;

    public virtual void Initialize()
    {

    }

    protected virtual void Awake()
    {

    }

    protected virtual void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        outline = outlineObject.gameObject.GetComponent<OutLine>();

        arrowObject.gameObject.SetActive(false);
    }


    protected virtual void Update()
    {
        if(bSelected)
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
        if (commandQueue.Count == 0)
            return;

        ICommand command = commandQueue.Dequeue();

        if (command == null)
            return;

        command.Execute(this);
    }

    public void EnqueueCommand(ICommand command)
    {
        Debug.Log("MoveCommand");
        commandQueue.Enqueue(command);
    }

    public void ShowOutLine()
    {
        bSelected = true;

        outline.ShowOutLine(sr.sprite);
    }

    public void HideOutLine()
    {
        bSelected = false;

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

    protected void CalcJumpDirection()
    {
        Vector2 mousePos = CalcMousePos();
        Vector2 curPos = new Vector2(transform.position.x,transform.position.y);

        Vector2 dir = mousePos - curPos;   // 방향 벡터
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // -180~180 → 0~360 보정
        if (angle < 0)
            angle += 360f;

        // 8방향은 45° 단위 → 360/8 = 45
        int index = Mathf.RoundToInt(angle / 45f) % 8;

        jumpDirection = (Dir8)index;
        jumpVector = dir.normalized;
    }

    public void SetSelected(bool _bSelected)
    {
        bSelected = _bSelected;
    }
}
