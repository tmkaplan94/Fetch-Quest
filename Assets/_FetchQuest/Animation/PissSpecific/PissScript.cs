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

    private void Awake() 
    {
        rendLine = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        MoveToPosition(0, transform.position);
        MoveToPosition(1, transform.position);
    }

    public void Begin()
    {
        StartCoroutine(BeginPiss());
    }

    private IEnumerator BeginPiss()
    {
        while (gameObject.activeSelf)
        {
            targetPosition = FindFloor();

            MoveToPosition(0, transform.position);
            MoveToPosition(1, targetPosition);

            yield return null;
        }
    }

    private Vector3 FindFloor()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);

        Physics.Raycast(ray, out hit, 2.0f);
        Vector3 endpoint = hit.collider ? hit.point : ray.GetPoint(2.0f);

        return endpoint;
    }

    private void MoveToPosition(int index, Vector3 targetPosition)
    {
        rendLine.SetPosition(index, targetPosition);
    }
}
