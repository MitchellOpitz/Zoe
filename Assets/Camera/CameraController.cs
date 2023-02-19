using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    public float cameraXMin = -25f;
    public float cameraXMax = 25f;
    public float cameraYMin = -8f;
    public float cameraYMax = 18f;
    public float minCameraSize = 1f;
    public float maxCameraSize = 10f;
    public float zoomSpeed = 5f;
    public float followSpeed = 5f;
    public KeyCode centerKeyCode;
    public float snapDuration = 0.2f;

    private Camera mainCamera;
    private float targetCameraSize;
    private bool isFollowing;
    private Vector3 targetPosition;
    private float snapStartTime;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        targetCameraSize = mainCamera.orthographicSize;
        targetPosition = transform.position;
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(centerKeyCode))
        {
            isFollowing = true;
            targetPosition = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
            snapStartTime = Time.time;
        }

        if (Input.GetKey(centerKeyCode))
        {
            isFollowing = true;
            targetPosition = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
        }

        if (Input.GetKeyUp(centerKeyCode))
        {
            isFollowing = false;
        }

        if (isFollowing)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
        else if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            transform.position -= new Vector3(mouseX, mouseY, 0f);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        targetCameraSize -= scroll * zoomSpeed;
        targetCameraSize = Mathf.Clamp(targetCameraSize, minCameraSize, maxCameraSize);
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetCameraSize, Time.deltaTime);

        float clampedX = Mathf.Clamp(transform.position.x, cameraXMin, cameraXMax);
        float clampedY = Mathf.Clamp(transform.position.y, cameraYMin, cameraYMax);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);

        if (isFollowing && Time.time - snapStartTime < snapDuration)
        {
            float t = (Time.time - snapStartTime) / snapDuration;
            transform.position = Vector3.Lerp(transform.position, targetPosition, t);
        }
    }
}
