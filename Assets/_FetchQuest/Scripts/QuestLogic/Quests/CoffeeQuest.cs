using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
// Daichi M 

// coffee quest
// Give everyone coffee

public class CoffeeQuest : Quest
{
    // holds npcs and their caffienation
    private Dictionary<int, int> _receivedCoffee = new Dictionary<int, int>();
    private bool _isNetworked;
    [SerializeField] private PhotonView v;
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

    public void onCoffeeHit(CoffeeItem coffee, GameObject npc)
    {
        if (_receivedCoffee.ContainsKey(npc.GetComponent<PhotonView>().ViewID))
        {
            if (_isNetworked)
                v.RPC("AlreadyHasCoffee", RpcTarget.All);
            else
                AlreadyHasCoffee();
            
        }
        else
        {
            if (_isNetworked)
            {
                v.RPC("CoffeeHitRPC", RpcTarget.All, npc.GetComponent<PhotonView>().ViewID);
                v.RPC("DestroyCoffee", RpcTarget.All, coffee.gameObject.GetComponent<PhotonView>().ViewID);
            }
            else
            {
                CoffeeHitRPC(npc.GetComponent<PhotonView>().ViewID);
                Destroy(coffee.gameObject);
            }
        }
    }
    [PunRPC]
    private void CoffeeHitRPC(int id)
    {
        _receivedCoffee.Add(id, 1);
        QuestObject update = new QuestObject(reward, "Coffee! Thanks doggerino!");
        questBus.update(update);
    }
    [PunRPC]
    private void AlreadyHasCoffee()
    {
        QuestObject update = new QuestObject(0, "Thanks pupper, but I've already got one!");
        questBus.update(update);
    }

    [PunRPC]
    private void DestroyCoffee(int id)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(PhotonView.Find(id));
        }
    }

    public void coffeeSpawned(CoffeeItem coffee)
    {

    }

    public override void Start()
    {
        base.Start();
    }
}
