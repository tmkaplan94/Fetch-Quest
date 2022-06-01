using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button1 : Interactable
{

    [SerializeField] ZombieCloning cloner;
    public  void Interact(GameObject interactor)
    {
        cloner.Clone();
    }
}
