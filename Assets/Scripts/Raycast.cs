using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public Transform cam;
    public LayerMask layerMask;
    public Material outline;
    private GameObject currentItem = null;
    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo, 5f, layerMask))
        {
            //Debug.Log("draw outline");
            if (hitInfo.collider.gameObject != currentItem && currentItem != null) // Вызывается когда переводится курсор с одного предмета сразу на другой
            {
                stopOutline();
            }
            currentItem = hitInfo.collider.gameObject;
            List<Material> materials = new List<Material>() { currentItem.GetComponent<Renderer>().material, outline };
            currentItem.GetComponent<Renderer>().SetMaterials(materials);

        }
        else
        {
            if (currentItem != null) 
            {
                stopOutline();
            }
        }
    }
    void stopOutline()
    {
        List<Material> newMaterials = new List<Material>() { currentItem.GetComponent<Renderer>().material };
        currentItem.GetComponent<Renderer>().SetMaterials(newMaterials);
        currentItem = null;
    }
}
