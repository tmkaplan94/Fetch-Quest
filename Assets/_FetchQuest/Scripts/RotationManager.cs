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
    private GameObject[] _dogs;
    private int _currentDogIndex;
    private GameObject _currentDog;

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

    #endregion


    #region Public Methods

    public void RotateLeft()
    {
        // rotate dog wheel left

        // reset what current dog looks at
        //_currentDog.transform.LookAt(lookAtCamera);
        
        // change current dog
        _currentDogIndex -= 1;
        if (_currentDogIndex < 0)
        {
            _currentDogIndex = dogList.transform.childCount - 1;
        }
        _currentDog = _dogs[_currentDogIndex];
        
        // update new current dog's look at
        //_currentDog.transform.LookAt(lookAtTarget);
    }

    public void RotateRight()
    {
        // rotate dog wheel right

        // reset what current dog looks at
        //_currentDog.transform.LookAt(lookAtCamera);
        
        // change current dog
        _currentDogIndex += 1;
        if (_currentDogIndex >= dogList.transform.childCount)
        {
            _currentDogIndex = 0;
        }
        _currentDog = _dogs[_currentDogIndex];
        
        // update new current dog's look at
        //_currentDog.transform.LookAt(lookAtTarget);
    }

    #endregion


    #region Private Methods

    private void InitializeDogsAndPositions()
    {
        // get number of dogs to select and make an vector3 array
        int size = dogList.transform.childCount;
        _dogPositions = new Vector3[size];
        _dogs = new GameObject[size];

        // for each dog, get/store each dog and its position
        for (int i = 0; i < size; i++)
        {
            _dogs[i] = dogList.transform.GetChild(i).GetChild(0).gameObject;
            _dogPositions[i] = dogList.transform.GetChild(i).position;
        }
    }

    #endregion
}