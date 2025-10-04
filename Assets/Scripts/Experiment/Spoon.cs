using UnityEngine;

public class Spoon : MonoBehaviour
{
    public CauldronRecipe cauldron;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (cauldron == null) return;

        if (other.CompareTag("Cauldron"))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            float strength = rb != null ? rb.linearVelocity.magnitude : 0f;
            cauldron.OnSpoonStir(strength);
        }
    }
}
