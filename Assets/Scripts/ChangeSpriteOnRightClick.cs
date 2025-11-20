using UnityEngine;

public class ChangeSpriteOnRightClick : MonoBehaviour
{
    public Sprite normalSprite;     // sprite ปกติ
    public Sprite rightClickSprite; // sprite ตอนคลิกขวา

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (normalSprite != null)
            sr.sprite = normalSprite;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (rightClickSprite != null)
                sr.sprite = rightClickSprite;
        }

        if (Input.GetMouseButtonUp(1))
        {
            if (normalSprite != null)
                sr.sprite = normalSprite;
        }
    }
}
