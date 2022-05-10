using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBones : MonoBehaviour
{

    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject _bone;

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

    public void SpawnNewBone()
    {
        int i = Random.Range(0, _spawnPoints.Length);
        Instantiate(_bone, _spawnPoints[i]);
    }

}
