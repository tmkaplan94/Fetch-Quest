using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Daichi M
// give to things that want to interact with the quest system

public class QuestComponent : MonoBehaviour
{
    // is it right to directly reference the bus?
    [SerializeField]
    QuestBus questBus;
    
    // call when ie. quest completed
    public void updateQuest(int points = 1, string message = "quest complete!!!")
    {
        QuestObject quest = new QuestObject(points, message);
        questBus.update(quest);
    }

    //

}
