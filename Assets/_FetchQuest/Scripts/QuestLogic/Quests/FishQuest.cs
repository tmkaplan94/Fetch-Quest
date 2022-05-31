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
    [SerializeField] private int reward = 50;
    [SerializeField] private int individualReward = 5;
    
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

        string message = "What's this?";
        QuestObject update = new QuestObject(individualReward, message);
        questBus.update(update);
    }

    public override void questCompleted()
    {
        base.questCompleted();

        string message = "Flying fish? Never seen one in my life! (...yum)";
        QuestObject update = new QuestObject(reward, message);
        update.rarity = rarity;
        questBus.update(update);
    }

    private void updateFish()
    {
        string message = "Fish Collected! " + fishRemaining + " fish left!";
        QuestObject update = new QuestObject(individualReward, message);

        questBus.update(update);
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
