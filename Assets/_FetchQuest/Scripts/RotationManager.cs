/*
 * Author: Tyler Kaplan
 * Contributors: Adrian Portillo
 * Summary: Handles the rotations of dogs in DogSelection scene.
 *
 * Description
 * - Rotates every dog in sync either left/right on button press
 * - Rotates currently selected dog constantly
 */
using UnityEngine;

public class RotationManager : MonoBehaviour
{
    #region Private Serialized Fields
    
    [SerializeField] private GameObject dogList;

    [Tooltip("The rotation speed for the currently selected dog, measured in degrees per second.")] [SerializeField]
    private float rotationSpeed;

    #endregion


    #region Private Fields
    
    private Vector3[] _dogPositions;
    private Vector3[] _dogRotations;
    private GameObject[] _dogs;
    private int _currentDogIndex;
    private GameObject _currentDog;
    private GameObject _previousDog;

    #endregion


    #region Properties
    public GameObject CurrentDog => _currentDog;

    #endregion
    

    #region MonoBehavior Callbacks

    private void Awake()
    {
        // initialize dogs, their positions, and set the current dog
        InitializeDogsAndPositions();
        _currentDogIndex = 0;
        _currentDog = _dogs[_currentDogIndex];
    }

    void Update(){

        // rotates the current dog in the selected position
        _currentDog.transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
    }

    #endregion

    
    #region Public Methods

    public void RotateLeft()
    {

        // rotate dog wheel left
        foreach(Transform child in dogList.transform){
            child.gameObject.transform.RotateAround(dogList.transform.position, new Vector3(0,-1,0), 30);
        }

        // get previous dog to reset their rotate value
        _previousDog = _currentDog;
        CorrectOrientationForLeftRotation(_previousDog);
        
        // change current dog
        _currentDogIndex -= 1;
        if (_currentDogIndex < 0)
        {
            _currentDogIndex = dogList.transform.childCount - 1;
        }

        _currentDog = _dogs[_currentDogIndex];
        
    }

    public void RotateRight()
    {
        // rotate dog wheel right
        foreach(Transform child in dogList.transform){
            child.gameObject.transform.RotateAround(dogList.transform.position, new Vector3(0,1,0), 30);
        }

        // get previous dog to reset their rotate value
        // currentDog has not changed yet
        _previousDog = _currentDog;
        CorrectOrientationForRightRotation(_previousDog);

        // change current dog
        _currentDogIndex += 1;
        if (_currentDogIndex > dogList.transform.childCount - 1)
        {
            _currentDogIndex = 0;
        }
        _currentDog = _dogs[_currentDogIndex];
        
    }

    #endregion


    #region Private Methods

    // stores dog positions and rotations
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

    // For right rotates, previous dog needs to be set to a rotateY of 30 to maintain correct orientation
    private void CorrectOrientationForRightRotation(GameObject previousDog){
        previousDog.transform.eulerAngles = new Vector3(0, 30, 0);
    }

    // For left rotates, previous dog needs to be set to a rotateY of -30 to maintain correct orientation
    private void CorrectOrientationForLeftRotation(GameObject previousDog){
        previousDog.transform.eulerAngles = new Vector3(0, -30, 0);
    }

    #endregion
}