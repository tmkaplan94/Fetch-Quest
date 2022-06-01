using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GotOut : MonoBehaviour
{
    bool isNetworked = false;
    PhotonView v;
    private void Awake()
    {
        if (FindObjectOfType<NetworkManager>() == null)
            isNetworked = false;
        else
        {
            v = GetComponent<PhotonView>();
            isNetworked = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(ComparePlayerTag(other.tag))
        {
            LevelStatic.currentLevel.questBus.update(new QuestObject(-99999999, "The great outdoors!", LevelData.publicEvents.NOEVENT, "", true, "Super Rare!"));
        }
    }

    bool ComparePlayerTag(string tag)
    {
        return tag == "small" || tag == "big" || tag == "medium";
    }
}
