using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkingState :  IState
{
    private readonly AIController _controller;
    private readonly NavMeshAgent _navMeshAgent;
    

    private Vector3 _lastPosition = Vector3.zero;
    public float TimeStuck;

    public WalkingState(AIController c, NavMeshAgent n)
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
        _navMeshAgent.SetDestination(_controller.Target.transform.position);
    }

    public void OnExit()
    {
        _controller.SetTarget(null);
        _navMeshAgent.enabled = false;
    }
}
