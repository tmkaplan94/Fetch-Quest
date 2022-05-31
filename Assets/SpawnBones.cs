using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SpawnBones : MonoBehaviour
{

    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject _bone;
    private bool _isNetworked;

    #region Instance
    private static SpawnBones instance;
    public static SpawnBones Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SpawnBones>();
                if (instance == null)
                {
                    instance = new GameObject("Spawned BoneSpawner", typeof(SpawnBones)).GetComponent<SpawnBones>();
                }
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    #endregion

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

    public void SpawnNewBone()
    {
        int i = Random.Range(0, _spawnPoints.Length);
        if (!_isNetworked)
            Instantiate(_bone, _spawnPoints[i]);
        else
            PhotonNetwork.Instantiate("Bone", _spawnPoints[i].position, Quaternion.identity);
    }

}
