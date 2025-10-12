using UnityEngine;
using UnityEngine.InputSystem;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject[] customerPrefab;

    public bool hasSpawned;
    void Update()
    {
        if ((Keyboard.current.spaceKey.wasPressedThisFrame) && !hasSpawned)
        {
            SpawnCustomer();
        }
    }

    void SpawnCustomer()
    {
        int r = Random.Range(0, customerPrefab.Length);
        Instantiate(customerPrefab[r], new Vector3(0 , 0, 0), Quaternion.identity);
        hasSpawned = true;
        Debug.Log($"Customer Spawn!");
    }
}
