using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interect : MonoBehaviour
{
    [SerializeField] private Transform interactPos;

    private string EventObjectTag = "EventObj";
    [SerializeField] private Vector3 pickUpBox;
    [SerializeField] private LayerMask interactLayer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Interact()
    {
        Collider[] items = Physics.OverlapBox(interactPos.position, pickUpBox, interactPos.rotation, interactLayer);
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
