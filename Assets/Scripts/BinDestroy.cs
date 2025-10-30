using UnityEngine;

public class BinDestroy : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Spoon" || collision.tag != "Pestle")
        { Destroy(collision.gameObject); }
        SFXManager.Instance.PlaySFX("RubbishBin");
    }
}
