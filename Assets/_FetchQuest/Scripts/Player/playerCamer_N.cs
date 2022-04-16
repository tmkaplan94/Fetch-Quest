/**
 * @Author: Shuai ( Nathaniel ) Wang
 * @Contributor: 
 * @Time:   04/11/2022
 * @version: 
 * 
 * @Update:
 *      1. player camera reconstruct for jump feature - 04/11/2022
 *          
 *      2. 
 *      
 * @Bug history:
 *      1. None 
 *      
**/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCamer_N : MonoBehaviour
{
    public float sensitivityOfMouse;
    [SerializeField]
    Transform Player;
    //Transform PlayerArms; 

    float xAxisclasmp;
    bool cursorLocked;

    // Start is called before the first frame update
    void Start()
    {
        sensitivityOfMouse = 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotAmountX = mouseX * sensitivityOfMouse;
        float rotAmountY = mouseY * sensitivityOfMouse;

        xAxisclasmp -= rotAmountY;

        //Vector3 rotArms = PlayerArms.transform.rotation.eulerAngles;
        Vector3 rotPlayer = Player.transform.rotation.eulerAngles;

        rotPlayer.x -= rotAmountY;
        rotPlayer.y += rotAmountX;
        rotPlayer.z = 0;


        if (xAxisclasmp > 90)
        {
            xAxisclasmp = 90;
            rotPlayer.x = 90;
        }
        else if (xAxisclasmp < -90)
        {
            xAxisclasmp = -90;
            rotPlayer.x = 270;
        }


        Player.rotation = Quaternion.Euler(rotPlayer);
        //PlayerArms.rotation = Quaternion.Euler(rotArms);
    }

}
