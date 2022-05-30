using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Daichi M
// packet of quest information passed into and out of quest bus
// glorified struct for now

public class QuestObject 
{
    // constructor
    public QuestObject(int points, string message, LevelData.publicEvents eventEnum = 0, 
                        string questName = null)
    {
        this.pointsAwarded = points;
        this.message = message;
        this.eventEnum = eventEnum;
        this.questName = questName;
    }
    
    public int pointsAwarded = 0;
    public string message = "Quest Completed!";
    public LevelData.publicEvents eventEnum = 0;
    public string questName = null;


}


