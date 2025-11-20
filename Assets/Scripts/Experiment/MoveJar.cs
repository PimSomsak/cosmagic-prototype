using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveJar : MonoBehaviour
{
    private bool moving = false;
    private bool rotating = false;
    private Vector2 dragOffset;
    private Rigidbody2D rb;

    [Header("Rotation Range")]
    public float rotationMin = 0f;
    public float rotationMax = 180f;
    public float dragForce = 20f;

    [Header("Left Click Drag Rotation Limit")]
    public float dragRotationMin = -10f;
    public float dragRotationMax = 10f;

    public float CurrentRotation { get; private set; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.angularDamping = 2f;

        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    void Update()
    {
        if (moving)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 targetPos = new Vector2(mousePos.x, mousePos.y) - dragOffset;

            Vector2 direction = targetPos - rb.position;

            rb.linearVelocity = direction * dragForce;
            CurrentRotation = rb.rotation;
            //rb.AddForce(direction * dragForce, ForceMode2D.Force);       
            //rb.MovePosition(rb.position + direction * dragForce * Time.deltaTime);

            float targetRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            targetRotation = Mathf.Clamp(targetRotation, dragRotationMin, dragRotationMax);

            float newAngle = Mathf.LerpAngle(rb.rotation, targetRotation, Time.deltaTime * 10f);
            rb.MoveRotation(newAngle);

            CurrentRotation = newAngle;

        }

        if (rotating)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 targetPos = new Vector2(mousePos.x, mousePos.y) - dragOffset;
            Vector2 direction = targetPos - rb.position;

            float targetRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            targetRotation = Mathf.Clamp(targetRotation, rotationMin, rotationMax);

            float newAngle = Mathf.LerpAngle(rb.rotation, targetRotation, Time.deltaTime * 10f);
            rb.MoveRotation(newAngle);

            CurrentRotation = newAngle;
        }

        if (Input.GetMouseButtonUp(1))
        {
            rotating = false;
            rb.gravityScale = 5f;
        }
    }

    private void OnMouseDown()
    {
        if (!Input.GetMouseButton(0)) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragOffset = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        moving = true;

        //rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        SFXManager.Instance.PlaySFX("GrabObject");
    }

    private void OnMouseUp()
    {
        if (!Input.GetMouseButtonUp(0)) return;
        moving = false;

        //rb.bodyType = RigidbodyType2D.Dynamic; unnecessary
        rb.gravityScale = 5f;
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1)) // คลิกขวาลง
        {
            rotating = true;
            rb.gravityScale = 0f;
            rb.angularVelocity = 0f;
        }
    }

    public void SetRotationRange(float min, float max)
    {
        rotationMin = min;
        rotationMax = max;
    }
}





