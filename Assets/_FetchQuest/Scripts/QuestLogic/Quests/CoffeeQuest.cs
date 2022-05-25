using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Daichi M 

// coffee quest
// Give everyone coffee

public class CoffeeQuest : Quest
{       
    // holds npcs and their caffienation
    private Dictionary<GameObject, int> _receivedCoffee = new Dictionary<GameObject, int>();

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

            Destroy(coffee.gameObject);
        }
    }
    
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
