using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EvacuationState : IState
{
    private readonly AIController _controller;
    private readonly NavMeshAgent _navMeshAgent;


    public EvacuationState(AIController c, NavMeshAgent n)
    {
        _controller = c;
        _navMeshAgent = n;
    }

    public void Tick()
    {
        
    }


    public void OnEnter()
    {
        _navMeshAgent.enabled = true;
        _navMeshAgent.SetDestination(_controller.exit.position);
    }

    public void OnExit()
    {
        _controller.GetNewTarget();
        _navMeshAgent.enabled = false;
        _controller.fireAlarm = false;
        _controller.turnAlarmOff = false;
    }
}
