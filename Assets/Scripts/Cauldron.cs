using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    public GameObject potionPrefab;
    public Transform spawnPoint;

    private Dictionary<IngredientType, Ingredients> selectedIngredients = new();

    public Vector3 finalColor;
    public bool finalCurseResist; public UniqueMagic finalMagic;
    public int finalMoisture; public int finalDurability;
    public bool finalGloss; public int finalAllergy;

    private void OnTriggerEnter(Collider other)
    {
        IngredientData data = other.GetComponent<IngredientData>();
        if (data != null)
        {
            // Replace the Selected one with the new one
            var type = data.ingredientData.type;
            selectedIngredients[type] = data.ingredientData;

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
        finalCurseResist = false; finalMagic = UniqueMagic.None;
        finalMoisture = 0; finalDurability = 0;
        finalGloss = false; finalAllergy = 0;

        foreach (var ingredient in selectedIngredients.Values)
        {
            finalColor += ingredient.color;

            finalCurseResist |= ingredient.curseResistance;
            foreach (var ing in selectedIngredients.Values)
            {
                if (ing.type == IngredientType.Active && ing.uniqueMagic != UniqueMagic.None)
                {
                    finalMagic = ing.uniqueMagic;
                    break;
                }
            }

            finalMoisture += ingredient.moisture;
            finalDurability += ingredient.durability;
            
            finalGloss |= ingredient.gloss;
            finalAllergy += ingredient.allergy;
        }
    }

    public void MixPotion()
    {
        Debug.Log($"Complete!");
        // Reset totalAttributes
        finalColor = new Vector3(0, 0, 0);
        finalCurseResist = false; finalMagic = new UniqueMagic();
        finalMoisture = 0; finalDurability = 0;
        finalGloss = false; finalAllergy = 0;
    }

    public void SpawnPotion()
    {
        GameObject newPotion = Instantiate(potionPrefab, spawnPoint.position, Quaternion.identity);
        Potion potionObj = newPotion.GetComponent<Potion>();
        potionObj.Initialize(finalColor, finalCurseResist, finalMagic, finalMoisture, finalDurability, finalGloss, finalAllergy);
    }
}