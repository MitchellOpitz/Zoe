using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EAbility : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public Transform ProjectileSpawnPoint;
    public float ProjectileSpeed = 10f;
    public float MaxTravelDistance = 10f;
    public float CooldownTime = 3f;
    public float AreaOfEffectRadius = 2f;

    public bool isOnCooldown = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isOnCooldown)
        {
            Vector3 targetPosition = GetMouseWorldPosition();
            Vector3 spawnPosition = ProjectileSpawnPoint.position;
            GameObject projectile = Instantiate(ProjectilePrefab, spawnPosition, Quaternion.identity);
            projectile.GetComponent<EController>().SetTargetAndRadius(targetPosition, AreaOfEffectRadius);
            projectile.GetComponent<Rigidbody2D>().velocity = (targetPosition - spawnPosition).normalized * ProjectileSpeed;
            Destroy(projectile, MaxTravelDistance / ProjectileSpeed);
            isOnCooldown = true;
            StartCoroutine(StartCooldown());
        }
    }

    private IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(CooldownTime);
        isOnCooldown = false;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, MaxTravelDistance);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, AreaOfEffectRadius);
    }
}
