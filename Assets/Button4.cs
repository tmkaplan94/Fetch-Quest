using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button4 : MonoBehaviour, Interactable
{
    [SerializeField] private ZombieCloning z;

    public void Interact(GameObject actor)
    {
        z.ResetTube();
    }

}
