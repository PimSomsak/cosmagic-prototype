using UnityEngine;
using UnityEngine.InputSystem;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;
    public Transform spawnPoint;

    private bool hasSpawned = false;
    void Update()
    {
        if ((Keyboard.current.spaceKey.wasPressedThisFrame) && !hasSpawned)
        {
            SpawnCustomer();
        }
    }

    void SpawnCustomer()
    {
        Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
        hasSpawned = true;
        Debug.Log($"Customer Spawn!");
    }
}
