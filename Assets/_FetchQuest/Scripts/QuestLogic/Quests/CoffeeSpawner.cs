using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeSpawner : MonoBehaviour, Interactable
{
    [SerializeField] public GameObject coffeePrefab;
    [SerializeField] public CoffeeQuest coffeeQuest;

    public void Interact(GameObject actor)
    {
        spawnCoffee();
    }

    private void spawnCoffee()
    {
        Vector3 pos = gameObject.transform.position;
        pos += new Vector3(0,0.6f,0);
        GameObject coffee = Instantiate(coffeePrefab, pos, Quaternion.identity);
        
        // inject quest into spawned coffees
        CoffeeItem coffeeScript = coffee.GetComponent<CoffeeItem>();
        coffeeScript.mainQuest = coffeeQuest;

        // to notify you
        // (and to ensure the editor field is set)
        coffeeQuest.coffeeSpawned(coffeeScript);
    }
}
