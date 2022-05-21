using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Daichi M
// Hub for quest and score management


public class QuestBus : MonoBehaviour
{

    #region PUBLIC METHODS

    // ===== PUBLIC API =====

    public delegate void QuestUpdatedDelegate(QuestObject quest);
    private QuestUpdatedDelegate questUpdatedDelegate; 

    // call to update all subscribers
    public void update(QuestObject quest)
    {
        publish(quest);
        Debug.Log("PUBLISHED QUEST: " + quest.message);
    }

    // call to subscribe to updates
    public void subscribe(QuestUpdatedDelegate subscriber)
    {
        questUpdatedDelegate += subscriber;
        Debug.Log("NEW SUBSCRIBER: " + subscriber);
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
