using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeItem : QuestItem
{
    // calls delegate when coffee hits an npc
    // coffee quest listens
    // public delegate GameObject OnNPCTouched();
    // public OnNPCTouched onNPCTouched;

    // public void subscribe(OnNPCTouched subscriber)
    // {
    //     onNPCTouched += subscriber;
    // }

    // lazy but eh
    [SerializeField] public CoffeeQuest mainQuest;

    public override void hitNPC(GameObject npc)
    {
        base.hitNPC(npc);
        print("coffee hit NPC");

        mainQuest.onCoffeeHit(this, npc);
    }
}
