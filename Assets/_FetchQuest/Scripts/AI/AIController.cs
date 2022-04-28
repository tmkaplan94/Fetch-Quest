using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AIController : MonoBehaviour
{
    [SerializeField] private AIStats _stats;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private Transform workplace; //Workplace coords
    [SerializeField] private Animator personAnimator;
    public bool dogNearby = false; //Set Bool for dog nearby
    public bool personNearby = false; //Set Bool for person nearby
    public bool hasWorkToDo = false; //Set Bool for at workplace
    private ReffBool canPet = new ReffBool(true);
    private ReffBool isTalking = new ReffBool(false);
    private ReffBool isWorking = new ReffBool(false);
    private ScoreManager scoreManager;
    public int idelCount = 0;

    private int currentWaypoint = 0;
    private StateMachine _stateMachine;
#nullable enable
    public Transform? Target { get; private set; }
    public Transform? Work { get; private set; }
    private NavMeshAgent? navMeshAgent;

    public AIStats AIStats => _stats;
    public class ReffBool
    {
        public bool value;
        public ReffBool(bool defult)
        {
            value = defult;
        }
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        scoreManager = FindObjectOfType<ScoreManager>();
        _stateMachine = new StateMachine();
        

        var walkingState = new WalkingState(this, navMeshAgent);
        var idleState = new IdleState(this);
        var pettingState = new PettingState(this); //Setting up PettingState
        var talkingState = new TalkingState(this); //Setting up TalkingState
        var workingState = new WorkingState(this); //Setting up WorkingState

        At(idleState, walkingState, HasTarget());
        At(walkingState, idleState, ReachedDestination());
        At(pettingState, walkingState, HasTarget()); //For now only allow to transition from petting to walking
        At(talkingState, walkingState, HasTarget()); //For now only allow to transition from talking to walking
        At(workingState, walkingState, HasTarget());
        Aat(pettingState, DogNear()); //Adding petting state as an any
        Aat(talkingState, PersonNear()); //Adding talking state as an any (Bump into them at work)
        Aat(workingState, HasWork()); //Adding petting state as an any



        _stateMachine.SetState(idleState);
    }
    void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
    void Aat(IState to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition); //Shorthand for AddAnyTransition
    

    Func<bool> HasTarget() => () => Target != null;
    Func<bool> HasWork() => () => isWorking.value == true;
    Func<bool> DogNear() => () => dogNearby == true; //Is dog near?
    Func<bool> PersonNear() => () => personNearby == true; //Is there a person near?
    Func<bool> ReachedDestination() => () => Target != null && Vector3.Distance(transform.position, Target.position) < 1f;



    // Here is where the code is actually ran! Update function calls State Machine's Tick() every frame.
    //State Machine's Tick() checks if it needs to change states based on the transitions set above then calls tick() on current state.
    private void Update()
    {
        _stateMachine.Tick();
        if (idelCount == 5)
        {
            hasWorkToDo = true;
            idelCount = 0;
        }
    }

    IEnumerator Cooldown(float waitTime, ReffBool boolToChange)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        boolFlip(boolToChange);
    }
    private void boolFlip(ReffBool changeBool) 
    {
        changeBool.value = !changeBool.value;
    }

    public void SetTarget(Transform t)
    {
        Target = t;
        
    }

    public void GetNewTarget()
    {
        
        if (currentWaypoint >= waypoints.Length -1)
            currentWaypoint = 0;
        else
            currentWaypoint++;

        Debug.Log("getting " + currentWaypoint);

        if (hasWorkToDo)
        {
            SetTarget(workplace);
        }
        else
            SetTarget(waypoints[currentWaypoint]);
    }
    
    public void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning(canPet.value);
        Debug.LogWarning(other.gameObject.tag);
        //navMeshAgent.transform.LookAt(other.transform); //Look At Object (Whatever it is)
        if (canPet.value && other.CompareTag("Player"))
        {
            Debug.Log("booty booty ");

            canPet.value = false;
            dogNearby = true;
            
            StartCoroutine(Cooldown(_stats.PettingCooldown, canPet));
           
        }
        if(!isTalking.value && other.CompareTag("AI"))
        {
            isTalking.value = true;
            personNearby = true;
            StartCoroutine(Cooldown(_stats.TalkingCooldown, isTalking));
        }
        if(!isWorking.value && other.CompareTag("Workplace"))
        {
            isWorking.value = true;
            hasWorkToDo = false;
            StartCoroutine(Cooldown(_stats.WorkingCooldown, isWorking));
        }
    }

    public void AnimationStart(float _sp)
    {
        scoreManager.IncrementScore(1);
        personAnimator.SetFloat("Speed", _sp);
        personAnimator.SetFloat("Forward", -0.5f);
    }

    public void AnimationStop()
    {
        personAnimator.SetFloat("Speed", 1);
        personAnimator.SetFloat("Forward", 0f);
    }
}
