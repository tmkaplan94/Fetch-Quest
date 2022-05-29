using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Interect : MonoBehaviour
{
    [SerializeField] private Transform interactPos;
    [SerializeField] private Vector3 _interactBox;
    [SerializeField] private LayerMask _interactLayer;
    private string EventObjectTag = "EventObj";
    private PickUpSystem _pickupSystem;
    GameObject heldItem;
    private bool isNetworked;
    private PhotonView v;


    private void Awake()
    {
        v = GetComponent<PhotonView>();
        if (FindObjectOfType<NetworkManager>() == null)
            isNetworked = false;
        else
            isNetworked = true;

    }
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
            AudioManager.Instance.PlaySFX("General_Bark", transform.position);
            GameObject heldItem = _pickupSystem.GetItem();
            if (heldItem != null)   
            {
                if(heldItem.TryGetComponent(out Interactable interactable))
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
