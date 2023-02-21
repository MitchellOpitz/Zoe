using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EController : MonoBehaviour
{
    private Vector3 _targetPosition;
    private float _areaOfEffectRadius;

    public void SetTargetAndRadius(Vector3 targetPosition, float areaOfEffectRadius)
    {
        _targetPosition = targetPosition;
        _areaOfEffectRadius = areaOfEffectRadius;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyMovement>().Stun(3);
            Explode();
        }
    }

    private void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _areaOfEffectRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.GetComponent<EnemyHealth>().TakeDamage(5);
                if (collider != null)
                {
                    collider.GetComponent<EnemyMovement>().Stun(2);
                }
            }
        }
        Destroy(gameObject);
    }
}
