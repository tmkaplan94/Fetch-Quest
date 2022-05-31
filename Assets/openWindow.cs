using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openWindow : MonoBehaviour
{
    [SerializeField] GameObject[] _windows;
    [SerializeField] Vector3 endPos;
    [SerializeField] private float _speed;
    private Coroutine _animationCor;
    private bool isOpen = false;
    [SerializeField] private bool needItem = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isOpen)
        {
            if (!needItem)
            {
                if (other.transform.parent.gameObject.CompareTag("medium"))
                {
                    Debug.Log("booty");
                    Open();
                }
            }
            else
            {
                if(ComparePlayerTag(other.tag))
                {
                    if (other.TryGetComponent<PickUpSystem>(out PickUpSystem pS))
                    {
                        if (pS.GetItem().CompareTag("IDCard"))
                        {
                            Open();
                            LevelStatic.currentLevel.questBus.update(new QuestObject(300, "Found the hidden Room!", LevelData.publicEvents.NOEVENT, "", true, "Ultra Mega Rare"));
                        }
                    }
                    else
                    {
                        LevelStatic.currentLevel.questBus.update(new QuestObject(0, "Need an ID Card!", LevelData.publicEvents.NOEVENT));
                    }
                }
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
        
        float time = 0;
        while (time < 1)
        {
            foreach (GameObject _window in _windows)
            {
                Vector3 startPos = _window.transform.position;
                float y = Mathf.Lerp(startPos.y, endPos.y, time);
                _window.transform.position = new Vector3(_window.transform.position.x, y, _window.transform.position.z);
                yield return null;
                time += Time.deltaTime * _speed;
            }
        }
    }
    bool ComparePlayerTag(string tag)
    {
        return tag == "small" || tag == "big" || tag == "medium";
    }
}
