using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseDogPrefab : MonoBehaviour
{

    [SerializeField] private GameObject[] dogPrefab;
    private int dogIndex;

    // Start is called before the first frame update
    void Start()
    {

        dogIndex = RotationManager._currentDogIndex;
        Instantiate(dogPrefab[dogIndex], transform.position, transform.rotation);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
