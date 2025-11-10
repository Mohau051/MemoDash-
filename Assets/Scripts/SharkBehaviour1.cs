using UnityEngine;

public class SharkBehaviour1 : MonoBehaviour
{
    public float speed = 1.0f;
    private float leftEdge;
    // Update is called once per frame

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }
}
