/*
 * Author: Brackeys
 * Contributors: Grant Reed
 * Summary: Handles Player Movement
 *
 * Description
 * - Stolen from this video: https://www.youtube.com/watch?v=4HpC--2iowE&list=RDCMUCYbK_tjZ2OrIZFBvU6CCMiA&index=1
 * 
 * Updates
 * - Grant Reed 4/16: updated variables and comments.
 */
using System.Collections;
using UnityEngine;

public class TempMove : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Transform _cam;

    [SerializeField] private float _speed = 6;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _jumpHeight = 3;
    private Vector3 _velocity;
    private bool _isGrounded;

    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private LayerMask _groundMask;

    float _turnSmoothVelocity;
    [SerializeField] private float _turnSmoothTime = 0.1f;

    // Update is called once per frame
    void Update()
    {

        //gravity
        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
        //walk
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _controller.Move(moveDir.normalized * _speed * Time.deltaTime);
        }
    }
}