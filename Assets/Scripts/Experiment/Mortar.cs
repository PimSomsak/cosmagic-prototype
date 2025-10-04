using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mortar : MonoBehaviour
{
    [System.Serializable]
    public class Recipe
    {
        public string recipeName;
        public GameObject[] inputPrefabs;    // Required ingredients
        public int requiredHits = 3;         // Total hits needed per ingredient
        public GameObject outputPrefab;      // Result after grinding
    }

    [Header("Recipes")]
    public Recipe[] recipes;

    [Header("UI")]
    public Slider grindProgressSlider;       // Progress bar

    private Dictionary<string, int> ingredientHits = new Dictionary<string, int>();
    private List<string> ingredientsInMortar = new List<string>();

    private Recipe currentRecipe = null; // สูตรตอนนี้

    void Start()
    {
        if (grindProgressSlider != null)
        {
            grindProgressSlider.value = 0;
            grindProgressSlider.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string tag = other.tag;

        foreach (var r in recipes)
        {
            foreach (var input in r.inputPrefabs)
            {
                if (tag == input.tag)
                {
                    if (!ingredientsInMortar.Contains(tag))
                        ingredientsInMortar.Add(tag);

                    if (!ingredientHits.ContainsKey(tag))
                        ingredientHits[tag] = 0;

                    Debug.Log($"[Mortar] Added ingredient: {tag}");
                    Destroy(other.gameObject); 

                    // เช็คว่า ingredientsInMortar ครบยัง
                    if (CheckRecipeCompleteIngredients(r))
                    {
                        currentRecipe = r; // lock สูตร
                        Debug.Log($"[Mortar] All ingredients for recipe {r.recipeName} are inside. Ready to grind!");
                        UpdateProgressUI(r);
                    }

                    return;
                }
            }
        }
    }

    public void Grind()
    {
        if (currentRecipe == null)
        {
            Debug.Log("[Mortar] Ingredients are not complete for any recipe yet!");
            return;
        }

        foreach (var input in currentRecipe.inputPrefabs)
        {
            string tag = input.tag;
            if (ingredientHits.ContainsKey(tag))
            {
                ingredientHits[tag]++;
                Debug.Log($"[Mortar] Grinding {tag} - Hits: {ingredientHits[tag]}");
            }
        }

        bool allIngredientsReady = true;
        foreach (var input in currentRecipe.inputPrefabs)
        {
            string tag = input.tag;
            if (!ingredientHits.ContainsKey(tag) || ingredientHits[tag] < currentRecipe.requiredHits)
            {
                allIngredientsReady = false;
                break;
            }
        }

        if (allIngredientsReady)
        {
            // output prefab
            if (currentRecipe.outputPrefab != null)
            {
                Instantiate(currentRecipe.outputPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
                Debug.Log($"[Mortar] Recipe complete! Output: {currentRecipe.outputPrefab.name}");
            }

            // Reset
            foreach (var input in currentRecipe.inputPrefabs)
                ingredientHits[input.tag] = 0;

            ingredientsInMortar.Clear();
            currentRecipe = null;

            if (grindProgressSlider != null)
                grindProgressSlider.gameObject.SetActive(false);

            return;
        }
        else
        {
            UpdateProgressUI(currentRecipe);
        }

        DebugCurrentState();
    }

    public void OnPestleHit(float strength)
    {
        float minStrength = 0.5f;
        if (strength >= minStrength)
        {
            Debug.Log($"[Mortar] Pestle hit detected! Strength: {strength:F2}");
            Grind();
        }
    }

    private void UpdateProgressUI(Recipe r)
    {
        if (grindProgressSlider == null) return;

        float total = 0;
        foreach (var input in r.inputPrefabs)
        {
            string tag = input.tag;
            int hits = ingredientHits.ContainsKey(tag) ? ingredientHits[tag] : 0;
            total += (float)hits / r.requiredHits;
        }
        float progress = total / r.inputPrefabs.Length;

        if (progress > 0f)
            grindProgressSlider.gameObject.SetActive(true);

        grindProgressSlider.value = Mathf.Clamp01(progress);
    }

    private bool CheckRecipeCompleteIngredients(Recipe r)
    {
        // เช็ค ingredientsInMortar ครบยัง
        foreach (var input in r.inputPrefabs)
        {
            if (!ingredientsInMortar.Contains(input.tag))
                return false;
        }
        return true;
    }

    private void DebugCurrentState()
    {
        if (ingredientsInMortar.Count == 0) return;

        foreach (var tag in ingredientsInMortar)
        {
            int hits = ingredientHits.ContainsKey(tag) ? ingredientHits[tag] : 0;
            Debug.Log($"Ingredient: {tag}, Hits: {hits}");
        }
    }
}
