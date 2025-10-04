using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    [System.Serializable]
    public class IngredientThreshold
    {
        public string inputTag;           // tag วัตถุดิบใส่
        public int requiredAmount;        // ใส่กี่อัน
        public string[] tagsToDestroy;    // tag ของ prefab ที่จะถูกลบ
    }

    public IngredientThreshold[] thresholds;
    private Dictionary<string, int> ingredientCount = new Dictionary<string, int>();
    private List<string> completedIngredients = new List<string>();

    public TMP_Text feedbackText;
    public CauldronRecipe recipeSystem; // reference CauldronRecipe

    private void OnTriggerEnter2D(Collider2D other)
    {
        string tag = other.tag;

        foreach (var t in thresholds)
        {
            if (tag == t.inputTag)
            {
                if (!ingredientCount.ContainsKey(tag))
                    ingredientCount[tag] = 0;

                ingredientCount[tag]++;

                Debug.Log($"{tag}: {ingredientCount[tag]} / {t.requiredAmount}");

                // ครบจำนวน
                if (ingredientCount[tag] >= t.requiredAmount && !completedIngredients.Contains(tag))
                {
                    completedIngredients.Add(tag);

                    if (feedbackText != null)
                        feedbackText.text += ($"\n{tag}");

                    if (recipeSystem != null)
                        recipeSystem.AddReadyIngredient(tag);

                    // ลบ prefab ตาม tag
                    foreach (var destroyTag in t.tagsToDestroy)
                    {
                        GameObject[] objs = GameObject.FindGameObjectsWithTag(destroyTag);
                        foreach (var obj in objs)
                        {
                            Destroy(obj);
                        }
                        Debug.Log($"Destroyed all objects with tag: {destroyTag}");
                    }
                }

                Destroy(other.gameObject);
                break;
            }
        }
    }
}
