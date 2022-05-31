using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeeColldier : MonoBehaviour
{
    [SerializeField] private AIController c;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Pee"))
        {
            c.TouchedPee(other);
        }
    }
}
