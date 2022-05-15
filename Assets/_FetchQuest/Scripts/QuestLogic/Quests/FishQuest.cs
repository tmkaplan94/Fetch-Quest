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

        // set particle number
        fishQuestParticles.setNumberOfFish(startingFish);
    }

    // called as subscriber to fish particles
    // quest lifetime logic
    private void fishQuestDetected()
    {
        if (!completed)
        {
            fishRemaining--;
            if (!started) questStarted();
            else
            {
                string message = "Fish Collected! " + fishRemaining + " fish left!";
                questBus.update( new QuestObject(0, message));
                fishQuestParticles.setNumberOfFish(fishRemaining);
            }
            if (fishRemaining <= 0) questCompleted();
        }
    }

    public override void questStarted()
    {
        base.questStarted();
        string message = "Flying fish! Everywhere! Quickly, clean up the mess! "
                                        + fishRemaining + " remaining";
        questBus.update(new QuestObject(0, message, LevelData.publicEvents.QUESTSTARTED, questName));
    }

    public override void questCompleted()
    {
        base.questCompleted();
        string message = "Flying fish? Never seen one in my life! (...yum)";
        questBus.update(new QuestObject(10, message, LevelData.publicEvents.QUESTFINISHED, questName));
    }
    
}
