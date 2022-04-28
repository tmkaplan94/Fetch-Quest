/*
 * Author: Tyler Kaplan
 * Contributors: Adrian
 * Summary: Deals with all dog rotations in DogSelection scene
 */
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RotationManager : MonoBehaviour
{
    #region Private Serialized Fields
    
    [SerializeField] private GameObject dogList;
    [SerializeField] private TextMeshProUGUI dogNameText;
    [SerializeField] private TextMeshProUGUI dogSizeText;
    [SerializeField] private Transform audioPos;
    
    #endregion


    #region Private Fields
    
    private Vector3[] _dogPositions;
    private Vector3[] _dogRotations;
    private GameObject[] _dogs;
    private GameObject _currentDog;
    private GameObject _previousDog;
   

    #endregion

    public static int _currentDogIndex;

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
        AudioManager.Instance.PlaySFX(AudioNames.click, audioPos.position);
        // rotate dog wheel left
        //Debug.Log("RotateLeft()");
        //dogList.transform.Rotate(new Vector3(0, -30, 0));
        foreach(Transform child in dogList.transform){
            child.gameObject.transform.RotateAround(dogList.transform.position, new Vector3(0,-1,0), 30);
        }
        
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
        changeUIText();
        
    }

    public void RotateRight()
    {
        AudioManager.Instance.PlaySFX(AudioNames.click, audioPos.position);
        // rotate dog wheel right

        //children rotate with parent
        //dogList.transform.Rotate(new Vector3(0, 30, 0));

        //children rotate around parent
        //each child rotates around parent 30 degrees
        foreach(Transform child in dogList.transform){
            child.gameObject.transform.RotateAround(dogList.transform.position, new Vector3(0,1,0), 30);
        }

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
        changeUIText();

    }

    #endregion

    #region Private Methods

    //changes text of DogNameText UI element to match the currently selected dog
    //changes text of DogSizeText UI element to match the currently selected dog
    private void changeUIText(){

        dogNameText.text = _currentDog.name;

        if(_currentDog.name == "BORDER COLLIE"){
            dogSizeText.text = "large";
        }

        if(_currentDog.name == "GERMAN SHEPARD"){
            dogSizeText.text = "large";
        }

        if(_currentDog.name == "DOBERMAN"){
            dogSizeText.text= "large";
        }

        if(_currentDog.name == "SAINT BERNARD"){
            dogSizeText.text = "large";
        }

        if(_currentDog.name == "LABRADOR"){
            dogSizeText.text = "mid";
        }

        if(_currentDog.name == "POODLE"){
            dogSizeText.text = "mid";
        }
  
        if(_currentDog.name == "LABRADOR"){
            dogSizeText.text = "mid";
        }

        if(_currentDog.name == "BRITISH BULLDOG"){
            dogSizeText.text = "mid";
        }

        if(_currentDog.name == "RHODESIAN RIDGEBACK"){
            dogSizeText.text = "mid";
        }

        if(_currentDog.name == "PUG"){
            dogSizeText.text = "small";
        }

        if(_currentDog.name == "CHIHUAHUA"){
            dogSizeText.text = "small";
        }

        if(_currentDog.name == "BEAGLE"){
            dogSizeText.text = "small";
        }
        
        if(_currentDog.name == "BULLTERRIER"){
            dogSizeText.text = "small";
        }
    }


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