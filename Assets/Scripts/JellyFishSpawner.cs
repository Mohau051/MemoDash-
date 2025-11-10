using UnityEngine;

public class JellyfishSpawner : MonoBehaviour
{
    [Header("Core Settings")]
    public GameObject jellyfishPrefab;
    public float minSpawnInterval = 2.5f;
    public float maxSpawnInterval = 4f;
    [Range(0f, 1f)] public float groupSpawnChance = 0.3f;
    public float jellyfishGap = 1.2f;

    [Header("Targeting Memo's Path")]
    public float centerOffsetY = 0.5f; // How much above Memo's usual height
    public float verticalSpread = 1.5f; // Tight spread around path
    public float horizontalStart = 0.8f; // 0.8 = 80% across screen (closer spawn)

    private Camera mainCamera;
    private Transform memo;
    private float nextSpawnTime;

    void Start()
    {
        mainCamera = Camera.main;
        memo = GameObject.FindGameObjectWithTag("Player").transform;
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime && memo != null)
        {
            SpawnJellyfishGroup();
            nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }

    void SpawnJellyfishGroup()
    {
        if (jellyfishPrefab == null) return;

        // Calculate dynamic center based on Memo's recent height
        float dynamicCenterY = memo.position.y + centerOffsetY;

        // Determine group size
        int groupSize = 1;
        float roll = Random.value;

        if (roll <= groupSpawnChance * 0.5f) groupSize = 3;
        else if (roll <= groupSpawnChance) groupSize = 2;

        // Base X position (closer to player)
        float xBase = mainCamera.ViewportToWorldPoint(new Vector3(horizontalStart, 0, 0)).x;

        for (int i = 0; i < groupSize; i++)
        {
            // Vertical positioning - tighter spread near Memo's path
            float yPos = dynamicCenterY + Random.Range(-verticalSpread, verticalSpread);
            yPos = Mathf.Clamp(yPos,
                mainCamera.ViewportToWorldPoint(new Vector3(0, 0.1f, 0)).y,
                mainCamera.ViewportToWorldPoint(new Vector3(0, 0.9f, 0)).y
            );

            // Horizontal staggering
            float xPos = xBase + (i * jellyfishGap);

            Instantiate(jellyfishPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
        }
    }

    // Visualize spawn area in Scene view
    private void OnDrawGizmos()
    {
        if (!mainCamera || memo == null) return;

        float centerY = memo.position.y + centerOffsetY;
        float xPos = mainCamera.ViewportToWorldPoint(new Vector3(horizontalStart, 0, 0)).x;

        Gizmos.color = new Color(1, 0.3f, 0.7f, 0.4f);
        Gizmos.DrawCube(
            new Vector3(xPos, centerY, 0),
            new Vector3(2f, verticalSpread * 2, 0)
        );
    }
}