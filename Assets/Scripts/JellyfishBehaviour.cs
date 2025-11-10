using UnityEngine;

public class JellyfishMover : MonoBehaviour
{
    public float moveSpeed = 1.5f;               // Leftward movement speed
    public float floatAmplitude = 0.8f;        // Vertical oscillation height
    public float floatFrequency = 1f;          // Vertical oscillation speed

    private float destroyX;
    private float baseY;

    void Start()
    { 
        baseY = transform.position.y;

        // Destroy point off the left edge of the screen
        destroyX = Camera.main.ViewportToWorldPoint(new Vector3(-0.2f, 0, 0)).x;
    }

    void Update()
    {
        // Horizontal movement
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        // Vertical floating (oscillation using sine wave)
        float yOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, baseY + yOffset, transform.position.z);

        // Destroy when off-screen
        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }

}
