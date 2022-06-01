using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Door_open : MonoBehaviour
{
    private bool _isOpen = false;
    //private bool _isOpening = false;
    private Vector3 _startRot;
    private Vector3 _forward;
    private float _forwardDir = 0f;
    [SerializeField] private float _openTime;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationAmt;
    [SerializeField] private BoxCollider bColl;

    private string[] _largeDogs = { "RhodesianRidgeback(Clone)", "SaintBernard(Clone)", "GermnShepherd(Clone)", "Doberman(Clone)" };
    
    [SerializeField] private bool isLocked;
    private Coroutine _animationCor;
    public GameObject player;

    private void Awake()
    {
        _startRot = transform.rotation.eulerAngles;
        _forward = transform.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AI"))
            Interact(other.gameObject);
        else if (isLocked)
        {
            float dot = Vector3.Dot(_forward, (other.transform.position - transform.position).normalized);
            if (other.transform.parent.gameObject.CompareTag("big"))
            {
                Debug.Log("booty");
                isLocked = false;
                Interact(other.gameObject);
            }
            else if (dot <= _forwardDir)
            {
                isLocked = false;
                Interact(other.gameObject);
            }
            else
            {
                LevelStatic.currentLevel.questBus.update(new QuestObject(0, "\n\nYou'll have to find another way in!"));
            }
            
        }
        else if (ComparePlayerTag(other.gameObject.tag) || other.CompareTag("AI"))
            Interact(other.gameObject);
    }
    bool ComparePlayerTag(string tag)
    {
        return tag == "small" || tag == "big" || tag == "medium";
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
            float dot = Vector3.Dot(_forward, (openerTransform.position - transform.position).normalized);
            _animationCor = StartCoroutine(OpenRot(dot));
        }
    }

    private IEnumerator OpenRot(float forwardAmount)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;

        if(forwardAmount >= _forwardDir)
        {
            
            endRotation = Quaternion.Euler(new Vector3(0, _startRot.y - _rotationAmt, 0));
        }
        else
        {
            endRotation = Quaternion.Euler(new Vector3(0, _startRot.y + _rotationAmt, 0));
        }
        _isOpen = true;

        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * _speed;
        }
        time = _openTime + Time.time;
        while(Time.time < time)
        {
            yield return null;
        }
        CloseDoor();
    }

    private void CloseDoor()
    {
        if(_isOpen)
        {
            if(_animationCor !=null)
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
