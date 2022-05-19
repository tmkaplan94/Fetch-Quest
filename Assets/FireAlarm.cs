using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAlarm : MonoBehaviour, Interactable
{
    private QuestBus eventSys;
    private bool active;
    void Start()
    {
        eventSys = LevelStatic.currentLevel.questBus;
    }

    public void Interact(GameObject interacter)
    {
        if (!active)
        {
            eventSys.update(new QuestObject(20, "Started a Fire!", LevelData.publicEvents.FIREALARM));
            AudioManager.Instance.PlaySFX(AudioNames.FireAlarm, transform.position);
        }
    }
}
