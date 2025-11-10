using UnityEngine;

public class Sharks: MonoBehaviour
{
    public float speed = 1.0f;
    private float leftEdge;
    // Update is called once per frame

    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    }
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        
        if(transform.position.x < leftEdge)
        {                     
              Destroy(gameObject);            
        }
    }
}
