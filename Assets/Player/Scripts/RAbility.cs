using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RAbility : MonoBehaviour
{
    public GameObject portalPrefab;
    public float disableTime = 0.5f;
    public float portalDistance = 5f;
    public float portalDelay = 0.1f;
    public float waitTime = 1f;
    public float cooldownTime = 5f; // The time in seconds before the ability can be used again
    public bool isOnCooldown = false;

    private Vector3 targetPosition;
    private GameObject portal1;
    private GameObject portal2;
    private float lastAbilityTime = -Mathf.Infinity; // Initialize to negative infinity to allow first use

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && Time.time - lastAbilityTime > cooldownTime)
        {
            CreatePortal(transform.position);
            DisableMovement();
            Invoke("CreateSecondPortal", portalDelay);
            lastAbilityTime = Time.time;
            isOnCooldown = true;
        }

        if (Time.time - lastAbilityTime > cooldownTime)
        {
            isOnCooldown = false;
        }
    }

    void CreatePortal(Vector3 position)
    {
        portal1 = Instantiate(portalPrefab, position, Quaternion.identity);
        portal1.transform.position = new Vector3(position.x, position.y, 0f);
    }

    void CreateSecondPortal()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = mousePos - transform.position;
        dir.z = 0f;
        dir.Normalize();

        Vector3 portal2Pos = transform.position + dir * portalDistance;
        portal2 = Instantiate(portalPrefab, portal2Pos, Quaternion.identity);
        portal2.transform.position = new Vector3(portal2Pos.x, portal2Pos.y, 0f);

        Invoke("TeleportToSecondPortal", portalDelay);
    }

    void TeleportToSecondPortal()
    {
        transform.position = portal2.transform.position;
        Invoke("TeleportBackToFirstPortal", waitTime);
    }

    void TeleportBackToFirstPortal()
    {
        transform.position = portal1.transform.position;
        DisableMovement();
        Invoke("DestroyPortalsAndEnableMovement", disableTime);
    }

    void DisableMovement()
    {
        targetPosition = transform.position;
        GetComponent<Movement>().enabled = false;
    }

    void DestroyPortalsAndEnableMovement()
    {
        GetComponent<Movement>().enabled = true;
        GetComponent<Movement>().RemoveTargetPosition();

        Destroy(portal1);
        Destroy(portal2);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, portalDistance);
    }
}
