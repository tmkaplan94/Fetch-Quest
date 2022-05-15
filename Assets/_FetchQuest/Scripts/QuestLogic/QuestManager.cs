using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Daichi m

// collator/manager for quests
// found through leveldata

public class QuestManager : MonoBehaviour
{
    // tracks active quests
    // injected by quests when activated
    // there are better data structures but also ehhhh
    private List<Quest> activeQuests = new List<Quest>();
    
    // ditto
    private List<Quest> completedQuests = new List<Quest>();

    // accessors

    public void questStarted(Quest quest)
    {
        activeQuests.Add(quest);
        print("QUEST MANAGER: QUEST ADDED: " + activeQuests);
    }

    public void questCompleted(Quest quest)
    {
        activeQuests.Remove(quest);
        completedQuests.Add(quest);
    }

    public Quest[] getActiveQuests()
    {
        return activeQuests.ToArray();
    }

    public Quest[] getCompletedQuests()
    {
        return completedQuests.ToArray();
    }
}
