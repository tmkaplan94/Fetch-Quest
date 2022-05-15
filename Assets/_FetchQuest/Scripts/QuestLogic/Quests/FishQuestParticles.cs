using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Daichi M
// unity forum user Josiah_Kunz
// https://answers.unity.com/questions/1438477/finding-particle-information-upon-collision.html

// connects to the fish quest
// handles fish particles side of things
// placed on the fish itself to detect fishn't
// notifies fish quest by delegate (because ocd)

public class FishQuestParticles : MonoBehaviour
{
    # region COMMUNICATION
    
    public delegate void OnFishQuestDetected();
    private OnFishQuestDetected onFishQuestDetected;
    public void subscribe(OnFishQuestDetected subscriber)
    {
        onFishQuestDetected += subscriber;
    }

    # endregion

    [SerializeField] private ParticleSystem fishParticles;
    [SerializeField] private ParticleSystem crumbParticles;

    private void OnParticleCollision(GameObject other)
    {
        // TODO do this properly
        if (other.GetComponent<PlayerMovement>())
        {
            Vector3 fishPos = deleteCollidingParticles(other);
            onFishQuestDetected();
            crumbParticles.transform.position = fishPos;
            crumbParticles.Play();
        }
    }

    // returns position of deleted particle for crumb spawning
    // hacky but I think best
    private Vector3 deleteCollidingParticles(GameObject target)
    {
        // following code shamelessly recreated from unity forums post:
        // https://answers.unity.com/questions/1438477/finding-particle-information-upon-collision.html

        Vector3 particlePos = Vector3.zero;
        
        // get particle array
        ParticleSystem.Particle[] allParticles;
        allParticles = new ParticleSystem.Particle[fishParticles.particleCount];
        fishParticles.GetParticles(allParticles);
        
        // find colliding particles
        Bounds particleBounds;
        for(int i = 0; i < allParticles.Length; i++)
        {
            foreach(Collider collider in target.GetComponentsInChildren<Collider>())
            {
                particleBounds = new Bounds(allParticles[i].position,
                                            allParticles[i].GetCurrentSize3D(fishParticles));
                if (collider.bounds.Intersects(particleBounds))
                {
                    particlePos = allParticles[i].position;
                    allParticles[i].remainingLifetime = -1;
                    break;
                }
            }
         }

         // build particles from updated array
         fishParticles.SetParticles(allParticles);
         return particlePos;
    }

    public void setNumberOfFish(int fish)
    {
        var main = fishParticles.main;
        main.maxParticles = fish;
    }
}
