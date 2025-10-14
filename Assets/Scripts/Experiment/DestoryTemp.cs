using UnityEngine;

public class DestoryTemp : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Spoon" || collision.tag != "Pestle")
        { Destroy(collision.gameObject); }
    }
}
