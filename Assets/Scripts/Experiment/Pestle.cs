using UnityEngine;

public class Pestle : MonoBehaviour
{
    public Mortar mortar;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (mortar == null) return;

        if (other.CompareTag("Mortar"))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            float strength = rb != null ? rb.linearVelocity.magnitude : 0f;
            mortar.OnPestleHit(strength);
        }
    }

}
