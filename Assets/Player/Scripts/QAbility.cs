using UnityEngine;

public class QAbility : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform spawnPoint;
    public float maxDistance = 5f;
    public float projectileSpeed = 10f;

    private Vector3 targetPosition;

    void Update()
    {
        // Check for input
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Calculate target position
            targetPosition = CalculateTargetPosition();

            // Spawn projectile
            GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
            QController projectileController = projectile.GetComponent<QController>();
            projectileController.SetTargetPosition(targetPosition);
            projectileController.SetSpeed(projectileSpeed);
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
