using UnityEngine;
using UnityEngine.InputSystem;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject[] customerPrefab;
    public static bool anyCustomerSpawned = false;
    public float reputationRequire;

    void OnMouseDown()
    {
        if (!anyCustomerSpawned && Player.Instance.Reputation >= reputationRequire) SpawnCustomer();  
    }

    void SpawnCustomer()
    {
        int r = Random.Range(0, customerPrefab.Length);
        Instantiate(customerPrefab[r], new Vector3(0 , 0, 0), Quaternion.identity);
        SFXManager.Instance.PlaySFX("KnockTheDoor");
        anyCustomerSpawned = true;
        Debug.Log($"Customer Spawn!");
    }
}
