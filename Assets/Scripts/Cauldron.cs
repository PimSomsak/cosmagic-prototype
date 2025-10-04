using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    public GameObject potionPrefab;
    public Transform spawnPoint;

    private Dictionary<IngredientType, Ingredients> selectedIngredients = new();

    public Vector3 finalColor;
    public CurseResistace finalCurseResist; public UniqueMagic finalMagic;
    public int finalMoisture; public int finalDurability;
    public Gloss finalGloss; public int finalAllergy;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IngredientData data = other.GetComponent<IngredientData>();
        if (data != null)
        {
            // Replace the Selected one with the new one
            var type = data.ingredientData.type;
            selectedIngredients[type] = data.ingredientData;
            Debug.Log($"{data.ingredientData.itemName} added.");
            Destroy(other.gameObject);

            if (selectedIngredients.Count == 3)
            {
                AddStats();
                SpawnPotion();
                MixPotion();
            }
        }
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

    public void MixPotion()
    {
        Debug.Log($"Mixing Complete! Potion has been created!");
        // Reset totalAttributes
        finalColor = new Vector3(0, 0, 0);
        finalCurseResist = new CurseResistace(); finalMagic = new UniqueMagic();
        finalMoisture = 0; finalDurability = 0;
        finalGloss = new Gloss(); finalAllergy = 0;
    }

    public void SpawnPotion()
    {
        GameObject newPotion = Instantiate(potionPrefab, spawnPoint.position, Quaternion.identity);
        Potion potionObj = newPotion.GetComponent<Potion>();
        potionObj.Initialize(finalColor, finalCurseResist, finalMagic, finalMoisture, finalDurability, finalGloss, finalAllergy);
    }
}