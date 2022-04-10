
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
        float gravity = 1f;
        float jumpSpeed = 10f;

    float energy = 10f;


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
        move();
    }



    private void FlashlightToggle()
    {
        
    }

    /// <summary>
    /// move
    /// </summary>
    void move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        if (mycontroller.isGrounded)
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

        }

        movedirection.y -= gravity;

        Animation walk = this.gameObject.GetComponent<Animation>();
        if (walk != null)
        {
            walk.Play();
        }
        mycontroller.Move(movedirection * Time.deltaTime);

    }


    private void OnTriggerExit(Collider other)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
    }

}