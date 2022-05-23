using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FiringState : IState
{
    private readonly AIController _controller;
    private readonly NavMeshAgent _navMeshAgent;


    public float TimeStuck;

    public FiringState(AIController c, NavMeshAgent n)
    {
        _controller = c;
        _navMeshAgent = n;
    }

    public void Tick()
    {   if (!_controller.gotFired)
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
        _controller.bossMad = false;
        _controller.gotFired = false;
        _navMeshAgent.enabled = false;
        _controller.AnimationStop();
    }
}
