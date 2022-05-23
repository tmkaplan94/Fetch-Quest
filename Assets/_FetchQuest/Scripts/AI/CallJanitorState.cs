using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CallJanitorState : IState
{
    private readonly AIController _controller;
    private readonly NavMeshAgent _navMeshAgent;
    private float waitTime;
    public CallJanitorState(AIController c, NavMeshAgent n)
    {
        _controller = c;
        _navMeshAgent = n;
    }

    public void Tick()
    {
        if(_controller.peeFound)
            _navMeshAgent.SetDestination(_controller.GetJanitorPos());
        else
            _controller.GetNewTarget();
    }
    public void OnEnter()
    {
        _navMeshAgent.enabled = true;
        _controller.AnimationWalking();
    }

    public void OnExit()
    {
        _controller.peeFound = false;
        _navMeshAgent.enabled = false;
        _controller.AnimationStop();
    }
}
