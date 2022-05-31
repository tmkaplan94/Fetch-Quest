using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Daichi M

// communicates with the coffee quest when interacted with
public class CoffeeItem : QuestItem
{
    // calls delegate when coffee hits an npc
    // coffee quest listens
    // public delegate GameObject OnNPCTouched();
    // public OnNPCTouched onNPCTouched;

    // public void subscribe(OnNPCTouched subscriber)
    // {
    //     onNPCTouched += subscriber;
    // }

    // lazy but eh
    [SerializeField] public CoffeeQuest mainQuest;
    [SerializeField] private ParticleSystem splashParticles;
    [SerializeField] private float splashVelocity = 1f;

    private void Awake()
    {
        mainQuest = FindObjectOfType<CoffeeQuest>();
        mainQuest.coffeeSpawned(this);
    }
    void Start()
    {
        splashParticles.Play();
    }

    void Update()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body.velocity.magnitude > splashVelocity)
        {
            splash();
        }
    }

    public void splash()
    {
        splashParticles.Play();
    }
    
    public override void hitNPC(GameObject npc)
    {
        base.hitNPC(npc);
        mainQuest.onCoffeeHit(this, npc);
    }

    public override void pickedUp()
    {
        base.pickedUp();
        splash();
    }

    public override void dropped()
    {
        base.dropped();
        splash();
    }

}
