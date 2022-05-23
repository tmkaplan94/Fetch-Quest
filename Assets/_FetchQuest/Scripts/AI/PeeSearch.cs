using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PeeSearch : IState
{
    private readonly AIController _controller;
    private readonly NavMeshAgent _navMeshAgent;
    public PeeSearch(AIController c, NavMeshAgent n)
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
        _navMeshAgent.SetDestination(_controller.peeObj.transform.position);
        _controller.AnimationWalking();
    }

    public void OnExit()
    {
        _controller.SetTarget(null);
        _navMeshAgent.enabled = false;
        _controller.AnimationStop();

    }
}