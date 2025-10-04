using UnityEngine;

public class ShakeByTilt : MonoBehaviour
{
    [Header("References")]
    public MoveJar jar;
    public GameObject ingredientPrefab;
    public Transform spawnPoint;

    [Header("Shake Settings")]
    public float spawnInterval = 0.2f;
    public float tiltThreshold = 30f;
    public float shakeDistanceThreshold = 0.1f; // ขยับซ้ายขวา
    public int minSpawnCount = 1;
    public int maxSpawnCount = 5;

    private float timer = 0f;
    private float lastX;
    private int shakeDirection = 0;
    private int shakeCount = 0;

    void Start()
    {
        if (jar != null)
            lastX = jar.transform.position.x;
    }

    void Update()
    {
        if (jar == null) return;

        float zRot = jar.CurrentRotation;
        if (zRot > 180) zRot -= 360;

        if (Mathf.Abs(zRot) < tiltThreshold)
        {
            shakeCount = 0;
            shakeDirection = 0;
            timer = 0f;
            lastX = jar.transform.position.x;
            return;
        }

        float deltaX = jar.transform.position.x - lastX;

        if (deltaX > shakeDistanceThreshold && shakeDirection != 1)
        {
            shakeDirection = 1;
            shakeCount++;
        }
        else if (deltaX < -shakeDistanceThreshold && shakeDirection != -1)
        {
            shakeDirection = -1;
            shakeCount++;
        }

        lastX = jar.transform.position.x;

        if (shakeCount >= 2 && ingredientPrefab != null && spawnPoint != null)
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                int count = Random.Range(minSpawnCount, maxSpawnCount + 1);
                for (int i = 0; i < count; i++)
                {
                    Vector3 randomOffset = new Vector3(Random.Range(-0.1f, 1.1f), Random.Range(-0.1f, 1.1f), 0f);
                    Instantiate(ingredientPrefab, spawnPoint.position + randomOffset, Quaternion.identity);
                }

                timer = 0f;
                shakeCount = 0;
            }
        }
    }
}
