using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatch_open : MonoBehaviour
{

    private bool _isOpen = false;
    private Vector3 _startRot;

    [SerializeField] private float _openTime;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationAmt;

    private Coroutine _animationCor;
    public GameObject player;

    private void Awake()
    {
        _startRot = transform.rotation.eulerAngles;
    }

    private void OnTriggerEnter(Collider other)
    {
        Interact(other.gameObject);
    }

    public void Interact(GameObject interacter)
    {
        OpenDoor(interacter.transform);
    }

    private void OpenDoor(Transform openerTransform)
    {
        if (!_isOpen)
        {
            if (_animationCor != null)
            {
                StopCoroutine(_animationCor);
            }
            _animationCor = StartCoroutine(OpenRot());
        }
    }

    private IEnumerator OpenRot()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(new Vector3(_rotationAmt, 0, 0));

        _isOpen = true;

        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * _speed;
        }
        time = _openTime + Time.time;
        while (Time.time < time)
        {
            yield return null;
        }
        CloseDoor();
    }
    private void CloseDoor()
    {
        if (_isOpen)
        {
            if (_animationCor != null)
            {
                StopCoroutine(_animationCor);
            }

            _animationCor = StartCoroutine(CloseRot());
        }
    }
    private IEnumerator CloseRot()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(_startRot);

        _isOpen = false;
        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * _speed;
        }

    }
}
