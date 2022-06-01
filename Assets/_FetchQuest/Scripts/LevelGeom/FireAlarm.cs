using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FireAlarm : MonoBehaviourPun, Interactable
{
    [SerializeField] private float alarmLength;
    private QuestBus eventSys;
    private bool active;
    private bool isNetworked;
    private void Awake()
    {
        if (FindObjectOfType<NetworkManager>() == null)
            isNetworked = false;
        else
            isNetworked = true;
    }
    
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
            if(isNetworked){
                Debug.Log("TESTING ALARM NETWORK");
                photonView.RPC("AlarmRPC", RpcTarget.All);}
            else{
                AlarmRPC();}
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
    [PunRPC]
    private void AlarmRPC()
    {
        AudioManager.Instance.PlaySFX(AudioNames.FireAlarm, transform.position);
    }
}
