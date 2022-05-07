using Photon.Pun;
using UnityEngine;

public class NetworkStart : MonoBehaviour
{
    [SerializeField] PhotonView _view;
    [SerializeField] TempMove_unused _movementScript;
    [SerializeField] private GameObject _camera1;
    [SerializeField] GameObject _camera2;

    // Start is called before the first frame update
    void Start()
    {
        if (!_view.IsMine)
        {
            _movementScript.enabled = false;
            Destroy(_camera1);
            Destroy(_camera2);
        }
    }
}