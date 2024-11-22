using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifetime = 2f;
    [SerializeField] private float speed;

    private bool hit;
    private BoxCollider2D bCollider;
    private float direction;
    private float timer;

    [SerializeField] private PowerUpController powerUpController;

    private void Awake()
    {
        bCollider = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        timer = 0f;
        hit = false;
        bCollider.enabled = true;
    }

    private void Update()
    {
        if (hit) return;

        float movementSpeed = speed * direction;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(movementSpeed, 0);
        
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Projectile") || collision.CompareTag("PlayerFeet")) return; // evita destruccion


            hit = true;
            bCollider.enabled = false;
            Destroy(gameObject);
        
    }

    public void SetDirection(float _direction)
    {
        direction = _direction;

        float localScaleX = transform.localScale.x;

        if (Mathf.Sign(localScaleX) != _direction)
        {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
}
