using UnityEngine;

public class Termination : MonoBehaviour
{
    private Customer customerData;
    public void Terminate()
    {
        if (FindAnyObjectByType<CustomerBehaviour>() != null)
        {
            CustomerBehaviour customerBehaviour = FindAnyObjectByType<CustomerBehaviour>();
            customerData = customerBehaviour.customerData;
            Debug.Log("Customer Found");
        }
        else return;

        if (Player.Instance.money >= customerData.terminationFee)
        {
            Player.Instance.Spend(customerData.terminationFee);
            Player.Instance.SubtractReputation(customerData.reputation);
            Destroy(GameObject.FindWithTag("Customer"));
            CustomerSpawner.anyCustomerSpawned = false;
            Debug.Log("Terminated");
        }
        else Debug.Log("Not enough money to terminate"); return;
    }
}
