using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FiredState : IState
{
    private readonly AIController _controller;
    private readonly NavMeshAgent _navMeshAgent;
    private float waitTime;

    private Vector3 _lastPosition = Vector3.zero;
    public float TimeStuck;

    public FiredState(AIController c, NavMeshAgent n)
    {
        _controller = c;
        _navMeshAgent = n;
    }

    public void Tick()
    {
        if (waitTime <= Time.time)
        {
            _controller.CallDestroy(_controller.gameObject);//Destroy(this); (This does not work, I need to call _controller, can fix later after flushing out a few things)
        }
    }


    public void OnEnter()
    {
        Debug.Log("Fired Janitor Start");
        waitTime = Time.time + _controller.AIStats.RestTime * 2;
        _navMeshAgent.enabled = true;
        _navMeshAgent.SetDestination(_controller.exit.position);
    }

    public void OnExit()
    {
        Debug.Log("Fired");
    }
}
