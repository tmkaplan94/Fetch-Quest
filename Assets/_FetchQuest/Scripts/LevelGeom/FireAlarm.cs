using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAlarm : MonoBehaviour, Interactable
{
    [SerializeField] private float alarmLength;
    private QuestBus eventSys;
    private bool active;
    
    void Start()
    {
        eventSys = LevelStatic.currentLevel.questBus;
    }
    public void Interact(GameObject interacter)
    {
        Debug.Log("Firealarm");
        if (!active)
        {
            eventSys.update(new QuestObject(20, "Started a Fire!", LevelData.publicEvents.FIREALARM));
            AudioManager.Instance.PlaySFX(AudioNames.FireAlarm, transform.position);
            active = true;
            StartCoroutine("FireAlarmTime");
        }
        else
        {
            
        }
    }
    private IEnumerator FireAlarmTime()
    {
        yield return new WaitForSecondsRealtime(alarmLength);
        eventSys.update(new QuestObject(0, "", LevelData.publicEvents.FIREALARM));
        active = false;
    }
}
