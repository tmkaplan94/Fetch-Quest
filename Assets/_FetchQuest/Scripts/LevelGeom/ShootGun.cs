using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ShootGun : MonoBehaviour, Interactable
{
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _lethalBullet;
    [SerializeField] private bool _lethal;
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

    public void Interact(GameObject interacter)
    {
        if (_isNetworked)
        {
            if(!_lethal)
                PhotonNetwork.Instantiate("Bullet", firePos.position, firePos.rotation);
            else
                PhotonNetwork.Instantiate("LethalBullet", firePos.position, firePos.rotation);
        }
        else
        {
            if (!_lethal)
                Instantiate(_bullet, firePos.position, firePos.rotation);
            else
                Instantiate(_lethalBullet, firePos.position, firePos.rotation);
        }
    }
}
