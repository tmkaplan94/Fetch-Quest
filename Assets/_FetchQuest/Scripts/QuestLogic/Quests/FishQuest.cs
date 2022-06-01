using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Daichi M
// the fish quest

// testing new quest architectures

public class FishQuest : Quest
{
    [SerializeField] private FishQuestParticles fishQuestParticles;
    [SerializeField] public int startingFish = 10;    
    private int fishRemaining = 0;
    
    public override void Start()
    {
        base.Start();

        fishRemaining = startingFish;
        fishQuestParticles.subscribe(fishQuestDetected);

        fishQuestParticles.setNumberOfFish(startingFish);
    }

    # region QUEST FUNCTIONS

    public override void questStarted()
    {
        base.questStarted();

        string message = "\n\nFlying fish! Everywhere! Quickly, clean up the mess!   "
                                        + fishRemaining + " remaining";
        QuestObject update = new QuestObject(0, message);
        questBus.update(update);
    }

    public override void questCompleted()
    {
        base.questCompleted();

        string message = "\n\nFlying fish? Never seen one in my life! (...yum)";
        QuestObject update = new QuestObject(reward, message, LevelData.publicEvents.NOEVENT, "", true, "Common!");
        questBus.update(update);
    }

    private void updateFish()
    {
        string message = "Fish Collected! " + fishRemaining + " fish left!";
        questBus.update( new QuestObject(0, message));
        fishQuestParticles.setNumberOfFish(fishRemaining);
    }

    # endregion
    
    // called as subscriber to fish particles
    // quest lifetime logic
    private void fishQuestDetected()
    {
        if (completed) return;

        fishRemaining--;
        
        if (!started) questStarted();
        else updateFish();
        if (fishRemaining <= 0) questCompleted();
    }
}
