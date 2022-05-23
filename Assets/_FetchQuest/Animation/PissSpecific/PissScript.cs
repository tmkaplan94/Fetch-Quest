/*
 * Author: Alex
 * Contributors: wut
 * Summary: creates piss stream
 *
 * Description
 * - it's to handle the pee pee stream
 * 
 * Updates
 * - Alex Pham 5/21 - created this piece of shit
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PissScript : MonoBehaviour
{

    private LineRenderer rendLine = null;
    private Vector3 targetPosition = Vector3.zero;

    private ParticleSystem splash = null;
    private Coroutine pissRoutine = null;

    private void Awake() 
    {
        rendLine = GetComponent<LineRenderer>();
        splash = GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        MoveToPosition(0, transform.position);
        MoveToPosition(1, transform.position);
    }

    public void Begin()
    {
        StartCoroutine(UpdateParticles());
        pissRoutine = StartCoroutine(BeginPiss());
    }

    private IEnumerator BeginPiss()
    {
        yield return new WaitForSeconds(1);
        if (gameObject.activeSelf)
        {
            targetPosition = FindFloor();

            MoveToPosition(0, transform.position);
            MoveToPosition(1, targetPosition);

            yield return null;
        }
    }

    public void End()
    {
        StartCoroutine(waitToEndPP());
        StopCoroutine(waitToEndPP());
    }
    private IEnumerator waitToEndPP()
    {
        //yield return new WaitForSeconds(1);
        StopCoroutine(pissRoutine);
        pissRoutine = StartCoroutine(EndPiss());
        yield return null;
    }

    private IEnumerator EndPiss()
    {
        while (!HasReachedPos(0, targetPosition))
        {
            AnimateToPos(0, targetPosition);
            AnimateToPos(1, targetPosition);

            yield return null;
        }

        Destroy(gameObject);
    }

    private Vector3 FindFloor()
    {
        RaycastHit hit;
        GameObject pissSpot = GameObject.Find("PissSpot");
        Vector3 dir = (pissSpot.transform.position - transform.position).normalized;
        Ray ray = new Ray(transform.position, dir);

        Physics.Raycast(ray, out hit, 2.0f);
        Vector3 endpoint = hit.collider ? hit.point : ray.GetPoint(10.0f);

        return endpoint;
    }

    private void MoveToPosition(int index, Vector3 targetPosition)
    {
        rendLine.SetPosition(index, targetPosition);
    }

    private void AnimateToPos(int index, Vector3 targetPosition)
    {
        Vector3 currentPoint = rendLine.GetPosition(index);
        Vector3 newPos = Vector3.MoveTowards(currentPoint, targetPosition, Time.deltaTime * 1.75f);
        rendLine.SetPosition(index, newPos);
    }

    private bool HasReachedPos(int index, Vector3 targetPosition)
    {
        Vector3 currentPos = rendLine.GetPosition(index);
        return currentPos == targetPosition;
    }

    private IEnumerator UpdateParticles()
    {
        while (gameObject.activeSelf)
        {
            splash.gameObject.transform.position = targetPosition;

            bool isHitting = HasReachedPos(1, targetPosition);
            splash.gameObject.SetActive(isHitting);

            yield return null;
        }
    }
}
