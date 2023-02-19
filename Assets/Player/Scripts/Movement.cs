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
        Vector3 clampedTargetPosition = targetPosition;
        Vector3 cameraPosition = Camera.main.transform.position;

        // Get camera bounds
        float vertExtent = Camera.main.orthographicSize;
        float horizExtent = vertExtent * Screen.width / Screen.height;

        // Subtract half the player's size from clamping calculations
        float playerWidth = transform.localScale.x / 2f;
        float playerHeight = transform.localScale.y / 2f;
        horizExtent -= playerWidth;
        vertExtent -= playerHeight;

        // Clamp x position to camera bounds
        float clampedX = Mathf.Clamp(targetPosition.x, cameraPosition.x - horizExtent, cameraPosition.x + horizExtent);
        clampedTargetPosition.x = clampedX;

        // Clamp y position to camera bounds
        float clampedY = Mathf.Clamp(targetPosition.y, cameraPosition.y - vertExtent, cameraPosition.y + vertExtent);
        clampedTargetPosition.y = clampedY;

        if (Vector3.Distance(transform.position, clampedTargetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, clampedTargetPosition, moveSpeed * Time.deltaTime);
            Vector3 direction = (clampedTargetPosition - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
