/*
 * Author: Alex
 * Contributors: wut
 * Summary: handles the piss particles
 *
 * Description
 * - it's to handle the pee pee itself
 * 
 * Updates
 * - Alex Pham 5/21 - created this piece of shit
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PissHandler : MonoBehaviour
{
    public int pissThreshold = 45;
    public Transform pissPoint = null;
    public GameObject streamPrefab = null;

    [SerializeField] private Animator _anime;
    private bool isPissing = false;
    private PissScript currPiss = null;

    // Update is called once per frame
    void Update()
    {
        bool pissCheck = CalcAngle() < pissThreshold;

        if (isPissing != pissCheck)
        {
            isPissing = pissCheck;
            if (isPissing)
            {
                StartPiss();
            }
            else
            {
                EndPiss();
            }
        }
    }

    private void StartPiss()
    {
        _anime.SetBool("isPissing", true);
        print("start");
        currPiss = CreatePiss();
        currPiss.Begin();
    }

    private void EndPiss()
    {
        _anime.SetBool("isPissing", false);
        print("end");
    }

    private float CalcAngle()
    {
        float currF = transform.forward.y * Mathf.Rad2Deg;
        return currF;
    }

    
    private PissScript CreatePiss()
    {
        GameObject streamObject = Instantiate(streamPrefab, pissPoint.position, Quaternion.identity, transform);
        return streamObject.GetComponent<PissScript>();
    }
    
}
