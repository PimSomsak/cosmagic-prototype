using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine.UI;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    public GameObject potionPrefab;

    private Dictionary<IngredientType, Ingredients> selectedIngredients = new();

    // ตรวจกวน
    private Vector2 lastMousePos;
    private float moveThreshold = 5f;
    private bool isDragging = false;
    // ช้อนอยู่ในหม้อมั้ย
    private bool spoonInside = false;

    //[Header("UI")]
    //public Slider stirProgressSlider;

    private int hits = 0;
    public int requiredHits = 8;

    private Vector3 finalColor;
    private CurseResistace finalCurseResist; private UniqueMagic finalMagic;
    private int finalMoisture; private int finalDurability;
    private Gloss finalGloss; private int finalAllergy;

    /*private int solventRequire = 15;
    private int solventCount = 0;
    private int additiveRequire = 15;
    private int additiveCount = 0;*/


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spoon"))
        {
            spoonInside = true;
            Debug.Log("[CauldronRecipe] Spoon entered cauldron!");
        }

        IngredientData data = other.GetComponent<IngredientData>();
        if (data != null)
        {
            // Replace the Selected one with the new one
            var type = data.ingredientData.type;
            selectedIngredients[type] = data.ingredientData;
            Debug.Log($"{data.ingredientData.itemName} added.");
            Destroy(other.gameObject);
            AddStats();
        }
        /*else if (data == null)
        {
            if (gameObject.name == "Water" || gameObject.name == "Water(Clone)")
            {
                solventCount++;
                if (solventCount == solventRequire)
                {
                }
            }
            else if ()
            {

            }
        }*/
    }
    void AddStats()
    {
        // Get 0 Stats
        finalColor = new Vector3(0, 0, 0);
        finalCurseResist = CurseResistace.Any; finalMagic = UniqueMagic.Any;
        finalMoisture = 0; finalDurability = 0;
        finalGloss = Gloss.Any; finalAllergy = 0;

        foreach (var ingredient in selectedIngredients.Values)
        {
            finalColor += ingredient.color;
            finalColor = new Vector3(
                Mathf.Clamp(finalColor.x, 0f, 1f),
                Mathf.Clamp(finalColor.y, 0f, 1f),
                Mathf.Clamp(finalColor.z, 0f, 1f));

            if (ingredient.type == IngredientType.Active && ingredient.curseResistance != CurseResistace.Any)
            {
                finalCurseResist = ingredient.curseResistance;
            }
            if (ingredient.type == IngredientType.Active && ingredient.uniqueMagic != UniqueMagic.Any)
            {
                finalMagic = ingredient.uniqueMagic;
            }

            finalMoisture += ingredient.moisture;
            finalDurability += ingredient.durability;
            
            if (ingredient.type == IngredientType.Additive && ingredient.gloss != Gloss.Any)
            {
                finalGloss = ingredient.gloss;
            }
            finalAllergy += ingredient.allergy;
        }
    }

    public void ResetCauldron()
    {
        // Reset totalAttributes
        finalColor = new Vector3(0, 0, 0);
        finalCurseResist = new CurseResistace(); finalMagic = new UniqueMagic();
        finalMoisture = 0; finalDurability = 0;
        finalGloss = new Gloss(); finalAllergy = 0;
    }

    public void SpawnPotion()
    {
        GameObject newPotion = Instantiate(potionPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        Potion potionObj = newPotion.GetComponent<Potion>();
        potionObj.Initialize(finalColor, finalCurseResist, finalMagic, finalMoisture, finalDurability, finalGloss, finalAllergy);
        Debug.Log($"Mixing Complete! Potion has been created!");
    }
    public void Stir()
    {
        if (selectedIngredients.Count < 3) return;
        hits++;
        Debug.Log("Hit");

        if (hits == requiredHits)
        {
            SpawnPotion();
            ResetCauldron();
        }
    }
    public void OnSpoonStir(float strength)
    {
        float minStrength = 0.5f;
        if (strength >= minStrength && spoonInside)
        {
            Debug.Log($"Spoon stir detected! Strength: {strength:F2}");
            Stir();
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