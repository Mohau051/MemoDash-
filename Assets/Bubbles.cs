using UnityEngine;

public class BubbleBehaviour : MonoBehaviour
{
    public float floatSpeed = 1f;
    public float swayAmount = 0.3f;
    private float randomOffset;

    public void Initialize(float speed, float drift)
    {
        randomOffset = Random.Range(0f, 100f); // Unique starting point for sway
        transform.localScale = Vector3.one * Random.Range(0.8f, 1.2f); // Random size
    }

    void Update()
    {
        // Float upward
        transform.Translate(Vector3.up * floatSpeed * Time.deltaTime);

        // Gentle side-to-side sway
        float sway = Mathf.Sin(Time.time + randomOffset) * swayAmount;
        transform.position = new Vector3(
            transform.position.x + sway * Time.deltaTime,
            transform.position.y,
            0
        );

        // Destroy when above screen
        if (transform.position.y > Camera.main.ViewportToWorldPoint(Vector3.one).y + 1f)
            Destroy(gameObject);

    }
}
