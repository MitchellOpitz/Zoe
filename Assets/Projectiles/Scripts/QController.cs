using UnityEngine;

public class QController : MonoBehaviour
{
    public float speed = 10f;
    public float maxDistance = 5f;
    public float destroyTime = 2f; // Time in seconds before projectile is destroyed
    public int damage = 1;

    private Vector3 targetPosition;
    private Vector3 startPosition;
    private float lifetime = 0f; // Time in seconds since projectile was created

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Increment lifetime
        lifetime += Time.deltaTime;

        // Move towards target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Check if max distance reached
        if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
        {
            // Stop the projectile at the edge of the maxDistance circle
            Vector3 directionToStartPosition = (startPosition - transform.position).normalized;
            transform.position = startPosition + (directionToStartPosition * maxDistance);
            speed = 0f;
        }

        // Check if projectile has lived longer than destroyTime
        if (lifetime >= destroyTime)
        {
            Destroy(gameObject);
        }
    }

    public void SetTargetPosition(Vector3 target)
    {
        // Set target position
        targetPosition = target;

        // Make sure projectile stays on the same z-axis
        targetPosition.z = transform.position.z;
    }

    public void SetSpeed(float newSpeed)
    {
        // Set new speed
        speed = newSpeed;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if collided with an enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Get the enemy's health script
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();

            // If the enemy has health script, damage it
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            // Destroy the projectile
            Destroy(gameObject);
        }
    }
}
