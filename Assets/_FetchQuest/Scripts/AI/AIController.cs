using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using Random = UnityEngine.Random;

public class ReffBool
{
    public bool value;
    public ReffBool(bool defult)
    {
        value = defult;
    }
}
public class AIController : MonoBehaviour
{
    [SerializeField] private AIStats _stats;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private Transform workplace; //Workplace coords
    [SerializeField] private Animator personAnimator;
    [SerializeField] private Mesh zombieMesh;
    [SerializeField] private Material zombieMaterial;
    [SerializeField] private bool isClone;
    public GameObject _janitor;
    public Transform exit;
    public bool dogNearby = false; //Set Bool for dog nearby
    public bool personNearby = false; //Set Bool for person nearby
    public bool hasWorkToDo = false; //Set Bool for at workplace
    public bool fireAlarm = false; //Set Bool for fire alarm
    public bool turnAlarmOff = false; //Set Bool for fire alarm to turn off
    public bool peeFound = false; //Set Bool for calling janitor if pee is found
    public bool gotFired = false;
    public bool bossMad = false;
 
    public GameObject peeObj;
    public List< GameObject> JanitorPeeObj;
    private ReffBool canPet = new ReffBool(true);
    private ReffBool isTalking = new ReffBool(false);
    private ReffBool isWorking = new ReffBool(false);
    private ScoreManager scoreManager;
    private QuestBus eventSys;
    public int idelCount = 0;
    private int scoreInc = 1;

    private int currentWaypoint = 0;
    private StateMachine _stateMachine;
#nullable enable
    public Transform? Target { get; private set; }
    public Transform? Work { get; private set; }
    private NavMeshAgent? navMeshAgent;
    private bool isNetworked;
    private PhotonView? v;

    public AIStats AIStats => _stats;
    

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        scoreManager = FindObjectOfType<ScoreManager>();
        _stateMachine = new StateMachine();


        if (FindObjectOfType<NetworkManager>() == null)
            isNetworked = false;
        else
        {
            v = GetComponent<PhotonView>();
            isNetworked = true;
        }
        
        if(isClone)
        {
            AIController[] ais = FindObjectsOfType<AIController>();
            foreach(AIController a in ais)
            {
                if (a._stats.IsJanitor)
                    _janitor = a.gameObject;
            }
            GetTransforms();
        }
        
        var walkingState = new WalkingState(this, navMeshAgent);
        var idleState = new IdleState(this);
        var pettingState = new PettingState(this); //Setting up PettingState
        var talkingState = new TalkingState(this); //Setting up TalkingState
        var workingState = new WorkingState(this); //Setting up WorkingState
        var evacuationState = new EvacuationState(this, navMeshAgent); //Setting up the EvacuationState
        var calljanitorState = new CallJanitorState(this, navMeshAgent);
        var cleaningState = new CleaningState(this);
        var firedState = new FiredState(this, navMeshAgent);
        var firingState = new FiringState(this, navMeshAgent);
        var peeSearch = new PeeSearch(this, navMeshAgent);


        At(idleState, walkingState, HasTarget());
        At(walkingState, idleState, ReachedDestination());
        At(pettingState, walkingState, HasTarget()); //For now only allow to transition from petting to walking
        At(talkingState, walkingState, HasTarget()); //For now only allow to transition from talking to walking
        At(workingState, walkingState, HasTarget());
        At(cleaningState, walkingState, HasTarget());
        At(calljanitorState, walkingState, HasTarget());
        At(evacuationState, walkingState, AlarmOff()); //Adding way to exit evacuation state that does not trigger everytime
        At(firingState, walkingState, HasTarget()); 

        Aat(evacuationState, AlarmOn()); //Adding evcuation state as an any
        Aat(firedState, Fired());
        Aat(firingState, Firing());
        Aat(calljanitorState, FoundPeeEmp());
        Aat(cleaningState, FoundPeeJan());   
        Aat(pettingState, DogNear()); //Adding petting state as an any
        Aat(talkingState, PersonNear()); //Adding talking state as an any (Bump into them at work)
        Aat(workingState, HasWork()); //Adding petting state as an any
        Aat(peeSearch, SearchPee()); //Adding petting state as an any
        



        _stateMachine.SetState(idleState);
    }  
    
    private void GetTransforms()
    {
        GameObject[] wp = GameObject.FindGameObjectsWithTag("Waypoint");
        GameObject[] works = GameObject.FindGameObjectsWithTag("Workplace");
        exit = GameObject.FindGameObjectWithTag("Exit").transform;
        int wpAmount = Random.Range(0, wp.Length);
        waypoints = new Transform[wpAmount];
        List<int> chosenVals = new List<int>();
        for(int i = 0; i < wpAmount; i++)
        {
            int index = Random.Range(0, wp.Length);
            while (chosenVals.Contains(index))
                index = Random.Range(0, wp.Length);
            waypoints[i] = wp[index].transform;
            chosenVals.Add(index);
        }
        workplace = works[ Random.Range(0, works.Length)].transform;
        if (isNetworked && v != null)
        {
            foreach (Transform t in waypoints)
            {
                v.RPC("SetTransforms", RpcTarget.Others, t.position);
            }
            v.RPC("SetWorkplace", RpcTarget.Others, workplace.position);
        }         
    }
    [PunRPC]
    private void SetTransforms(Vector3 pos)
    {
        GameObject newTransform = new GameObject();
        newTransform.transform.position = pos;
        newTransform.tag = "Workplace";
        waypoints[waypoints.Length - 1] = newTransform.transform;
    }
    [PunRPC]
    private void SetWorkplace(Vector3 pos)
    {
        exit = GameObject.FindGameObjectWithTag("Exit").transform;
        GameObject newTransform = new GameObject();
        newTransform.transform.position = pos;
        waypoints[waypoints.Length - 1] = newTransform.transform;
    }
    void Start()
    {
        _janitor = GameObject.Find("Janitor");
        eventSys = LevelStatic.currentLevel.questBus;
        eventSys.subscribe(HandleEvents);
    }

    void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
    void Aat(IState to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition); //Shorthand for AddAnyTransition
    

    Func<bool> HasTarget() => () => Target != null;
    Func<bool> HasWork() => () => isWorking.value == true;
    Func<bool> AlarmOn() => () => fireAlarm == true;
    Func<bool> AlarmOff() => () => fireAlarm == false;
    Func<bool> DogNear() => () => dogNearby == true; //Is dog near?
    Func<bool> PersonNear() => () => personNearby == true && !(_stats.IsBoss && bossMad) && !(_stats.IsJanitor && gotFired); //Is there a person near?
    Func<bool> ReachedDestination() => () => Target != null && Vector3.Distance(transform.position, Target.position) < 1f;
    Func<bool> FoundPeeEmp() => () => peeFound == true && !_stats.IsJanitor == true;
    Func<bool> FoundPeeJan() => () => peeFound == true && _stats.IsJanitor == true;
    Func<bool> Fired() => () => _stats.IsJanitor == true && gotFired == true;
    Func<bool> Firing() => () => _stats.IsBoss == true && bossMad == true;
    Func<bool> SearchPee() => () => _stats.IsJanitor == true && JanitorPeeObj.Count > 0;



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
        if (gotFired)
        {
            dogNearby = false;
            personNearby = false;
            hasWorkToDo = false;
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
    public Vector3 GetJanitorPos()
    {
        return _janitor.transform.position;
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
    private void HandleEvents(QuestObject q)
    {
        switch (q.eventEnum)
        {
            case LevelData.publicEvents.FIREALARM:
                {
                    fireAlarm = !fireAlarm;
                }
                break;
        }
    }

    bool ComparePlayerTag(string tag) { 
        return tag == "small" || tag == "big" || tag == "medium" ;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (_stats.IsJanitor && gotFired && other.CompareTag("Exit"))
        {
            Destroy(this.gameObject);
        }

        if (ComparePlayerTag(other.gameObject.tag) && !fireAlarm)
        {
            GameObject bone = other.GetComponent<PickUpSystem>().GetItem();
            if (bone!= null && bone.CompareTag("Special") && _stats.IsBoss)
            {
                if (isNetworked && v != null)
                    v.RPC("BossMad", RpcTarget.All);
                else
                    bossMad = true;
                Destroy(bone);
            }
            if (canPet.value)
            {
                if (bone != null)
                {
                    QuestItem questItem = bone.GetComponent<QuestItem>();
                    if (questItem)
                    {
                        questItem.hitNPC(gameObject);
                        AudioManager.Instance.PlaySFX(AudioNames.ScoreUp, transform.position);
                    }
                    else
                    {
                        AudioManager.Instance.PlaySFX(AudioNames.hover, transform.position);
                        scoreManager.IncrementScore(scoreInc);
                        scoreInc = 1;
                    }
                }
                else
                {
                    AudioManager.Instance.PlaySFX(AudioNames.hover, transform.position);
                    scoreManager.IncrementScore(scoreInc);
                    scoreInc = 1;
                }
                canPet.value = false;
                dogNearby = true;

                StartCoroutine(Cooldown(_stats.PettingCooldown, canPet));

            }
        }
        
        AIController ai = other.gameObject.GetComponent<AIController>();
        if (ai != null && !_stats.IsJanitor && ai._stats.IsJanitor)
        {
            if(peeObj != null)
            { 
                if(!ai.JanitorPeeObj.Contains(this.peeObj))
                    ai.JanitorPeeObj.Add(this.peeObj);
            }
            peeFound = false;
        }
        if (ai != null && _stats.IsJanitor == true && ai._stats.IsBoss && ai.bossMad == true)
        {
            gotFired = true;
            ai.gotFired = true;
            ai.bossMad = false;
        }
        else if (!isTalking.value && ai != null && !ai._stats.IsJanitor && !fireAlarm && !hasWorkToDo)
        {
            isTalking.value = true;
            personNearby = true;
            StartCoroutine(Cooldown(_stats.TalkingCooldown, isTalking));
        }
        if(!isWorking.value && other.CompareTag("Workplace") && !fireAlarm)
        {
            isWorking.value = true;
            hasWorkToDo = false;
            StartCoroutine(Cooldown(_stats.WorkingCooldown, isWorking));
        }
        
    }
    [PunRPC]
    private void BossMad()
    {
        bossMad = true;
    }
    public void TouchedPee(Collider other)
    {
        ExpandPiss piss = other.gameObject.GetComponent<ExpandPiss>();
        if (_stats.IsJanitor || (!piss.spotted && _janitor != null))
        {
            piss.spotted = true;
            peeFound = true;
            peeObj = other.gameObject;
        }
    }

    public void CallDestroy(GameObject obj)
    {
        Debug.Log("Destroyed was Called on: " + obj);
        Destroy(obj);
    }
    public void Zombify()
    {
        if (isNetworked)
        {
            PhotonView v = GetComponent<PhotonView>();
            v.RPC("ZombifyRPC", RpcTarget.All);
        }
        else
            ZombifyRPC();
    }
    [PunRPC]
    private void ZombifyRPC()
    {
        eventSys.update(new QuestObject(50, "A DISGUISE!"));
        SkinnedMeshRenderer[] renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach(SkinnedMeshRenderer rend in renderers)
        {
            if (rend.enabled)
            {
                rend.material = zombieMaterial;
                rend.sharedMesh = zombieMesh;
            }
        }
    }

    public void AnimationStart(float _sp)
    {
        personAnimator.SetFloat("Speed", _sp);
        personAnimator.SetFloat("Forward", -0.5f);
    }

    public void AnimationStop()
    {
        personAnimator.SetFloat("Speed", 1);
        personAnimator.SetFloat("Forward", 0f);
    }

    public void AnimationWalking()
    {
        personAnimator.SetFloat("Forward", 0.5f);
    }
}
