using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Daichi M
// Hub for quest and score management


public class QuestBus : MonoBehaviour
{

    #region PUBLIC METHODS

    // ===== PUBLIC API =====

    // subscribe to this to receive updates when quests are updated
    // ie. questBus.questUpdatedDelegate += listenerFunc;
    public delegate void QuestUpdatedDelegate(QuestObject quest);
    public QuestUpdatedDelegate questUpdatedDelegate; 

    // call to publish quest updates
    public void update(QuestObject quest)
    {
        publish(quest);
    }


    #endregion


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
}
