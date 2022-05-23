using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private int ballCount;
    private bool loss = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Billiard"))
        {
            if (other.gameObject.name == "WhiteBall")
                return;
            if (other.gameObject.name == "8Ball")
            {
                if (ballCount == 9 && !loss)
                    Win();
                else
                    Lose();
            }
            ballCount++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Billiard"))
        {
            ballCount--;
            if (ballCount == 0)
                loss = false;
        }
    }
    private void Win()
    {

    }
    private void Lose()
    {
        loss = true;
    }
}
