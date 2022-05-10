using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Daichi M
// Hub for quest and score management


public class QuestBus : MonoBehaviour
{

    #region PUBLIC METHODS

    // ===== PUBLIC API =====

    // public singleton
    public static QuestBus QUEST_BUS;

    // subscribe to this to receive updates when quests are updated
    // ie. questBus.questUpdatedDelegate += listenerFunc;
    public delegate void QuestUpdatedDelegate(QuestObject quest);
    public QuestUpdatedDelegate questUpdatedDelegate; 

    // call to update all subscribers
    public void update(QuestObject quest)
    {
        publish(quest);
    }

    // test
    public void subscribe(QuestUpdatedDelegate subscriber)
    {
        questUpdatedDelegate += subscriber;
    }

    #endregion


    # region HELPERS AND MONODEVELOP


    // ===== HELPERS =====

    private void publish(QuestObject quest)
    {
        // publishes to delegate
        questUpdatedDelegate(quest);
    }


    // ===== unity callbacks =====
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion
}
