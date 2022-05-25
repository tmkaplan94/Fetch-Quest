using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Daichi M

// inheritance base for quest items
// holds functions called by AI and pickup script

public class QuestItem : MonoBehaviour
{
    // called by AI ontriggerenter
    public virtual void hitNPC(GameObject npc)
    {

    } 

    // called when picked up by dog
    public virtual void pickedUp()
    {
        
    }

    // called when dropped
    public virtual void dropped()
    {
        
    }
}
