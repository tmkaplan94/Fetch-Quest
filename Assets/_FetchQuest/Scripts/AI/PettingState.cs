using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PettingState : IState
{
    private readonly AIController _controller;
    //private readonly NavMeshAgent _navMeshAgent;
    //private readonly Transform _target;

    private float waitTime;

    public PettingState(AIController c)
    {
        _controller = c;
        //_target = t;
        //_navMeshAgent = n;
    }

    public void Tick()
    {
        if (waitTime <= Time.time)
        {
            _controller.GetNewTarget();
        }
    }

    public void OnEnter()
    {
        Debug.Log("petting");
        if (_controller.AIStats.PettingCooldown != 0)
        {
            //Still on cooldown, exit
        }

        waitTime = Time.time + _controller.AIStats.RestTime;
        //_navMeshAgent.enabled = true;
        //_navMeshAgent.SetDestination(_controller.Target.transform.position);

    }

    public void OnExit()
    {
        //_controller.SetTarget(null);
        //_navMeshAgent.enabled = false;
    }
}
