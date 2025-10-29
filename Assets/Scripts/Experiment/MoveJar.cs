using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveJar : MonoBehaviour
{
    private bool moving = false;
    private Vector2 dragOffset;
    private Rigidbody2D rb;

    [Header("Rotation Range")]
    public float rotationMin = 0f;
    public float rotationMax = 180f;
    public float dragForce = 20f;

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
            //rb.AddForce(direction * dragForce, ForceMode2D.Force);       
            //rb.MovePosition(rb.position + direction * dragForce * Time.deltaTime);


            float targetRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            targetRotation = Mathf.Clamp(targetRotation, rotationMin, rotationMax);

            float newAngle = Mathf.LerpAngle(rb.rotation, targetRotation, Time.deltaTime * 10f);
            rb.MoveRotation(newAngle);

            CurrentRotation = newAngle;
        }
        else
        {
            CurrentRotation = rb.rotation;
        }
    }

    private void OnMouseDown()
    {
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
        moving = false;

        //rb.bodyType = RigidbodyType2D.Dynamic; unnecessary
        rb.gravityScale = 5f;
    }

    public void SetRotationRange(float min, float max)
    {
        rotationMin = min;
        rotationMax = max;
    }
}
