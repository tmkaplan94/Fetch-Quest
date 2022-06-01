using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnComponents : MonoBehaviour
{
    [SerializeField] BoxCollider b;
    [SerializeField] Rigidbody rb;
    [SerializeField] BoxCollider idReader;

    private void OnTriggerEnter(Collider other)
    {
        if(ComparePlayerTag(other.tag))
        {
            b.enabled = true;
            idReader.enabled = true;
            rb.isKinematic = false;
        }
    }

    bool ComparePlayerTag(string tag)
    {
        return tag == "small" || tag == "big" || tag == "medium";
    }
}
