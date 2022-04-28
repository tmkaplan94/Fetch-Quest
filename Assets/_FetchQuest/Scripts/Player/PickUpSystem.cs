using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField] private GameObject currentItem = null;
    private bool _touchedBone = false;
   
    [SerializeField] private Transform bonePos;
    [SerializeField] private Transform dropPos;
    [SerializeField] private Vector3 pickUpBox;
    [SerializeField] private LayerMask pickUpsLayer;

    private string interactableTag = "Interactable";

    [SerializeField] private bool __DebugbyNat;

    private void Update()
    {
         if (Input.GetKeyDown(KeyCode.E))
         {
            PickUp();
         }
    }

    private void PickUp()
    {
        Collider[] items = Physics.OverlapBox(dropPos.position, pickUpBox, dropPos.rotation, pickUpsLayer);
        if (items.Length > 0)
        {
            foreach (Collider item in items)
            {
                if (item.gameObject.CompareTag(interactableTag))
                {
                    Drop();
                    currentItem = item.gameObject;
                    BoxCollider[] cols = currentItem.GetComponentsInChildren<BoxCollider>();
                    foreach (BoxCollider col in cols)
                    {
                        col.enabled = false;
                    }
                    currentItem.GetComponent<Rigidbody>().isKinematic = true;
                    currentItem.transform.parent = bonePos;
                    currentItem.transform.position = bonePos.position;
                    currentItem.transform.rotation = bonePos.rotation;
                }
            }
        }
        else
        {
            Drop();
        }
    }

    private void Drop()
    {
        if (currentItem != null)
        {
            currentItem.transform.parent = null;
            currentItem.transform.position = dropPos.position;
            currentItem.transform.rotation = dropPos.rotation;
            BoxCollider[] cols = currentItem.GetComponentsInChildren<BoxCollider>();
            foreach (BoxCollider col in cols)
            {
                col.enabled = true;
            }
            currentItem.GetComponent<Rigidbody>().isKinematic = false;
            currentItem = null;
        }
    }

}

