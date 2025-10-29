using UnityEngine;

public class IngredientChange : MonoBehaviour
{
    public GameObject crushedSprite;

    // How much crush force is needed before state changes
    public int hitsNeeded = 3;
    private int hitCount = 0;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.GetComponent<PestleNew>() != null)
        {
            ApplyHit();
            Debug.Log("Ingredient hit by pestle!");
        }
        
    }

    public void ApplyHit()
    {
        hitCount++;

        if (hitCount >= hitsNeeded)
        {
            hitCount = 0;

            UpdateSprite();
            Debug.Log($"Ingredient hit {hitCount}/{hitsNeeded}");
            Debug.Log($"Ingredient changed state");
        }
        else
        {
            Debug.Log($"Ingredient hit {hitCount}/{hitsNeeded}");
        }
        SFXManager.Instance.PlaySFX("GrindingActive");
    }

    private void UpdateSprite()
    {
        Instantiate(crushedSprite, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
