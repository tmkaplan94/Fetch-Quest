using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CoffeeSpawner : MonoBehaviour, Interactable
{
    [SerializeField] public GameObject coffeePrefab;
    [SerializeField] public CoffeeQuest coffeeQuest;

    private bool _isNetworked;

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
    }

    public void Interact(GameObject actor)
    {
        spawnCoffee();
    }

    private void spawnCoffee()
    {
        GameObject coffee;
        Vector3 pos = gameObject.transform.position;
        pos += new Vector3(0,0.6f,0);
        if (!_isNetworked)
            coffee = Instantiate(coffeePrefab, pos, Quaternion.identity);
        else
            coffee = PhotonNetwork.Instantiate("ScriptedCoffee", pos, Quaternion.identity);
    }
}
