using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CoffeeSpawner : MonoBehaviour, Interactable
{
    [SerializeField] public GameObject coffeePrefab;
    [SerializeField] public CoffeeQuest coffeeQuest;

    private PhotonView v;
    private bool _isNetworked;
    private Vector3 spawnPos;

    private void Awake()
    {
        if (FindObjectOfType<NetworkManager>() == null)
        {
            _isNetworked = false;
        }
        else
        {
            _isNetworked = true;
        }
        v = GetComponent<PhotonView>();
        
    }

    public void Interact(GameObject actor)
    {
        spawnCoffee();
    }

    private void spawnCoffee()
    {
        AudioManager.Instance.PlaySFX(AudioNames.click, transform.position);
        spawnPos = gameObject.transform.position;
        spawnPos += new Vector3(0, 0.6f, 0);
        if (!_isNetworked)
            Instantiate(coffeePrefab, spawnPos, Quaternion.identity);
        else
        {
            v.RPC("SpawnCoffeeRPC", RpcTarget.All, spawnPos);
        }
    }

    [PunRPC]
    private void SpawnCoffeeRPC(Vector3 pos)
    {
        if(PhotonNetwork.IsMasterClient)
            PhotonNetwork.Instantiate("ScriptedCoffee", pos, Quaternion.identity);
    }
}
