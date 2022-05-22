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
    //private Stream currentStream = null;

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
        print("start");
        GameObject streamObject = Instantiate(streamPrefab, pissPoint.position, Quaternion.identity, transform);
    }

    private void EndPiss()
    {

    }

    private float CalcAngle()
    {
        return transform.forward.y * Mathf.Rad2Deg;
    }

    /*
    private Stream CreatePiss()
    {
        GameObject streamObject = Instantiate(streamPrefab, pissPoint.position, Quaternion.identity, transform);
        return streamObject.GetComponent<Stream>();
    }
    */
}
