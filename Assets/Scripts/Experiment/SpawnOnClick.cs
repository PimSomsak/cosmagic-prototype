using UnityEngine;

public class SpawnOnClick : MonoBehaviour
{
    [Header("prefabToSpawn")]
    public GameObject prefabToSpawn;

    [Header("spawnPoint")]
    public Transform spawnPoint;

    void OnMouseDown()
    {
        if (prefabToSpawn != null)
        {
            Vector3 pos = spawnPoint != null ? spawnPoint.position : transform.position;
            GameObject newObj = Instantiate(prefabToSpawn, pos, Quaternion.identity);
            Debug.Log($"Spawned prefab with tag {newObj.tag}!");
        }
    }
}
