using UnityEngine;

public class PourByTilt : MonoBehaviour
{
    public MoveJar jar;
    public GameObject ingredientPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 0.2f;
    public float tiltThreshold = 60f;
    private float timer = 0f;

    public int maxPourCount = 15;
    private int pouredCount = 0;

    void Update()
    {
        if (jar == null) return;

        float zRot = jar.CurrentRotation;
        if (zRot > 180) zRot -= 360;

        bool isPouring = Mathf.Abs(zRot) > tiltThreshold;

        if (isPouring && pouredCount < maxPourCount)
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                Instantiate(ingredientPrefab, spawnPoint.position, Quaternion.identity);
                pouredCount++;
                timer = 0f;

                if (pouredCount >= maxPourCount)
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            timer = 0f;
        }
    }
}
