using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationManager : MonoBehaviour
{
    #region Private Serialized Fields
    
    [SerializeField] private GameObject dogList;
    [SerializeField] private Transform lookAtCamera;
    [SerializeField] private Transform lookAtTarget;
    
    #endregion


    #region Private Fields
    
    private Vector3[] _dogPositions;
    private Vector3[] _dogRotations;
    private GameObject[] _dogs;
    private int _currentDogIndex;
    private GameObject _currentDog;
    private GameObject _previousDog;
   

    #endregion


    #region MonoBehavior Callbacks

    private void Awake()
    {
        // initialize dogs, their positions, and set the current dog
        InitializeDogsAndPositions();
        _currentDogIndex = 0;
        _currentDog = _dogs[_currentDogIndex];
    }

    private void Start()
    {
        // have the first dog look at target point on camera
        //_currentDog.transform.LookAt(lookAtTarget);
    }

    void Update(){

        //rotates the current dog in the selected position
        _currentDog.transform.Rotate(new Vector3(0, 50, 0) * Time.deltaTime);

    }

    #endregion

    #region Public Methods

    public void RotateLeft()
    {

        // rotate dog wheel left
        //Debug.Log("RotateLeft()");
        //dogList.transform.Rotate(new Vector3(0, -30, 0));
        foreach(Transform child in dogList.transform){
            child.gameObject.transform.RotateAround(dogList.transform.position, new Vector3(0,-1,0), 30);
        }
        // reset what current dog looks at
        //_currentDog.transform.LookAt(lookAtCamera);
        
        //get previous dog to reset their rotate value
        _previousDog = _currentDog;
        correctOrientationForLeftRotation(_previousDog);
        
        // change current dog
        _currentDogIndex -= 1;
        if (_currentDogIndex < 0)
        {
            _currentDogIndex = dogList.transform.childCount - 1;
        }

        _currentDog = _dogs[_currentDogIndex];

        //reset rotation data 
        

        // update new current dog's look at
        //_currentDog.transform.LookAt(lookAtTarget);

;
    }

    public void RotateRight()
    {
        // rotate dog wheel right

        //children rotate with parent
        //dogList.transform.Rotate(new Vector3(0, 30, 0));

        //children rotate around parent 
        foreach(Transform child in dogList.transform){
            child.gameObject.transform.RotateAround(dogList.transform.position, new Vector3(0,1,0), 30);
        }

        // reset what current dog looks at
        //_currentDog.transform.LookAt(lookAtCamera);
        
        //get previous dog to reset their rotate value
        //currentDog has not changed yet
        _previousDog = _currentDog;
        correctOrientationForRightRotation(_previousDog);

        // change current dog
        _currentDogIndex += 1;
        if (_currentDogIndex > dogList.transform.childCount - 1)
        {
            _currentDogIndex = 0;
        }
        _currentDog = _dogs[_currentDogIndex];

        // update new current dog's look at
        //_currentDog.transform.LookAt(lookAtTarget);

    }

    #endregion


    #region Private Methods


    //For right rotates, previous dog needs to be set to a rotateY of 30 to maintain correct orientation
    private void correctOrientationForRightRotation(GameObject previousDog){
        previousDog.transform.eulerAngles = new Vector3(0, 30, 0);
    }

    //For left rotates, previous dog needs to be set to a rotateY of -30 to maintain correct orientation
    private void correctOrientationForLeftRotation(GameObject previousDog){
        previousDog.transform.eulerAngles = new Vector3(0, -30, 0);
    }

    private void InitializeDogsAndPositions()
    {
        // get number of dogs to select and make an vector3 array
        int size = dogList.transform.childCount;
        _dogPositions = new Vector3[size];
        _dogRotations = new Vector3[size];
        _dogs = new GameObject[size];
        
        // for each dog, get/store each dog and its position
        for (int i = 0; i < size; i++)
        {
            _dogs[i] = dogList.transform.GetChild(i).gameObject;
            _dogPositions[i] = dogList.transform.GetChild(i).position;
        }

    }

    #endregion
}