using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Vector3 targetPosition;
    private bool isMoving = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SetTargetPosition();
            isMoving = true;
        }

        if (Input.GetMouseButton(1))
        {
            SetTargetPosition();
        }

        MovePlayer();
    }

    void SetTargetPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.transform.position.z;
        targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        targetPosition.z = 0f;
    }

    void MovePlayer()
    {
        if (isMoving)
        {
            if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                Vector3 direction = (targetPosition - transform.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else
            {
                isMoving = false;
            }
        }
    }
}
