using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EvacuationState : IState
{
    private readonly AIController _controller;
    private readonly NavMeshAgent _navMeshAgent;


    private Vector3 _lastPosition = Vector3.zero;
    public float TimeStuck;

    public EvacuationState(AIController c, NavMeshAgent n)
    {
        _controller = c;
        _navMeshAgent = n;
    }

    public void Tick()
    {
        if (Vector3.Distance(_controller.transform.position, _lastPosition) <= 0f)
            TimeStuck += Time.deltaTime;

        _lastPosition = _controller.transform.position;
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
