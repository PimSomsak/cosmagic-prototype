using UnityEngine;

public class MortarNew : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ingredient"))
        {
            IngredientChange ing = col.collider.GetComponent<IngredientChange>();

            // Did the pestle hit it?
            if (col.otherCollider.GetComponent<PestleNew>() != null)
            {
                ing.ApplyHit();
                Debug.Log("Ingredient crushed!");
            }
        }
    }
}
