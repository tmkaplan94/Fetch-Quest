using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button3 : MonoBehaviour, Interactable
{
    [SerializeField] ZombieCloning zc;
    public void Interact(GameObject actor)
    {
        zc.Activate();
    }
}
