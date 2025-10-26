using UnityEngine;
using UnityEngine.InputSystem;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject[] customerPrefab;
    public static bool anyCustomerSpawned = false;

    void OnMouseDown()
    {
        if (!anyCustomerSpawned) SpawnCustomer();  
    }

    void SpawnCustomer()
    {
        int r = Random.Range(0, customerPrefab.Length);
        Instantiate(customerPrefab[r], new Vector3(0 , 0, 0), Quaternion.identity);
        anyCustomerSpawned = true;
        Debug.Log($"Customer Spawn!");
    }
}
