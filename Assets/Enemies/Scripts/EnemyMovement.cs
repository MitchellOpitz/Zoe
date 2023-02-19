using UnityEngine;

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

    void Start()
    {
        targetPosition = transform.position;
        SetTargetPosition(targetPosition);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            SetTargetPosition(targetPosition);
        }
    }

    public void SetTargetPosition(Vector3 targetPos)
    {
        switch (enemyType)
        {
            case EnemyType.Type1:
                targetPosition = targetPos;
                targetPosition = new Vector3(Random.Range(-10f, 10f), Random.Range(-5f, 5f), 0f);
                break;
            case EnemyType.Type2:
                targetPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f);
                break;
            case EnemyType.Type3:
                targetPosition = new Vector3(Random.Range(-10f, 10f), Random.Range(-2f, 2f), 0f);
                break;
            case EnemyType.Type4:
                targetPosition = new Vector3(Random.Range(-2f, 2f), Random.Range(-10f, 10f), 0f);
                break;
            case EnemyType.Type5:
                targetPosition = new Vector3(Random.Range(-10f, 10f), 0f, 0f);
                break;
        }
    }
}
