using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CauldronRecipe : MonoBehaviour
{
    [System.Serializable]
    public class Recipe
    {
        public string recipeName;
        public GameObject[] inputPrefabs; // วัตถุดิบที่ต้องใช้
        public int requiredHits = 3;       // จำนวนครั้งที่ต้องกวน
        public GameObject outputPrefab;    // ของที่ได้
    }

    [Header("Recipes")]
    public Recipe[] recipes;

    [Header("UI")]
    public Slider stirProgressSlider;

    private Dictionary<string, int> ingredientHits = new Dictionary<string, int>();
    private List<string> ingredientsInCauldron = new List<string>();
    private Recipe currentRecipe = null;

    // ตรวจกวน
    private Vector2 lastMousePos;
    private float moveThreshold = 5f;
    private bool isDragging = false;

    // ช้อนอยู่ในหม้อมั้ย
    private bool spoonInside = false;

    void Start()
    {
        if (stirProgressSlider != null)
        {
            stirProgressSlider.value = 0;
            stirProgressSlider.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        HandleStirInput();
    }

    // ingredient ใส่ครบ
    public void AddReadyIngredient(string tag)
    {
        if (!ingredientsInCauldron.Contains(tag))
            ingredientsInCauldron.Add(tag);

        if (!ingredientHits.ContainsKey(tag))
            ingredientHits[tag] = 0;

        Debug.Log($"[CauldronRecipe] Ingredient ready: {tag}");

        // เช็คว่าสูตรไหนท ingredients ครบ
        foreach (var r in recipes)
        {
            if (CheckRecipeCompleteIngredients(r))
            {
                currentRecipe = r;
                Debug.Log($"[CauldronRecipe] Recipe selected: {r.recipeName}, ready to stir!");
                UpdateProgressUI(r);
                return;
            }
        }
    }

    private void HandleStirInput()
    {
        if (ingredientsInCauldron.Count == 0 || currentRecipe == null) return;
        if (!spoonInside) return;

        if (Input.GetMouseButtonDown(0))
        {
            lastMousePos = Input.mousePosition;
            isDragging = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector2 delta = (Vector2)Input.mousePosition - lastMousePos;
            if (Mathf.Abs(delta.x) > moveThreshold)
            {
                lastMousePos = Input.mousePosition;
                Stir();
            }
        }
    }

    public void Stir()
    {
        if (currentRecipe == null)
        {
            Debug.Log("[CauldronRecipe] Ingredients are not complete for any recipe yet!");
            return;
        }

        foreach (var input in currentRecipe.inputPrefabs)
        {
            string tag = input.tag;
            if (ingredientHits.ContainsKey(tag))
            {
                ingredientHits[tag]++;
                Debug.Log($"[CauldronRecipe] Stirring {tag} - Hits: {ingredientHits[tag]}");
            }
        }

        bool allReady = true;
        foreach (var input in currentRecipe.inputPrefabs)
        {
            string tag = input.tag;
            if (!ingredientHits.ContainsKey(tag) || ingredientHits[tag] < currentRecipe.requiredHits)
            {
                allReady = false;
                break;
            }
        }

        if (allReady)
        {
            if (currentRecipe.outputPrefab != null)
                Instantiate(currentRecipe.outputPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);

            Debug.Log($"[CauldronRecipe] Recipe complete! Output: {currentRecipe.outputPrefab.name}");

            // Reset state
            ingredientsInCauldron.Clear();
            ingredientHits.Clear();
            currentRecipe = null;

            if (stirProgressSlider != null)
                stirProgressSlider.gameObject.SetActive(false);
        }
        else
        {
            UpdateProgressUI(currentRecipe);
        }
    }

    public void OnSpoonStir(float strength)
    {
        float minStrength = 0.5f;
        if (strength >= minStrength && spoonInside)
        {
            Debug.Log($"[CauldronRecipe] Spoon stir detected! Strength: {strength:F2}");
            Stir();
        }
    }

    private void UpdateProgressUI(Recipe r)
    {
        if (stirProgressSlider == null) return;

        float total = 0;
        foreach (var input in r.inputPrefabs)
        {
            string tag = input.tag;
            int hits = ingredientHits.ContainsKey(tag) ? ingredientHits[tag] : 0;
            total += (float)hits / r.requiredHits;
        }

        float progress = total / r.inputPrefabs.Length;
        if (progress > 0f)
            stirProgressSlider.gameObject.SetActive(true);

        stirProgressSlider.value = Mathf.Clamp01(progress);
    }

    private bool CheckRecipeCompleteIngredients(Recipe r)
    {
        foreach (var input in r.inputPrefabs)
        {
            if (!ingredientsInCauldron.Contains(input.tag))
                return false;
        }
        return true;
    }

    // ตรวจการชนช้อนหม้อ
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spoon"))
        {
            spoonInside = true;
            Debug.Log("[CauldronRecipe] Spoon entered cauldron!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Spoon"))
        {
            spoonInside = false;
            Debug.Log("[CauldronRecipe] Spoon left cauldron!");
        }
    }
}
