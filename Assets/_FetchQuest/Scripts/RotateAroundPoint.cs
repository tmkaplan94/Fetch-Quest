using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundPoint : MonoBehaviour
{

    private float rotationSpeed = 80;
    public GameObject pivotObject;
    
    public bool rotating = true; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(rotating){
            transform.RotateAround(pivotObject.transform.position, new Vector3(0, 1, 0), rotationSpeed * Time.deltaTime);
            StartCoroutine(rotateTimer());
        }
     
    }

    private IEnumerator rotateTimer(){
        yield return new WaitForSeconds(1);
        Debug.Log("Wait 5 seconds");
        rotating = false; 
    }
}
