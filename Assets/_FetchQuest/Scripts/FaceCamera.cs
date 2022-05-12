using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceCamera : MonoBehaviour
{
    public Transform mLookat;

    private Transform localTrans;
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMesh text;

    void Start()
    {
        localTrans = GetComponent<Transform>();
    }

    
    void Update()
    {
        if (mLookat)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                panel.SetActive(true);
                localTrans.LookAt(2 * localTrans.position - mLookat.position);
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                panel.SetActive(false);
            }
        }
        
        {
            localTrans.LookAt(2 * localTrans.position - mLookat.position);
        }
    }
}
