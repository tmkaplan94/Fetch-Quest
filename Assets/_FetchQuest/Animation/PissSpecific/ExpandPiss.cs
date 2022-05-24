using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandPiss : MonoBehaviour
{
    private float initx = .1f, inity = .07f, initz = .02f;
    private float finalx = 1.3f, finaly = .7f, finalz = .3f;

    private float tx = 0.0f, ty = 0.0f, tz = 0.0f;

    private float _timeRemaining = 1f;
    public bool spotted = false;

    private void Update()
    {
        _timeRemaining -= Time.deltaTime;
        if ( _timeRemaining <= 0)
        {
            transform.localScale = new Vector3(Mathf.Lerp(initx, finalx, tx), Mathf.Lerp(inity, finaly, ty), Mathf.Lerp(initz, finalz, tz));

            tx += 0.1f * Time.deltaTime;
            ty += 0.1f * Time.deltaTime;
            tz += 0.05f * Time.deltaTime;
        }
        
    }
}
