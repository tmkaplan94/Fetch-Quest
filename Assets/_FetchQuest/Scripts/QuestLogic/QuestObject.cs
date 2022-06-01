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
                       string questName = null, bool display = false, string rarity = null)
    {
        this.pointsAwarded = points;
        this.message = message;
        this.eventEnum = eventEnum;
        this.questName = questName;
        this.rarity = rarity;
        this.display = display;
    }
    
    public int pointsAwarded = 0;
    public string message = "Quest Completed!";
    public LevelData.publicEvents eventEnum = 0;
    public string questName = null;
    public bool display;
    public string rarity;


}


