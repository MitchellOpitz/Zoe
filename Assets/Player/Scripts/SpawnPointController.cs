using UnityEngine;

public class SpawnPointController : MonoBehaviour
{
    public Transform playerTransform;
    public float spawnDistance = 5f;

    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = CalculateTargetPosition();
    }

    void Update()
    {
        // Calculate target position
        targetPosition = CalculateTargetPosition();

        // Move towards target position
        Vector3 newPosition = playerTransform.position + (targetPosition - playerTransform.position).normalized * spawnDistance;
        newPosition.z = 0f;
        transform.position = newPosition;
    }

    void OnDrawGizmosSelected()
    {
        // Draw gizmo for spawn distance circle
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerTransform.position, spawnDistance);

        // Draw gizmo from spawn point to target position
        Gizmos.DrawLine(transform.position, targetPosition);
    }

    private Vector3 CalculateTargetPosition()
    {
        // Calculate target position on circle around player
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 directionToMouse = (mousePosition - playerTransform.position).normalized;
        Vector3 targetPosition = playerTransform.position + (directionToMouse * spawnDistance);

        return targetPosition;
    }
}
