using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;
    public Transform spawnPoint;

    private bool hasSpawned = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !hasSpawned)
        {
            SpawnCustomer();
        }
    }

    void SpawnCustomer()
    {
        Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
        hasSpawned = true;
        Debug.Log("Customer Spawn!");
    }
}
