using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerBehaviour : MonoBehaviour
{
    public Customer customerData;
    CustomerSpawner customerSpawner;

    public Sprite[] customerEmotion;

    private void Start()
    {
        customerSpawner = FindAnyObjectByType<CustomerSpawner>();
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

        public bool IsSatisfied { get => colorOK && curseOK && magicOK && moistureOK && durabilityOK && glossOK && allergyOK; }

        public string GetFeedback(Customer customerData, Potion potion)
        {
            List<string> issues = new List<string>();

            if (!colorOK) issues.Add("Color Incorrect");
            if (!curseOK) issues.Add("CurseResist Incorrect");
            if (!magicOK) issues.Add("Magic Incorrect");
            if (!moistureOK)
            {
                if (potion.moisture < customerData.moistureRange.x) issues.Add("Moisture too low");
                else if (potion.moisture > customerData.moistureRange.y) issues.Add("Moisture too high");
            }
            if (!durabilityOK)
            {
                if (potion.durability < customerData.durabilityRange.x) issues.Add("Durability too low");
                else if (potion.durability > customerData.durabilityRange.y) issues.Add("Durability too high");
            }
            if (!glossOK) issues.Add("Gloss Incorrect");
            if (!allergyOK)
            {
                if (potion.allergy < customerData.allergyRange.x) issues.Add("Allergy too low");
                else if (potion.allergy > customerData.allergyRange.y) issues.Add("Allergy too high");
            }

            return issues.Count == 0 ? "ALL CLEAR!" : string.Join("\n", issues);
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
    public void OnTriggerEnter2D(Collider2D other)
    {
        Potion potion = other.GetComponent<Potion>();
        if (potion != null)
        {
            var feedback = EvaluatePotion(potion);

            // Feedback Display
            FeedbackBubble feedbackBubble = FindAnyObjectByType<FeedbackBubble>();
            if (feedbackBubble != null)
            {
                feedbackBubble.ShowFeedback(feedback, customerData, potion);
            }

            if (feedback.IsSatisfied)
            {
                Debug.Log("Customer is satisfied with the potion!");
                UpdateSprite(feedback);
                Player.Instance.AddMoney(customerData.budget);
                Player.Instance.AddReputation(customerData.reputation);
                StartCoroutine(DelayDestroyCustomer(3));
                CustomerSpawner.anyCustomerSpawned = false;
            }
            else
            {
                Debug.Log("Customer is not satisfied...");
                UpdateSprite(feedback);
            }

            Destroy(other.gameObject);
        }
    }
    IEnumerator DelayDestroyCustomer(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject target = GameObject.FindWithTag("Customer");
        if (target != null)
        {
            Destroy(target);
        }
    }
    public void UpdateSprite(EvaluationResult result)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (result.IsSatisfied)
        {
            sr.sprite = customerEmotion[0]; // Happy
        }
        else
        {
            sr.sprite = customerEmotion[1]; // Angry
        }
    }
}