using System;
using Photon.Pun;
using UnityEngine;

public class ChooseDogPrefab : MonoBehaviour
{
    [SerializeField] private GameObject[] dogPrefab;
    private int dogIndex;
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

    void Start()
    {
        // I know this are a little overkill, but Rider was bugging me...
        Transform dogTransform = transform;
        Vector3 dogPosition = dogTransform.position;
        Quaternion dogRotation = dogTransform.rotation;
        
        dogIndex = RotationManager._currentDogIndex;

        if (_isNetworked)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate(dogPrefab[dogIndex].name, dogPosition + new Vector3(0, 0, -2), dogRotation);
            }
            else
            {
                PhotonNetwork.Instantiate(dogPrefab[dogIndex].name, dogPosition + new Vector3(0, 0, 2), dogRotation);
            }
        }
        else
        {
            Instantiate(dogPrefab[dogIndex], dogPosition, dogRotation);
        }
    }
    
}
