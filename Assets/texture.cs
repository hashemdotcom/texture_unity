using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    public Sprite[] sprites; // لیست اسپرایت‌ها
    public float changeInterval = 6f; // زمان بین تغییرات
    private SpriteRenderer spriteRenderer;
    private int currentIndex = 0;
    private float timer = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[0];
        }
    }

    void Update()
    {
        // timer += Time.deltaTime;
        // if (timer >= changeInterval)
        // {
        //     timer = 0f;
        //     currentIndex = (currentIndex + 1) % sprites.Length;
        //     spriteRenderer.sprite = sprites[currentIndex];
        // }
    }
}
