using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ShootGun : MonoBehaviour, Interactable
{
    [SerializeField] private Transform firePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(GameObject interacter)
    {
        PhotonNetwork.Instantiate("Bullet", firePos.position, firePos.rotation);
    }
}
