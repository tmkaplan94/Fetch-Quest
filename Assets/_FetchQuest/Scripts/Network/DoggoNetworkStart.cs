using Photon.Pun;
using UnityEngine;

public class DoggoNetworkStart : MonoBehaviour
{
    [SerializeField] PhotonView _view;
    [SerializeField] TempMove_unused _movementScript;
    [SerializeField] private GameObject _camera1;
    [SerializeField] GameObject _camera2;
    [SerializeField] private Interect _interect;
    [SerializeField] private Player_Physic _physics;
    [SerializeField] private PickUpSystem _pickUpSystem;

    private void Awake()
    {
        if (FindObjectOfType<NetworkManager>() == null)
        {
            GetComponent<DoggoNetworkStart>().enabled = false;
        }
    }

    void Start()
    {
        if (!_view.IsMine)
        {
            _interect.enabled = false;
            _physics.enabled = false;
            _pickUpSystem.enabled = false;
            _movementScript.enabled = false;
            Destroy(_camera1);
            Destroy(_camera2);
        }
    }
}