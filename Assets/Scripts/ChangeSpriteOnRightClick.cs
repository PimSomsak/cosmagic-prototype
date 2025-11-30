using UnityEngine;

public class ChangeSpriteOnRightClick : MonoBehaviour
{
    [Header("Target Jar Object")]
    public MoveJar jar;

    [Header("Sprites")]
    public Sprite normalSprite;
    public Sprite rotatedSprite;

    private SpriteRenderer sr;
    public float tiltThreshold = 60f;
    private bool isRightClicking = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = normalSprite;
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(1))
            isRightClicking = true;

        if (Input.GetMouseButtonUp(1))
        {
            isRightClicking = false;
            sr.sprite = normalSprite;
        }

        if (isRightClicking)
            CheckRotation();

        CheckRotation();
    }

    void CheckRotation()
    {
        if (jar == null) return;

        float zRot = jar.CurrentRotation;
        if (zRot > 180) zRot -= 360;

        bool isPouring = Mathf.Abs(zRot) > tiltThreshold;

        if (isPouring)
            sr.sprite = rotatedSprite;
        else
            sr.sprite = normalSprite;
    }
}