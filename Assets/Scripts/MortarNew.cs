using UnityEngine;

public class MortarNew : MonoBehaviour
{
    /*void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ingredient"))
        {
            IngredientChange ing = col.GetComponent<IngredientChange>();
            if (ing != null) return;
            
            if (GetComponent<PestleNew>() != null)
            {
                ing.ApplyHit();
                Debug.Log("Ingredient crushed!");
            }     
        }
    }*/

    /*void OnTriggerEnter2D(Collider2D col)
    {
        GameObject other = col.gameObject;
        GameObject self = col.gameObject;

        if (other.CompareTag("Ingredient") && self.GetComponent<PestleNew>() != null)
        {
            IngredientChange ing = other.GetComponent<IngredientChange>();
            if (ing != null)
            {
                ing.ApplyHit();
                Debug.Log("Ingredient crushed!");
            }
        }
        else if (self.CompareTag("Ingredient") && other.GetComponent<PestleNew>() != null)
        {
            IngredientChange ing = self.GetComponent<IngredientChange>();
            if (ing != null)
            {
                ing.ApplyHit();
                Debug.Log("Ingredient crushed!");
            }
        }
    }*/
}
