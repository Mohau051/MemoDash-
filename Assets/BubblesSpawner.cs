using UnityEngine;


public class BubbleSpawner : MonoBehaviour
{
    public GameObject bubblePrefab;
    public float spawnInterval = 2f;
    public float spawnWidth = 5f;
    public float minY = -2f;
    public float maxY = 2f;

    void Start()
    {
        InvokeRepeating("SpawnBubble", 0f, spawnInterval);
    }

    void SpawnBubble()
    {
        Vector2 spawnPos = new Vector2(
            transform.position.x + Random.Range(-spawnWidth / 2, spawnWidth / 2),
            Random.Range(minY, maxY)
        );

        Instantiate(bubblePrefab, spawnPos, Quaternion.identity);
    }
}