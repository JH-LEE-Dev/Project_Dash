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
        sr.sprite = currentSprite;
        gameObject.SetActive(true);
        transform.localScale = new Vector3(1.2f, 1.2f, 1f); // 10% 확대
    }

    public void HideOutLine()
    {
        gameObject.SetActive(false);
        transform.localScale = new Vector3(1f, 1f, 1f); // 기본 크기
    }

    public void SetCurrentSprite(Sprite sprite)
    {
        sr.sprite = sprite;
        sr.color = color;
    }
}
