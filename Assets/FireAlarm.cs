using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAlarm : MonoBehaviour
{
    private QuestBus eventSys;
    void Start()
    {
        eventSys = LevelStatic.currentLevel.questBus;
    }

    public void Interact()
    {
        eventSys.update(new QuestObject(20, "Started a Fire!", LevelData.publicEvents.FIREALARM));
    }
}
