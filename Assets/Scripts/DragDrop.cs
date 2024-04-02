using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    [SerializeField] private Transform cameraPosition;
    [SerializeField] LayerMask pickUpLayerMask;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private bool isGrabbing;

    [SerializeField] float pickUpDistance = 4f;
    private RaycastHit raycastHit;
    private ObjectGrabbable objectGrabbable;

    // UI
    [SerializeField] GameObject indicator;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(cameraPosition.position, cameraPosition.forward * 10, Color.green);
        if (Physics.Raycast(cameraPosition.position, cameraPosition.forward, out raycastHit, pickUpDistance, pickUpLayerMask))
        {
            indicator.SetActive(true);
        }
        else if (!Physics.Raycast(cameraPosition.position, cameraPosition.forward, out raycastHit, pickUpDistance, pickUpLayerMask) && !isGrabbing)
        {
            indicator.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (objectGrabbable == null)
            {
                if (Physics.Raycast(cameraPosition.position, cameraPosition.forward, out raycastHit, pickUpDistance, pickUpLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        objectGrabbable.Grab(objectGrabPointTransform);
                        isGrabbing = true;
                    }
                }
            }
            else
            {
                objectGrabbable.Drop();
                objectGrabbable = null;
                isGrabbing = false;
            }
        }
    }
}
