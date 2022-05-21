/*
 * Author: Brackeys
 * Contributors:
 * Summary: Handles Player Movement
 *
 * Description
 * - Stolen from this video: https://www.youtube.com/watch?v=4HpC--2iowE&list=RDCMUCYbK_tjZ2OrIZFBvU6CCMiA&index=1
 * 
 * Updates
 * 
 */
using System.Collections;
using UnityEngine;

public class TempMove_unused : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Transform _cam;
    [SerializeField] private Animator _anime;

    [SerializeField] private float _speed = 6;
    [SerializeField] private float _sprintSpeed;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _jumpHeight = 3;
    private Vector3 _velocity;
    private bool _isGrounded;
    private bool _isSprint;

    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private LayerMask _groundMask;

    float _turnSmoothVelocity;
    [SerializeField] private float _turnSmoothTime = 0.1f;
    // Get Player Position for Barking
    Transform _pos;

    void Awake()
    {
        _pos = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed;
        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = _sprintSpeed;
            _isSprint = true;
        }
        else
        {
            speed = _speed;
            _isSprint = false;
        }
        
        //jump
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2 * _gravity);
        }
        //gravity
        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
        //walk
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //animation section -----------------------------------------
        float inputMag;
        if (_isSprint)
        {
            inputMag = Mathf.Clamp01(direction.magnitude);
        }
        else
        {
            inputMag = Mathf.Clamp(direction.magnitude, 0f, 0.5f);
        }
        _anime.SetFloat("Input Mag", inputMag);
        // ----------------------------------------------------------

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }


    public float get_player_speed()
    {
        return _speed;
    }
}