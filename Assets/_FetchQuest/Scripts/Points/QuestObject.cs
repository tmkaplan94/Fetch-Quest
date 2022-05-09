using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Daichi M
// packet of quest information passed into and out of quest bus
// glorified struct for now

public class QuestObject : MonoBehaviour
{
    // placeholder constructor
    public QuestObject(int points, string message)
    {
        this.pointsAwarded = points;
        this.message = message;
    }
    
    public int pointsAwarded = 0;
    public string message = "Quest Completed!";

}
