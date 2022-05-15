using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Daichi m
// the fish quest

// testing new quest architectures

public class FishQuest : Quest
{
    [SerializeField] private FishQuestParticles fishQuestParticles;
    [SerializeField] public int startingFish = 10;
    
    int fishRemaining = 0;
    
    public override void Start()
    {
        base.Start();
        fishRemaining = startingFish;
        fishQuestParticles.subscribe(fishQuestDetected);
    }

    // called as subscriber to fqd
    // quest start/finish logic
    private void fishQuestDetected()
    {
        if (!completed)
        {
            fishRemaining--;
            if (!started) questStarted();
            else
            {
                questBus.update( new QuestObject(0, 
                             "Fish Collected! " + fishRemaining + " fish left!"));
            }
            if (fishRemaining <= 0) questCompleted();
        }
    }

    public override void questStarted()
    {
        base.questStarted();
        print("fish quest starting");
        questBus.update(new QuestObject(0, "Flying fish! Everywhere! Quickly, clean up the mess! "
                                        + fishRemaining + " remaining"));
    }

    public override void questCompleted()
    {
        base.questCompleted();
        questBus.update(new QuestObject(10, "Flying fish? Never seen one in my life! (...yum)"));
    }
    
}
