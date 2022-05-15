using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Daichi M

// inheritable template for quests
// manages quest lifetimes, interacts with manager
// see FishQuest.cs for (bad) example

public class Quest : MonoBehaviour
{
    // public to open to inheritance
    [HideInInspector] public QuestBus questBus;
    [HideInInspector] public QuestManager questManager;
    [SerializeField] public string questName = "Quest";
    public bool completed = false;
    public bool started = false;
    
    public virtual void questStarted()
    {
        started = true;
        questManager.questStarted(this);
    }

    public virtual void questCompleted()
    {
        completed = true;
        questManager.questCompleted(this);
    }
    
    
    public virtual void Start()
    {
        // get questbus and manager from static
        questBus = LevelStatic.currentLevel.questBus;
        questManager = LevelStatic.currentLevel.questManager;
    }

}
