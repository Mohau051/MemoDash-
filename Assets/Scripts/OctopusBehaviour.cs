using UnityEngine;

public class OctopusBehaviour : MonoBehaviour
{
    // Movement parameters (set by spawner)
    private float _moveSpeed;
    private float _sinkSpeed;
    private float _floatIntensity;
    private float _destroyX;

    // Runtime state
    private float _baseY;
    private bool _isSinking;

    public void Initialize(float moveSpeed, float sinkSpeed, float floatIntensity, float destroyX)
    {
        _moveSpeed = moveSpeed;
        _sinkSpeed = sinkSpeed;
        _floatIntensity = floatIntensity;
        _destroyX = destroyX;
        _baseY = transform.position.y;
        _isSinking = sinkSpeed > 0;

        if (_isSinking)
            GetComponent<SpriteRenderer>().color = new Color(1, 0.8f, 0.8f);
    }

    void Update()
    {
        // Horizontal movement
        transform.Translate(Vector2.left * _moveSpeed * Time.deltaTime);

        // Vertical movement
        if (_isSinking)
        {
            transform.Translate(Vector2.down * _sinkSpeed * Time.deltaTime);
        }
        else
        {
            float yOffset = Mathf.Sin(Time.time) * _floatIntensity;
            transform.position = new Vector3(transform.position.x, _baseY + yOffset, 0);
        }

        // Cleanup
        if (transform.position.x < _destroyX)
            Destroy(gameObject);
    }
}