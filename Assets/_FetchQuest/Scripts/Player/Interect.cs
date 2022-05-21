using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interect : MonoBehaviour
{
    [SerializeField] private Transform interactPos;
    [SerializeField] private Vector3 _interactBox;
    [SerializeField] private LayerMask _interactLayer;
    private string EventObjectTag = "EventObj";
    private PickUpSystem _pickupSystem;

    // Start is called before the first frame update
    void Start()
    {
        _pickupSystem = GetComponent<PickUpSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("f"))
        {
            Interact();
            AudioManager.Instance.PlaySFX("General_Bark", transform.position);
            GameObject heldItem = _pickupSystem.GetItem();
            if (heldItem != null)   
            {
                if(heldItem.TryGetComponent(out Interactable interactable))
                    interactable.Interact(this.gameObject);
                
            }
        }
    }
    private void Interact()
    {
        Collider[] items = Physics.OverlapBox(interactPos.position, _interactBox, interactPos.rotation, _interactLayer);
        if (items.Length > 0)
        {
            foreach (Collider item in items)
            {
                if (item.gameObject.CompareTag(EventObjectTag))
                {
                    item.gameObject.GetComponent<Interactable>().Interact(this.gameObject);
                }
            }
        }
    }
}
