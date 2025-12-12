using System;
using UnityEngine;

public class ParabolaComponent
{
    public event Action DashFinishedEvent;

    public Vector2 start;
    public Vector2 end;
    public float height = 2f;    
    public float duration = 1f;  
    public Vector2 CurrentPos;
    public Vector2 CurrentLinearPos;

    private float t = 0f;
    bool bJumpFinished = false;

    public void Reset(Vector2 StartPos,Vector2 EndPos, float _height, float _duration)
    {
        start = StartPos;
        end = EndPos;
        height = _height;
        duration = _duration;
        t = 0;

        Start();
        bJumpFinished = false;
    }

    public void Start()
    {
       CurrentPos = start;
    }

    public void Update()
    {
        if (bJumpFinished)
            return;

        t += Time.deltaTime / duration;

        if (t > 1f) 
        { 
            t = 1f;
            bJumpFinished = true;
            DashFinishedEvent.Invoke();
        }

        CurrentPos = GetParabolaPos(start, end, height, t);
    }

    Vector2 GetParabolaPos(Vector2 start, Vector2 end, float height, float t)
    {
        CurrentLinearPos = Vector2.Lerp(start, end, t);

        float parabola = 4f * height * t * (1f - t);

        return CurrentLinearPos + Vector2.up * parabola;
    }

    public Vector2 GetCurrentLinearPos()
    {
        return CurrentLinearPos;
    }

    public Vector2 GetCurrentPos()
    {
        return CurrentPos;
    }
}
