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
    private Dictionary<GameObject, int> _receivedCoffee = new Dictionary<GameObject, int>();
    private CoffeeItem currentCoffee;
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
        if (_receivedCoffee.ContainsKey(npc))
        {
            //_receivedCoffee[npc] ++;
            // TODO coffee stacks?
            QuestObject update = new QuestObject(0, "Thanks pupper, but I've already got one!");
            questBus.update(update);
        }
        else
        {
            _receivedCoffee.Add(npc, 1);
            QuestObject update = new QuestObject(reward, "Coffee! Thanks doggerino!");
            questBus.update(update);
            currentCoffee = coffee;
            currentCoffee.splash();
            if (!_isNetworked)
                Destroy(currentCoffee.gameObject);
            else
            {
                v.RPC("DestroyCoffee", RpcTarget.All, coffee.gameObject.GetComponent<PhotonView>().ViewID);
            }
        }
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
