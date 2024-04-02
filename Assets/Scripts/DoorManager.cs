using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] Transform playerCamera;
    [SerializeField] Transform distCheck;
    [SerializeField] Transform hinge;

    [SerializeField] float moveSpeed;
    [SerializeField] Vector2 rotationConstraints;

    bool movingDoor;
    float rotation;
    Vector3 targetPosition;

    [SerializeField] LayerMask layerMask;


    // Start is called before the first frame update
    void Start()
    {
        targetPosition = distCheck.position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(movingDoor);
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(playerCamera.position, playerCamera.forward, out RaycastHit hit, 3f))
            {
                if (hit.collider.CompareTag("Door"))
                {
                    movingDoor = true;
                }
            }
        }
        if (movingDoor)
        {
            if (Input.GetMouseButtonUp(0))
            {
                movingDoor = false;
            }
            targetPosition = playerCamera.position + playerCamera.forward * 2f;
        }

        rotation += Mathf.Clamp(-GetRotation() * 5000 * Time.deltaTime, -moveSpeed, moveSpeed);
        rotation = Mathf.Clamp(rotation, rotationConstraints.x, rotationConstraints.y);
        hinge.rotation = Quaternion.Euler(0, rotation, 0);
    }

    float GetRotation()
    {
        float firstDistance = (distCheck.position - targetPosition).sqrMagnitude;
        hinge.Rotate(Vector3.up);
        float secondDistance = (distCheck.position - targetPosition).sqrMagnitude;
        hinge.Rotate(-Vector3.up);
        return secondDistance - firstDistance;
    }
}
