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
using Photon.Pun;

public class PissHandler : MonoBehaviour
{
    public int pissThreshold = 45;
    public Transform pissPoint = null;
    public Transform pissSpotOnFloor = null;
    public GameObject streamPrefab = null;

    [SerializeField] private Animator _anime;
    [SerializeField] private GameObject pissPuddle;
    private ReffBool isPissing;
    private PissScript currPiss = null;
    private bool isNetworked;

    private void Awake()
    {

        if (FindObjectOfType<NetworkManager>() != null)
        {
            isNetworked = true;
        }
        else
            isNetworked = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*bool pissCheck = CalcAngle() < pissThreshold;

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
        }*/
    }
    public void StartPiss()
    {
        if(isNetworked)
        {
            PhotonView v = GetComponent<PhotonView>();
            v.RPC("StartPissRPC", RpcTarget.All);
        }
        else
        {
            StartPissRPC();
        }
    }
    [PunRPC]
    public void StartPissRPC()
    {
        //PhotonView v = GetComponent<PhotonView>();
        //if (v.IsMine)
        //{
            _anime.SetBool("isPissing", true);
            print("start");
            currPiss = CreatePiss();
            currPiss.Begin();
        //}
    }
    public void EndPiss(ReffBool pp)
    {
        isPissing = pp;
        if (isNetworked)
        {
            PhotonView v = GetComponent<PhotonView>();
            v.RPC("EndPissRPC", RpcTarget.All);
        }
        else
        {
            EndPissRPC();
        }
    }
    [PunRPC]
    public void EndPissRPC()
    {
        PhotonView v = GetComponent<PhotonView>();
        if (v.IsMine)
        {
            _anime.SetBool("isPissing", false);
            print("end");
            currPiss.End(isPissing);
            currPiss = null;    
        }
    }

    private float CalcAngle()
    {
        float currF = transform.forward.y * Mathf.Rad2Deg;
        return currF;
    }

    
    private PissScript CreatePiss()
    {
        GameObject streamObject;
        /*if (isNetworked)
        {
            streamObject = PhotonNetwork.Instantiate("Stream", pissPoint.position, Quaternion.identity);
            streamObject.transform.parent = transform;
        }
        else*/
            streamObject = Instantiate(streamPrefab, pissPoint.position, Quaternion.identity, transform);
        return streamObject.GetComponent<PissScript>();
    }


    public void CreatePuddle()
    {
        if (isNetworked)
            PhotonNetwork.Instantiate("PissPuddle", pissSpotOnFloor.position, Quaternion.Euler(new Vector3(90, 0, Random.Range(0, 180))));
        else
            Instantiate(pissPuddle, pissSpotOnFloor.position, Quaternion.Euler(new Vector3(90, 0, Random.Range(0, 180))));
    }
    
}
