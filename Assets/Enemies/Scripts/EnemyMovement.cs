using UnityEngine;
using System.Collections;
using System;

public class EnemyMovement : MonoBehaviour
{
    public enum EnemyType
    {
        Type1,
        Type2,
        Type3,
        Type4,
        Type5
    }

    public EnemyType enemyType;
    public float speed = 5f;

    private Vector3 targetPosition;
    private bool isStunned = false;

    void Start()
    {
        SetTargetPosition(targetPosition);
    }

    void Update()
    {
        if (!isStunned)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }

        if (transform.position == targetPosition)
        {
            DestroyImmediate(gameObject);
        }
    }

    public void SetTargetPosition(Vector3 targetPos)
    {
        switch (enemyType)
        {
            case EnemyType.Type1:
                Type1Movement();
                break;
            case EnemyType.Type2:
                break;
            case EnemyType.Type3:
                break;
            case EnemyType.Type4:
                break;
            case EnemyType.Type5:
                break;
        }
    }

    private void Type1Movement()
    {
        bool randomBool = UnityEngine.Random.Range(0, 2) == 0;
        if (randomBool)
        {
            // Horizontal
            targetPosition = transform.position + new Vector3(30f, 0, 0);
        } else
        {
            // Vertical
            targetPosition = transform.position + new Vector3(0, 30f, 0);
        }
    }

    public void Stun(int seconds)
    {
        if (!isStunned)
        {
            isStunned = true;
            // Disable movement and other actions here
            StartCoroutine(RecoverFromStun(seconds));
        }
    }

    private IEnumerator RecoverFromStun(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        // Enable movement and other actions here
        isStunned = false;
    }
}
