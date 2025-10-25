using UnityEngine;
using TMPro;
using System.Collections;
using System.Transactions;

public class FeedbackBubble : MonoBehaviour
{
    [Header("Customer Feedback")]
    public TextMeshProUGUI customerFeedbackText;

    public void ShowFeedback(CustomerBehaviour.EvaluationResult result, Customer customerData, Potion potion)
    {
        customerFeedbackText.text = result.GetFeedback(customerData, potion);
    }
}
