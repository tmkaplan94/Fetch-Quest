/*
 * Author: Brackeys, Nathaniel
 * Contributors: Grant Reed
 * Summary: Handles Player Movement
 *
 * Description
 * - Some of this code is Stolen from this video: https://www.youtube.com/watch?v=4HpC--2iowE&list=RDCMUCYbK_tjZ2OrIZFBvU6CCMiA&index=1
 * 
 * Updates
 * - Grant Reed 4/15: updated variables and comments.
 * - Grant Reed 4/16: Added Jump from Nathaniel's code and reorganized code/comments.
 */
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Serialized Private Variables
    [SerializeField] private CharacterController _myCharacterController;
    [SerializeField] private Transform _cam;
    [SerializeField] private Animator _anime;

    [SerializeField] private float _speed = 6;
    [SerializeField] private float _sprintspeed = 10;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _jumpStrength = 3;
    [SerializeField] private float _turnSmoothTime = 0.1f;
    #endregion
    #region Private Variables
    private float _turnSmoothVelocity;
    private Vector3 _moveDirection = Vector3.zero;
    #endregion

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        float movementSpeed = _speed;
        
        
        if (_myCharacterController.isGrounded)
        {
            //using temp variable for finding angle of rotation.
            Vector3 moveDirection = new Vector3(moveX, 0f, moveZ).normalized;
            //setting _moveDirection to moveDirection to zero it if there is no input.
            _moveDirection = moveDirection;
            //checking to see if we need to actually move before we call controller.Move()
            if (moveDirection.magnitude >= 0.1f)
            {
                //Getting angle to rotate too based off of player input moveDirection and camera position.
                float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + _cam.eulerAngles.y;
                //smoothing player rotation.
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                //setting vector direction for player movement based on calculated angle.
                _moveDirection  = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                //normalizing direction vector so we can use speed/sprint speed.
                _moveDirection.Normalize();
                //changing the speed if player is pressing shift.
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    movementSpeed = _sprintspeed;
                }
                //applying speed to direction vector.
                _moveDirection *= movementSpeed;
            }

            float inputMag = Mathf.Clamp01(_moveDirection.magnitude);
            _anime.SetFloat("Input Mag", inputMag);
            Debug.Log("Current value of input mag is :" + inputMag);

        //jump / Gravity
        
            if (Input.GetKey(KeyCode.Space))
            {
                _moveDirection.y += _jumpStrength;
            }
        }
        else
        {
            _moveDirection.y -= _gravity;
        }
        //Applying actual movement forward for player.
        _myCharacterController.Move(_moveDirection * Time.deltaTime);
    } 
}