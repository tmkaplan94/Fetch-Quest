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
            
            if(isNetworked){
                photonView.RPC("ClientAlarmRPC", RpcTarget.All);
                photonView.RPC("AlarmRPC", RpcTarget.All);}
            else{
                ClientAlarmRPC();
                AlarmRPC();}
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
        active = true;
        StartCoroutine("FireAlarmTime");
    }
    [PunRPC]
    private void ClientAlarmRPC()
    {
        eventSys.update(new QuestObject(20, "Started a Fire!", LevelData.publicEvents.FIREALARM));
    }
}
