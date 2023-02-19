using UnityEngine;

public class SpawnPointController : MonoBehaviour
{
    public Transform playerTransform;
    public float spawnDistance = 5f;
    public float rotationSpeed = 5f;

    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = CalculateTargetPosition();
    }

    void Update()
    {
        // Calculate target position
        targetPosition = CalculateTargetPosition();

        // Rotate towards target position
        Vector3 targetDirection = targetPosition - playerTransform.position;
        targetDirection.z = 0f;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, targetDirection);

        // Move towards target position
        Vector3 newPosition = playerTransform.position + (targetDirection.normalized * spawnDistance);
        newPosition.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * rotationSpeed);
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
