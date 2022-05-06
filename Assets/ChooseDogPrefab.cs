using Photon.Pun;
using UnityEngine;

public class ChooseDogPrefab : MonoBehaviour
{
    [SerializeField] private GameObject[] dogPrefab;
    private int dogIndex;

    // Start is called before the first frame update
    void Start()
    {
        // I know this are a little overkill, but Rider was bugging me...
        Transform dogTransform = transform;
        Vector3 dogPosition = dogTransform.position;
        Quaternion dogRotation = dogTransform.rotation;
        
        dogIndex = RotationManager._currentDogIndex;
        Instantiate(dogPrefab[dogIndex], dogPosition, dogRotation);
        //PhotonNetwork.Instantiate(dogPrefab[dogIndex].name, dogPosition, dogRotation);
    }
    
}
