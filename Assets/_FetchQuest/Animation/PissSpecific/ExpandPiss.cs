using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandPiss : MonoBehaviour
{
    private float _currentScale = InitScale;
    private const float TargetScale = 1.1f;
    private const float InitScale = 1f;
    private const int FramesCount = 100;
    private const float AnimationTimeSeconds = 2;
    private float _deltaTime = AnimationTimeSeconds/FramesCount;
    private float _dx = (TargetScale - InitScale)/FramesCount;
    private bool _upScale = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(goBigger());
    }

    private IEnumerator goBigger()
    {
        while (true)
        {
            while (_upScale)
            {
                _currentScale += _dx;
                if (_currentScale > TargetScale)
                {
                    _upScale = false;
                    _currentScale = TargetScale;
                }
                transform.localScale = Vector3.one * _currentScale;
                yield return new WaitForSeconds(_deltaTime);
            }
        }
    }
}
