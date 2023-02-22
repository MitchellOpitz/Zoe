using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float spawnDelay = 1f; // Delay between spawns in seconds
    public float spawnDistance = 10f; // Distance from camera clamp to spawn enemies

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        // Start spawning enemies
        InvokeRepeating("SpawnEnemy", spawnDelay, spawnDelay);
    }

    void SpawnEnemy()
    {
        // Choose a random enemy prefab to spawn
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        // Instantiate enemy outside of camera clamp distance
        Vector3 spawnPosition = GetRandomSpawnPosition();
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Assign enemy movement script
        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.SetTargetPosition();
        }
    }

    public void ResetSpawner(int level)
    {
        CancelInvoke("SpawnEnemy");
        InvokeRepeating("SpawnEnemy", spawnDelay, spawnDelay / (1 + (level * 0.1f)));

    }

    Vector3 GetRandomSpawnPosition()
    {
        // Get camera clamp dimensions
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        Vector2 cameraPosition = mainCamera.transform.position;

        // Choose random side to spawn on
        int side = Random.Range(0, 4);
        switch (side)
        {
            case 0: // Top
                return new Vector3(cameraPosition.x + Random.Range(-cameraWidth / 2f, cameraWidth / 2f), cameraPosition.y + cameraHeight / 2f + spawnDistance, 0f);
            case 1: // Right
                return new Vector3(cameraPosition.x + cameraWidth / 2f + spawnDistance, cameraPosition.y + Random.Range(-cameraHeight / 2f, cameraHeight / 2f), 0f);
            case 2: // Bottom
                return new Vector3(cameraPosition.x + Random.Range(-cameraWidth / 2f, cameraWidth / 2f), cameraPosition.y - cameraHeight / 2f - spawnDistance, 0f);
            case 3: // Left
                return new Vector3(cameraPosition.x - cameraWidth / 2f - spawnDistance, cameraPosition.y + Random.Range(-cameraHeight / 2f, cameraHeight / 2f), 0f);
            default:
                return Vector3.zero;
        }
    }
}
