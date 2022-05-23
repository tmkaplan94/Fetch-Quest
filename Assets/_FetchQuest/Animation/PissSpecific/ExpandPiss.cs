using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandPiss : MonoBehaviour
{
    private float initx = .1f, inity = .07f, initz = .02f;
    private float finalx = 1.3f, finaly = .7f, finalz = .3f;

    static float t = 0.0f;

    private void Update()
    {
        //transform.localScale = transform.localScale + new Vector3 (.1f * Time.deltaTime, .1f * Time.deltaTime, .1f * Time.deltaTime);
        transform.localScale = new Vector3(Mathf.Lerp(initx, finalx, t), Mathf.Lerp(inity, finaly, t), Mathf.Lerp(initz, finalz, t));

        t += 0.1f * Time.deltaTime;
    }
}
