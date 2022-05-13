using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Grant Reed
 * Contributors: 
 * Summary: Allows dog to pickup items
 *
 * Description:
 * Updates
 * - N/A
 */

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
    public GameObject GetItem()
    {
        return currentItem;
    }
    private void PickUp()
    {
        Collider[] items = Physics.OverlapBox(dropPos.position, pickUpBox, dropPos.rotation, pickUpsLayer );
        
        if (items.Length > 0)
        {
            foreach (Collider item in items)
            {
                if (item.gameObject.CompareTag(interactableTag))
                {
                    AudioManager.Instance.PlaySFX(AudioNames.PickUp, transform.position);
                    Drop();
                    currentItem = item.gameObject;
                    Collider[] cols = currentItem.GetComponentsInChildren<Collider>();
                    foreach (Collider col in cols)
                    {
                        col.enabled = false;
                    }
                    currentItem.GetComponent<Rigidbody>().isKinematic = true;
                    currentItem.transform.parent = bonePos;
                    currentItem.transform.position = bonePos.position;
                    currentItem.transform.rotation = bonePos.rotation;
                    break;
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
            Collider[] cols = currentItem.GetComponentsInChildren<Collider>();
            foreach (Collider col in cols)
            {
                col.enabled = true;
            }
            currentItem.GetComponent<Rigidbody>().isKinematic = false;
            currentItem = null;
        }
    }

}

