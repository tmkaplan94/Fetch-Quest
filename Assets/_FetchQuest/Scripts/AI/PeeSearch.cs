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
        Vector3 peePos = _controller.JanitorPeeObj[0].transform.position;
        _navMeshAgent.enabled = true;
        _navMeshAgent.SetDestination(peePos);
        _controller.AnimationWalking();
    }

    public void OnExit()
    {
        _controller.SetTarget(null);
        _navMeshAgent.enabled = false;
        _controller.AnimationStop();

    }
}