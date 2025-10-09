using System.Collections.Generic;
using UnityEngine;

public class CustomerBehaviour : MonoBehaviour
{
    public Customer customerData;

    public void Initialize(Customer data)
    {
        customerData = data;
        Debug.Log($"Customer {customerData.customerName} is ready with budget {customerData.budget}");
    }
    public class EvaluationResult
    {
        public bool colorOK;
        public bool curseOK;
        public bool magicOK;
        public bool moistureOK;
        public bool durabilityOK;
        public bool glossOK;
        public bool allergyOK;

        public bool IsSatisfied => colorOK && curseOK && magicOK && moistureOK && durabilityOK && glossOK && allergyOK;

        public string GetFeedback()
        {
            List<string> issues = new List<string>();

            if (!colorOK) issues.Add("❌ Color");
            if (!curseOK) issues.Add("❌ CurseResist");
            if (!magicOK) issues.Add("❌ Magic");
            if (!moistureOK) issues.Add("❌ Moisture");
            if (!durabilityOK) issues.Add("❌ Durability");
            if (!glossOK) issues.Add("❌ Gloss");
            if (!allergyOK) issues.Add("❌ Allergy");

            return issues.Count == 0 ? "✅ ALL CLEAR!" : string.Join("\n", issues);
        }
    }

    // Mix Data
    public EvaluationResult EvaluatePotion(Potion potion)
    {
        var result = new EvaluationResult();

        float colorDistance = Vector3.Distance(potion.color, customerData.colorMix);
        result.colorOK = colorDistance == 0 || customerData.colorMix.x == 5;
        result.curseOK = potion.curseResistance == customerData.isCurseResist || customerData.isCurseResist == CurseResistace.Any;
        result.magicOK = potion.uniqueMagic == customerData.magic || customerData.magic == UniqueMagic.Any;
        result.moistureOK = potion.moisture >= customerData.moistureRange.x && potion.moisture <= customerData.moistureRange.y;
        result.durabilityOK = potion.durability >= customerData.durabilityRange.x && potion.durability <= customerData.durabilityRange.y;
        result.glossOK = potion.gloss == customerData.isGloss || customerData.isGloss == Gloss.Any;
        result.allergyOK = potion.allergy >= customerData.allergyRange.x && potion.allergy <= customerData.allergyRange.y;

        return result;
    }

    // Check Potion Collide
    private void OnTriggerEnter2D(Collider2D other)
    {
        Potion potion = other.GetComponent<Potion>();
        if (potion != null)
        {
            var feedback = EvaluatePotion(potion);
            Debug.Log(feedback.GetFeedback());

            if (feedback.IsSatisfied)
            {
                Debug.Log("Customer is satisfied with the potion!");
                Wallet.Instance.AddMoney(customerData.budget);
            }
            else
            {
                Debug.Log("Customer is not satisfied...");
            }

            Destroy(other.gameObject);
        }
    }
}