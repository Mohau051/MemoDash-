using UnityEngine;

public class SharkSpawner : MonoBehaviour
{
    public GameObject sharkPrefab;
    public float spawnInterval =2f;

    [Header("Spawn Distribution")]
    [Range(0, 1)] public float bottomSpawnChance = 0.3f;
    [Range(0, 1)] public float topSpawnChance = 0.3f;
    [Range(0, 1)] public float twoSharksChance = 0.2f;
    public float sharkGap = 1.5f;
    public float verticalSharkSpacing = 2f; // Space between vertically stacked sharks

    [Header("Screen Margin")]
    [Range(0, 1)] public float verticalMargin = 0.1f;
    [Range(0, 1)] public float edgeAvoidance = 0.2f;

    private Camera mainCamera;
    private float minY;
    private float maxY;
    private float safeMinY;
    private float safeMaxY;

    private void Start()
    {
        mainCamera = Camera.main;
        CalculateScreenBounds();
        InvokeRepeating(nameof(SpawnShark), spawnInterval, spawnInterval);
    }

    void CalculateScreenBounds()
    {
        maxY = mainCamera.ViewportToWorldPoint(new Vector3(0, 1 - verticalMargin, 0)).y;
        minY = mainCamera.ViewportToWorldPoint(new Vector3(0, verticalMargin, 0)).y;
        safeMaxY = maxY * (1 - edgeAvoidance);
        safeMinY = minY * (1 - edgeAvoidance);
    }

    void SpawnShark()
    {
        if (sharkPrefab == null) return;

        bool isDoubleSpawn = Random.value <= twoSharksChance;
        float baseX = mainCamera.ViewportToWorldPoint(new Vector3(1.1f, 0, 0)).x;

        if (isDoubleSpawn)
        {
            // Get a primary Y position
            float primaryY = GetStrategicYPosition();

            // Calculate secondary Y position with spacing
            float secondaryY = primaryY + verticalSharkSpacing;

            // Adjust if too close to top edge
            if (secondaryY > maxY)
            {
                secondaryY = primaryY - verticalSharkSpacing;
            }

            Instantiate(sharkPrefab, new Vector3(baseX, primaryY, 0), Quaternion.identity);
            Instantiate(sharkPrefab, new Vector3(baseX + sharkGap, secondaryY, 0), Quaternion.identity);
        }
        else
        {
            float yPos = GetStrategicYPosition();
            Instantiate(sharkPrefab, new Vector3(baseX, yPos, 0), Quaternion.identity);
        }
    }

    float GetStrategicYPosition()
    {
        float roll = Random.value;

        if (roll <= bottomSpawnChance)
            return Random.Range(minY, minY + (safeMinY - minY));
        else if (roll <= bottomSpawnChance + topSpawnChance)
            return Random.Range(safeMaxY, maxY);
        else
            return Random.Range(safeMinY, safeMaxY);
    }

    private void OnDrawGizmosSelected()
    {
        if (!mainCamera) return;
        CalculateScreenBounds();

        float xPos = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0, 0)).x;
        float width = mainCamera.ViewportToWorldPoint(new Vector3(1.1f, 0, 0)).x - xPos;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            new Vector3(xPos, (minY + safeMinY) / 2, 0),
            new Vector3(width, safeMinY - minY, 0)
        );

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(
            new Vector3(xPos, (safeMaxY + maxY) / 2, 0),
            new Vector3(width, maxY - safeMaxY, 0)
        );

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(
            new Vector3(xPos, (safeMinY + safeMaxY) / 2, 0),
            new Vector3(width, safeMaxY - safeMinY, 0)
        );
    }
}