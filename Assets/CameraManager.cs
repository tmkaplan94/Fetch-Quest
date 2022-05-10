using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
  

    public CinemachineVirtualCamera currentCamera;
    

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        CinemachineTrackedDolly dolly = currentCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        dolly.m_PathPosition = dolly.m_PathPosition + (0.4f * Time.deltaTime);

    }


}