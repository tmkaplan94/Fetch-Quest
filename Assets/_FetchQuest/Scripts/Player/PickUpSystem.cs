using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/*
 * Author: Grant Reed
 * Contributors: 
 * Summary: Allows dog to pickup items
 *
 * Description:
 * Updates
 * - N/A
 */

public class PickUpSystem : MonoBehaviourPun
{

    [SerializeField] private GameObject currentItem = null;
   
    [SerializeField] private Transform holdPos;
    [SerializeField] private Transform dropPos;
    [SerializeField] private Transform interactPos;
    [SerializeField] private Vector3 pickUpBox;
    [SerializeField] private LayerMask pickUpsLayer;
    private string EventObjectTag = "EventObj";

    private string interactableTag = "Interactable";
    private bool isNetworked;
    
    private float maxMass;
    private void Awake()
    {
        if (FindObjectOfType<NetworkManager>() == null)
        {
            isNetworked = false;
        }
        else
            isNetworked = true;
    }
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
            {
                if (isNetworked)
                    PickUp();
                else
                    PickUpRPC();
            }
            else
            {
                if (isNetworked)
                    Drop();
                else
                    DropRPC();
            }
         }
    }
    public GameObject GetItem()
    {
        return currentItem;
    }

    private void PickUp()
    {
        photonView.RPC("PickUpRPC", RpcTarget.All);
    }
    private void Drop()
    {
        photonView.RPC("DropRPC", RpcTarget.All);
    }
    
    [PunRPC]
    private void PickUpRPC()
    {
        Collider[] items = Physics.OverlapBox(interactPos.position, pickUpBox, interactPos.rotation, pickUpsLayer );
        if (items.Length > 0)
        {
            foreach (Collider item in items)
            {
                if (item.attachedRigidbody.mass <= maxMass)
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
                if (item.gameObject.CompareTag(EventObjectTag))
                {
                    item.gameObject.GetComponent<Interactable>().Interact(this.gameObject);
                     break;
                }
            }
        }
        
    }

    [PunRPC]
    private void DropRPC()
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

