using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EvacuationState : IState
{
    private readonly AIController _controller;
    private readonly NavMeshAgent _navMeshAgent;
    private float waitTime;

    public EvacuationState(AIController c, NavMeshAgent n)
    {
        _controller = c;
        _navMeshAgent = n;
    }

    public void Tick()
    {
        if (waitTime <= Time.time)
        {
            waitTime += Time.deltaTime;
            if (_controller.turnAlarmOff == true) { waitTime = 0; }
        }
    }


    public void OnEnter()
    {
        Debug.Log("Evacuation Enter");
        waitTime = Time.time + _controller.AIStats.RestTime * 3;
        _navMeshAgent.enabled = true;
        _navMeshAgent.SetDestination(_controller.exit.position);
        _controller.AnimationWalking();
    }

    public void OnExit()
    {
        Debug.Log("Evacuation Exit");
        _navMeshAgent.enabled = false;
        _controller.fireAlarm = false;
        _controller.turnAlarmOff = false;
        _controller.GetNewTarget();
        _controller.AnimationStop();
    }
}
