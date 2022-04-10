
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerControllerNathaniel : MonoBehaviour
{

    [Header("PlayerController Variable Define")]
        [SerializeField]
        float speed = 8f;
        float sprintspeed = 10f;
        float gravity = -1f;
        float jumpSpeed = 10f;

    float energy = 10f;

    public Transform cam;
    Vector3 camY;

    Vector3 movedirection;
    CharacterController mycontroller;

    // Start is called before the first frame update
    void Start()
    {
        mycontroller = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        camY = new Vector3(cam.InverseTransformPoint(cam.position).x, 0, cam.InverseTransformPoint(cam.position).z);
        cam.transform.position = transform.position;
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            transform.LookAt(cam.position + (cam.right * Input.GetAxis("Horizontal") + cam.forward * Input.GetAxis("Vertical")));
            move(Mathf.Sqrt(Mathf.Pow(Input.GetAxis("Horizontal"),2) + Mathf.Pow(Input.GetAxis("Vertical"),2)));
        }
        else
        {
            mycontroller.Move(new Vector3(0, gravity, 0));
        }
    }



    private void FlashlightToggle()
    {
        
    }

    /// <summary>
    /// move
    /// </summary>
    void move(float momentum)
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        /*if (mycontroller.isGrounded)
        {
            movedirection = new Vector3(moveX, 0, moveZ);
            movedirection = transform.TransformDirection(movedirection);


            if (Input.GetKey(KeyCode.LeftShift) && moveZ == 1)
            {
                movedirection *= sprintspeed;
                energy -= 2f;

            }
            else
            {
                movedirection *= speed;
            }

        }*/

        movedirection.y -= gravity;
        movedirection = cam.forward * moveZ + cam.right * moveX;

        Animation walk = this.gameObject.GetComponent<Animation>();
        if (walk != null)
        {
            walk.Play();
        }
        mycontroller.Move(new Vector3(movedirection.x * speed * Time.deltaTime, gravity, movedirection.z * speed * Time.deltaTime));
        //mycontroller.Move(new Vector3((transform.forward.x * momentum * speed * Time.deltaTime), movedirection.y, (transform.forward.z * momentum * speed * Time.deltaTime)));

    }


    private void OnTriggerExit(Collider other)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
    }

}