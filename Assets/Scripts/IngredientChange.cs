using UnityEngine;
public enum IngredientState { Raw, Crushed }

public class IngredientChange : MonoBehaviour
{
    public IngredientState state = IngredientState.Raw;

    public Sprite rawSprite;
    public Sprite crushedSprite;

    private SpriteRenderer sr;

    // How much crush force is needed before state changes
    public int hitsNeeded = 3;
    private int hitCount = 0;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        UpdateSprite();
    }
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
            if (state == IngredientState.Raw) state = IngredientState.Crushed;

            UpdateSprite();
            Debug.Log($"Ingredient hit {hitCount}/{hitsNeeded}");
            Debug.Log($"Ingredient state changed to {state}");
        }
        else
        {
            Debug.Log($"Ingredient hit {hitCount}/{hitsNeeded}");
        }
    }

    private void UpdateSprite()
    {
        if (state == IngredientState.Raw) sr.sprite = rawSprite;
        else if (state == IngredientState.Crushed) sr.sprite = crushedSprite;
    }
}
