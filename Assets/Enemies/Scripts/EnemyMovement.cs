using UnityEngine;
using System.Collections;

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
    private bool moveHorizontal;

    void Start()
    {
        float randomNumber = Random.Range(0, 100);
        moveHorizontal = (float)randomNumber < 50f;
        SetTargetPosition();
    }

    void Update()
    {
        Type1Movement();
        Type2Movement();
        Type3Movement();

        if (!isStunned)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }

        if (transform.position == targetPosition)
        {
            DestroyImmediate(gameObject);
        }
    }

    public void SetTargetPosition()
    {
        switch (enemyType)
        {
            case EnemyType.Type1:
                Type1Movement();
                break;
            case EnemyType.Type2:
                Type2Movement();
                break;
            case EnemyType.Type3:
                Type3Movement();
                break;
            case EnemyType.Type4:
                break;
            case EnemyType.Type5:
                break;
        }
    }

    private void Type1Movement()
    {
        if (moveHorizontal)
        {
            // Horizontal
            targetPosition = transform.position + new Vector3(30f, 0, 0);
        } else
        {
            // Vertical
            targetPosition = transform.position + new Vector3(0, 30f, 0);
        }
    }

    private void Type2Movement()
    {
        if (enemyType == EnemyType.Type2)
        {
            targetPosition = GameObject.Find("Player").transform.position;
        }
    }

    private void Type3Movement()
    {
        float amplitude = 25f;
        float frequency = 5f;
        float sineWaveValue = Mathf.Sin(Time.time * frequency) * amplitude;

        if (enemyType == EnemyType.Type3)
        {
            Vector3 newPosition = transform.position;

            if (moveHorizontal)
            {
                targetPosition = transform.position + new Vector3(30f, sineWaveValue, 0);
            } else
            {
                targetPosition = transform.position + new Vector3(sineWaveValue, 30f, 0);
            }
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
