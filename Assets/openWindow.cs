using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openWindow : MonoBehaviour
{
    [SerializeField] GameObject[] _windows;
    [SerializeField] Vector3 endPos;
    [SerializeField] private float _speed;
    private string[] _midDogs = {"BorderCollie(Clone)", "Labrador(Clone)", "Poodle(Clone)", "Bulldog(Clone)" };
    private Coroutine _animationCor;
    private bool isOpen = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered");
        if (!isOpen)
        {
            Debug.Log("isnt open");

                if (other.transform.parent.gameObject.CompareTag("medium"))
                {
                    Debug.Log("booty");
                    Open();
                }
        }
    }

    private void Open()
    {
        isOpen = true;
            if (_animationCor != null)
            {
                StopCoroutine(_animationCor);
            }
            _animationCor = StartCoroutine(OpenSlide());
    }

    private IEnumerator OpenSlide()
    {
        Vector3 startPos = transform.position;
        float time = 0;
        while (time < 1)
        {
            foreach (GameObject _window in _windows)
            {
                float y = Mathf.Lerp(startPos.y, endPos.y, time);
                _window.transform.position = new Vector3(_window.transform.position.x, y, _window.transform.position.z);
                yield return null;
                time += Time.deltaTime * _speed;
            }
        }
    }
}
