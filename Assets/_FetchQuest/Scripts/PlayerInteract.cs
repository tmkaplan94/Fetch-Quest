using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float _interactRange;
    private string interactableTag = "Interactable";
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            Interactable inter;
            Physics.BoxCast(transform.position, new Vector3(1, 2, 1), transform.forward, out hit, Quaternion.identity, _interactRange);
            if (hit.collider.CompareTag(interactableTag))
            {
                hit.collider.gameObject.GetComponent<Interactable>().Interact(this.gameObject);
            }
        }
    }
}
