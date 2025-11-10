using UnityEngine;

public class OctopusSpawner : MonoBehaviour
{
    [Header("Core Settings")]
    public GameObject octopusPrefab;
    public float minSpawnInterval = 10f;
    public float maxSpawnInterval = 15f;
    [Range(0, 1)] public float twoOctopusChance = 0.2f;

    [Header("Screen Positioning")]
    [Tooltip("Vertical spawn range (viewport coordinates)")]
    public Vector2 spawnHeightRange = new Vector2(0.6f, 0.9f);
    public float sinkThreshold = 0.3f; // Below this Y, octopus will sink
    public float groupSpacing = 2f;

    [Header("Movement Settings")]
    public float baseSpeed = 1.5f;
    public float sinkSpeed = 1f;
    public float floatIntensity = 0.5f;

    private Camera _mainCam;
    private float _nextSpawnTime;
    private float _screenLeftEdge;

    void Start()
    {
        _mainCam = Camera.main;
        _screenLeftEdge = _mainCam.ViewportToWorldPoint(new Vector3(-0.2f, 0, 0)).x;
        ScheduleNextSpawn();
    }

    void Update()
    {
        if (Time.time >= _nextSpawnTime && _mainCam != null)
        {
            SpawnOctopusGroup();
            ScheduleNextSpawn();
        }
    }

    void SpawnOctopusGroup()
    {
        if (octopusPrefab == null) return;

        bool spawnPair = Random.value <= twoOctopusChance;
        float spawnX = _mainCam.ViewportToWorldPoint(new Vector3(1.1f, 0, 0)).x;

        // Spawn primary octopus
        SpawnSingleOctopus(spawnX);

        // Conditionally spawn second octopus
        if (spawnPair)
        {
            SpawnSingleOctopus(spawnX + groupSpacing);
        }
    }

    void SpawnSingleOctopus(float xPos)
    {
        float viewportY = Random.Range(spawnHeightRange.x, spawnHeightRange.y);
        Vector3 spawnPos = new Vector3(
            xPos,
            _mainCam.ViewportToWorldPoint(new Vector3(0, viewportY, 0)).y,
            0
        );

        GameObject octopus = Instantiate(octopusPrefab, spawnPos, Quaternion.identity);
        ConfigureOctopus(octopus, viewportY);
    }

    void ConfigureOctopus(GameObject octopus, float viewportY)
    {
        OctopusBehaviour behaviour = octopus.GetComponent<OctopusBehaviour>();
        if (behaviour != null)
        {
            bool shouldSink = viewportY < sinkThreshold;
            behaviour.Initialize(
                baseSpeed,
                shouldSink ? sinkSpeed : 0,
                floatIntensity,
                _screenLeftEdge
            );
        }
    }

    void ScheduleNextSpawn()
    {
        _nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    // Visualize spawn area in Scene view
    private void OnDrawGizmosSelected()
    {
        if (!_mainCam) return;

        float topY = _mainCam.ViewportToWorldPoint(new Vector3(0, spawnHeightRange.y, 0)).y;
        float bottomY = _mainCam.ViewportToWorldPoint(new Vector3(0, spawnHeightRange.x, 0)).y;
        float rightX = _mainCam.ViewportToWorldPoint(new Vector3(1.1f, 0, 0)).x;

        Gizmos.color = new Color(0.8f, 0.2f, 0.8f, 0.3f);
        Gizmos.DrawCube(
            new Vector3(rightX, (topY + bottomY) / 2, 0),
            new Vector3(1, topY - bottomY, 0)
        );
    }
}