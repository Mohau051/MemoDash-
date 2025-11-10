using UnityEngine;

public class SubmarineSpawner : MonoBehaviour
{
    public GameObject submarinePrefab;
    public float spawnInterval = 10f; // Less frequent than sharks
    public float spawnChance = 0.5f;  // 50% chance to spawn each interval
    public float fixedY = -3.5f;      // Near bottom of the screen

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

        // Clamp spawn Y to stay within screen view
        float verticalBound = mainCamera.orthographicSize * 0.8f;
        fixedY = Mathf.Clamp(fixedY, -verticalBound, verticalBound);

        InvokeRepeating(nameof(AttemptSpawnSubmarine), spawnInterval, spawnInterval);
    }

    void AttemptSpawnSubmarine()
    {
        if (submarinePrefab == null) return;

        // Only spawn if random chance hits
        if (Random.value > spawnChance) return;

        float xPos = mainCamera.ViewportToWorldPoint(new Vector3(1.1f, 0, 0)).x;
        Instantiate(submarinePrefab, new Vector3(xPos, fixedY, 0f), Quaternion.identity);
    }
}
