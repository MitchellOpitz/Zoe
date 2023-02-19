using UnityEngine;

public class QController : MonoBehaviour
{
    public float speed = 10f;
    public float maxDistance = 5f;

    private Vector3 targetPosition;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
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
}
