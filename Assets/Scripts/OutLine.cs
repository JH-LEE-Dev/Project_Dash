using UnityEngine;

public class OutLine : MonoBehaviour
{
    SpriteRenderer sr;
    [SerializeField] private Color color;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void ShowOutLine(Sprite currentSprite)
    {
        gameObject.SetActive(true);
        sr.sprite = currentSprite;
        transform.localScale = new Vector3(1.2f, 1.2f, 1f); // 10% 확대
    }

    public void HideOutLine()
    {
        transform.localScale = new Vector3(1f, 1f, 1f); // 기본 크기
        gameObject.SetActive(false);
    }

    public void SetCurrentSprite(Sprite sprite)
    {
        sr.sprite = sprite;
        sr.color = color;
    }
}
