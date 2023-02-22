using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QSplash : MonoBehaviour
{
    public float MovementSpeed = 5f;
    public int Damage = 10;
    public float MaxRange = 5f;

    private GameObject _targetEnemy;

    void Start()
    {
        // Find the nearest enemy within MaxRange
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, MaxRange);
        float closestDistance = Mathf.Infinity;
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    _targetEnemy = collider.gameObject;
                    closestDistance = distance;
                }
            }
        }

        // Move towards the target enemy
        if (_targetEnemy != null)
        {
            StartCoroutine(MoveToTargetEnemy());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator MoveToTargetEnemy()
    {
        while (Vector2.Distance(transform.position, _targetEnemy.transform.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, _targetEnemy.transform.position, MovementSpeed * Time.deltaTime);
            yield return null;
        }

        // Damage the enemy
        EnemyHealth enemyHealth = _targetEnemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(Damage);
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, MaxRange);
    }
}
