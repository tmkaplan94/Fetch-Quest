using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button1 : MonoBehaviour, Interactable
{

    [SerializeField] ZombieCloning cloner;
    public void Interact(GameObject interactor)
    {
        cloner.Clone();
    }
}
