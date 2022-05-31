using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Interect : MonoBehaviourPun
{
    private PickUpSystem _pickupSystem;
    private bool isNetworked;
    GameObject heldItem;
    PhotonView v;
    QuestBus eventSys;

    private void Awake()
    {
        if (FindObjectOfType<NetworkManager>() == null)
            isNetworked = false;
        else
            isNetworked = true;

        v = GetComponent<PhotonView>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _pickupSystem = GetComponent<PickUpSystem>();
        LevelData l = FindObjectOfType<LevelData>();
        eventSys = l.questBus;
    }

    void Update()
    {
        if (Input.GetButtonDown("f"))
        {
            if(isNetworked)
            {
                photonView.RPC("BarkRPC", RpcTarget.All);
            }
            else
            {
                BarkRPC();
            }
            heldItem = _pickupSystem.GetItem();
            if (heldItem != null)
            {
                string itemTag = heldItem.tag;
                if (itemTag == "Special" || itemTag == "Coffee")
                    return;
                if (heldItem.TryGetComponent(out Interactable interactable))
                    interactable.Interact(this.gameObject);
                else
                {
                    if (isNetworked)
                    {
                        v.RPC("EatRPC", RpcTarget.All);
                        if (PhotonNetwork.IsMasterClient)
                            PhotonNetwork.Destroy(heldItem);
                        else
                            v.RPC("NetworkDestroy", RpcTarget.All);
                    }
                    else
                    {
                        eventSys.update(new QuestObject(5, "MMM.. Delicious!!!", LevelData.publicEvents.NOEVENT));
                        Destroy(heldItem);
                    }
                }
            }
        }
    }

    [PunRPC]
    private void EatRPC()
    {
        eventSys.update(new QuestObject(5, "MMM.. Delicious!!!", LevelData.publicEvents.NOEVENT));        
    }
    [PunRPC]
    private void NetworkDestroy()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(_pickupSystem.GetItem());
        }
    }
    [PunRPC]
    private void BarkRPC()
    {
        AudioManager.Instance.PlaySFX("General_Bark", transform.position);
    }   
}
