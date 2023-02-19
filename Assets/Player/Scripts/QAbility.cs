using UnityEngine;

public class QAbility : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform spawnPoint;
    public float maxDistance = 5f;
    public float projectileSpeed = 10f;
    public float cooldownTime = 2f; // Cooldown time in seconds

    private Vector3 targetPosition;
    public bool isOnCooldown = false;
    private float cooldownTimer = 0f;

    void Update()
    {
        // Check for input and cooldown timer
        if (!isOnCooldown && Input.GetKeyDown(KeyCode.Q))
        {
            // Calculate target position
            targetPosition = CalculateTargetPosition();

            // Spawn projectile
            GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
            QController projectileController = projectile.GetComponent<QController>();
            projectileController.SetTargetPosition(targetPosition);
            projectileController.SetSpeed(projectileSpeed);

            // Start cooldown timer
            isOnCooldown = true;
            cooldownTimer = cooldownTime;
        }

        // Update cooldown timer
        if (isOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0f)
            {
                isOnCooldown = false;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw gizmo for max distance circle
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }

    private Vector3 CalculateTargetPosition()
    {
        // Calculate target position based on mouse position and max distance
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 directionToMouse = (mousePosition - transform.position).normalized;
        Vector3 targetPosition = transform.position + (directionToMouse * maxDistance);

        return targetPosition;
    }
}
