using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    public Transform cam;
    public Transform holdPos;
    public LayerMask itemMask;
    public float throwForce = 500f; 
    public float pickUpRange = 5f;
    private GameObject heldObj;
    private Rigidbody heldObjRb; 
    private bool canDrop = true;
    private int LayerNumber;

    
    void Start()
    {
        LayerNumber = LayerMask.NameToLayer("Hold"); 

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if (heldObj == null) 
            {
                
                RaycastHit hit;
                if (Physics.Raycast(cam.position, cam.forward, out hit, pickUpRange))
                {
                    
                    if ((itemMask & (1 << hit.transform.gameObject.layer)) != 0)
                    {
                        PickUpObject(hit.transform.gameObject);
                    }
                }
            }
            else
            {
                if (canDrop == true)
                {
                    StopClipping(); 
                    DropObject();
                }
            }
        }
        if (heldObj != null) 
        {
            MoveObject(); 
            if (Input.GetKeyDown(KeyCode.Mouse0) && canDrop == true) 
            {
                StopClipping();
                ThrowObject();
            }

        }
    }
    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>())
        {
            heldObj = pickUpObj;
            heldObjRb = pickUpObj.GetComponent<Rigidbody>();
            heldObjRb.isKinematic = true;
            heldObjRb.transform.parent = holdPos.transform; 
            heldObj.layer = LayerNumber;

            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), GetComponent<Collider>(), true);
        }
    }
    void DropObject()
    {

        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), GetComponent<Collider>(), false);
        heldObj.layer = 3; 
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null; 
        heldObj = null; 
    }
    void MoveObject()
    {
        heldObj.transform.position = holdPos.transform.position;
    }
    void ThrowObject()
    {
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), GetComponent<Collider>(), false);
        heldObj.layer = 3;
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObjRb.AddForce(cam.forward * throwForce);
        heldObj = null;
    }
    void StopClipping() 
    {
        var clipRange = Vector3.Distance(heldObj.transform.position, transform.position);

        RaycastHit[] hits;
        hits = Physics.RaycastAll(cam.position, cam.forward, clipRange);

        if (hits.Length > 1)
        {

            heldObj.transform.position = cam.position + new Vector3(0f, -0.5f, 0f);

        }
    }
}