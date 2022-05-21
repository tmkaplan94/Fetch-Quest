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
   
    [SerializeField] private Transform holdPos;
    [SerializeField] private Transform dropPos;
    [SerializeField] private Transform interactPos;
    [SerializeField] private Vector3 pickUpBox;
    [SerializeField] private LayerMask pickUpsLayer;

    private string interactableTag = "Interactable";
    
    private float maxMass;
    private void Start()
    {
        switch (gameObject.tag) 
        {
            case "big": maxMass = 15; break;
            case "medium": maxMass = 10; break;
            case "small": maxMass = 5; break;
            default: maxMass = 0; break;
        }
    }

    private void Update() 
    {
         if (Input.GetKeyDown(KeyCode.E))
         {
            if (currentItem == null)
                PickUp();
            else
                Drop();
         }
    }
    public GameObject GetItem()
    {
        return currentItem;
    }
    
    private void PickUp()
    {
        Collider[] items = Physics.OverlapBox(interactPos.position, pickUpBox, interactPos.rotation, pickUpsLayer );
        if (items.Length > 0)
        {
            foreach (Collider item in items)
            {
                if (item.gameObject.CompareTag(interactableTag) && item.attachedRigidbody.mass <= maxMass)
                {
                    AudioManager.Instance.PlaySFX(AudioNames.PickUp, transform.position);
                    
                    currentItem = item.gameObject;
                    Collider[] cols = currentItem.GetComponentsInChildren<Collider>();
                    foreach (Collider col in cols)
                    {
                        col.enabled = false;
                    }
                    currentItem.GetComponent<Rigidbody>().isKinematic = true;
                    currentItem.transform.parent = holdPos;
                    currentItem.transform.position = holdPos.position;
                    currentItem.transform.rotation = holdPos.rotation;
                    break;
                }
            }
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

