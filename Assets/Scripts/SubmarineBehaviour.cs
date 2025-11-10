using UnityEngine;

public class SubmarineMover : MonoBehaviour
{
    public float moveSpeed = 1f;
    private float destroyX;

    void Start()
    {
        // Calculate the X position beyond which the submarine should be destroyed
        destroyX = Camera.main.ViewportToWorldPoint(new Vector3(-0.2f, 0, 0)).x;
    }

    void Update()
    {
        // Move left
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        // Destroy when off-screen
        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}
