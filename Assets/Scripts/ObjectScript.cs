using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    public CharacterController controller;
    public Transform holdPos;

    public float throwForce = 500f; 
    public float pickUpRange = 50f; 
    public float gravity = 9.81f;

    public GameObject heldObjPrefab; 
    private Rigidbody heldObjRb; 
    private bool canDrop = true; 

    private int LayerNumber;


    void Start()
    {
        LayerNumber = LayerMask.NameToLayer("heldObject");
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObjPrefab == null)
            {

                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    if (hit.transform.gameObject.tag == "throwableObject")
                    {
                        PickUpObject(hit.transform.gameObject);
                    }
                }
            }
            else
            {
                if (canDrop == true)
                {
                    DropObject();
                }
            }
        }
        if (heldObjPrefab != null) 
        {
            MoveObject();
            if (Input.GetKeyDown(KeyCode.Q) && canDrop == true) 
            {
                ThrowObject();
            }

        }
    }

    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>())
        {
            heldObjPrefab = pickUpObj; 
            heldObjRb = pickUpObj.GetComponent<Rigidbody>(); 
            heldObjRb.isKinematic = true;
            heldObjRb.transform.parent = holdPos.transform; 
            heldObjPrefab.layer = LayerNumber;
            Physics.IgnoreCollision(heldObjPrefab.GetComponent<Collider>(), controller.GetComponent<Collider>(), true);
        }
    }

    void DropObject()
    {
        Physics.IgnoreCollision(heldObjPrefab.GetComponent<Collider>(), controller.GetComponent<Collider>(), false);
        heldObjPrefab.layer = 0; 
        heldObjRb.isKinematic = false;
        heldObjPrefab.transform.parent = null; 
        heldObjPrefab = null;
    }

    void MoveObject()
    {
        heldObjPrefab.transform.position = holdPos.transform.position;
    }

    void ThrowObject()
    {
        Physics.IgnoreCollision(heldObjPrefab.GetComponent<Collider>(), controller.GetComponent<Collider>(), false);
        heldObjPrefab.layer = 0;
        heldObjRb.isKinematic = false;
        heldObjPrefab.transform.parent = null;
        heldObjRb.AddForce(transform.forward * throwForce * gravity);
        heldObjPrefab = null;
    }


}




