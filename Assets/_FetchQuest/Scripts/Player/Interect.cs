using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Interect : MonoBehaviour
{
    private PickUpSystem _pickupSystem;
    private bool isNetworked;
    GameObject heldItem;
    PhotonView v;

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
    }

    void Update()
    {
        if (Input.GetButtonDown("f"))
        {
            AudioManager.Instance.PlaySFX("General_Bark", transform.position);
            heldItem = _pickupSystem.GetItem();
            if (heldItem != null)
            {
                if (heldItem.TryGetComponent(out Interactable interactable))
                    interactable.Interact(this.gameObject);
                else
                {
                    if (isNetworked)
                        v.RPC("EatRPC", RpcTarget.All);
                    else
                        Eat();
                }
            }
        }
    }

    [PunRPC]
    private void EatRPC()
    {
        Eat();
    }
    private void Eat()
    {
        LevelStatic.currentLevel.questBus.update(new QuestObject(5, "MMM.. Delicious! I love " + heldItem.name + "!!", LevelData.publicEvents.NOEVENT));
        Destroy(heldItem);
    }
}
