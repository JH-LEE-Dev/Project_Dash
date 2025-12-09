using UnityEngine;

public class ParabolaComponent
{
    public Vector2 start;
    public Vector2 end;
    public float height = 2f;     // 포물선 높이
    public float duration = 1f;   // 총 비행 시간
    public Vector2 CurrentPos;

    private float t = 0f;

    void Initialize(Vector2 StartPos,Vector2 EndPos)
    {
        start = StartPos;
        end = EndPos;
    }

    public void Start()
    {
       CurrentPos = start;
    }

    public void Update()
    {
        t += Time.deltaTime / duration;

        if (t > 1f) t = 1f;

        CurrentPos = GetParabolaPos(start, end, height, t);
    }

    Vector2 GetParabolaPos(Vector2 start, Vector2 end, float height, float t)
    {
        // 직선 내 보간 위치
        Vector2 linear = Vector2.Lerp(start, end, t);

        // 포물선 높이 (최대 height)
        float parabola = 4f * height * t * (1f - t);

        return linear + Vector2.up * parabola;
    }

    public Vector2 GetCurrentPos()
    {
        return CurrentPos;
    }
}
