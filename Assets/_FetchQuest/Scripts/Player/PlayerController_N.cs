/**
 * @Author: Shuai ( Nathaniel ) Wang == initial: S.W
 * @Contributor: 
 * @Time:   04/11/2022
 * @version: -
 * 
 * @Update:
 *      1. player movement extend verision: 
 *          add feature jump -> using space to control
 *      2. Jump Feature adding - DetectJump(moveDirection : Vector 3)
 *      
 * @Bug history:
 *      1. None - 04/11/2022
 *      
**/

using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerController_N : MonoBehaviour
{
    #region Debug vaiable
    [SerializeField]
    bool __DebugbyNat = false;
    public int flag_key = 0;
    #endregion

    #region PlayerController Variable Define
    [Header(" PlayerController Variable")]
    [SerializeField]
    float _speed = 8f;
    [SerializeField]
    float _sprintspeed = 10;
    [SerializeField]
    float _energy = 10f;

    Vector3 _movedirection;
    CharacterController _mycharacterController;
    #endregion


    #region Jump Feature Adding
    [Header("Player Jump Features Variable")]
    [SerializeField]
    float _gravity = -0.02f;
    [SerializeField]
    float _jumpStrength = 10f;
    #endregion

    #region Pick up bone variable
    private Transform _bone_transform = null;
    private bool _touchedBone = false;
    [Header("Adjust the bone shifted postion in Players")]
    [SerializeField]
    float shift_position_x = 0f;
    [SerializeField]
    float shift_position_y = 0f;
    [SerializeField]
    float shift_position_z = 2f;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _mycharacterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        
    }


    /// <summary>
    /// Player movement implement
    /// 
    /// </summary>
    void move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        if (_mycharacterController.isGrounded)
        {
            _movedirection = new Vector3(moveX, 0, moveZ);
            _movedirection = transform.TransformDirection(_movedirection);


            if (Input.GetKey(KeyCode.LeftShift) && moveZ == 1)
            {
                _movedirection *= _sprintspeed;
                _energy -= 2f;

            }
            else
            {
                _movedirection *= _speed;
            }

            /**
             * @Author: S.W
             * @Description:
             *  Check the player jump or not in the JumpFeature.cs
             *  get return value of new movedirection, which can directly 
             *  update the move direction
             * @Time: 04/11/2022
             * 
             */
            _movedirection = DetectJump(_movedirection);
        }
        else
        {
            _movedirection.y -= _gravity;
            
        }

        //make sure different frame got same speed;
        Animation walk = this.gameObject.GetComponent<Animation>();
        if (walk != null)
        {
            walk.Play();
        }
        _mycharacterController.Move(_movedirection * Time.deltaTime);

    }



    #region Pickup Bone Implement
    /// <summary>
    /// @Function Description: 
    /// this is interacting with any object
    /// @Method: Collision - gotta check what tag the object has
    /// @Time: 04/11/2022
    /// @Conditions:
    ///     1. if other player,  - do nothing
    ///     2. if item - bone, do the pick up code
    ///     3. if other object,   - do nothing
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        //found the item is bone, then take this bone with player
        if (__DebugbyNat)
        {
            Debug.Log(other.name);
        }

        /**
         * @Author: S.W
         * @Limitation/Warning:
         *  Be carefule, you can only create a prefeb with "bone"
         *  if it's clone, bone (1), and etc will not supportable
         *  
         *  Solution:
         *      1. You can edit your clone name be bone all the time
         *      2. You can change if condition with only other.tag == "Item"
         *      3. create new tage, and change if condition
         * */
        if (other.tag == "Item" && other.name == "bone")
        {
            _bone_transform = other.transform;
            //Set bone's parent is player
            _bone_transform.parent = this.transform;
            Debug.Log("Detected bone");

            //Set bone's local position on Player
            Vector3 _bone_postion = _bone_transform.position;
            _bone_postion = new Vector3(shift_position_x, shift_position_y, shift_position_z);

            //update the bone position compare with player
            _bone_transform.localPosition = _bone_postion;

            //if (__DebugbyNat)
            //{
            //    Debug.Log(_bone_transform.position);
           // }
            
        }
        //else { //might want to interact with other objects
        //}
    }






    /// <summary>
    ///     @Author: S.W
    ///     @Description: if touch the bone, the play will carry the bone together
    ///     @Time: 04/11/222
    ///     @
    /// </summary>
    /// <param name="bone_transform">   get the bone's transform</param>
    /// <param name="player_transform"> get the player's transform</param>
    /// 
    private void carryBone(Transform bone_transform, Transform player_transform)
    {
        
        
        if (__DebugbyNat)
        {
            if (flag_key == 0)
            { 
                Debug.LogWarning("bone_transform.position = " + bone_transform.position);
                Debug.LogWarning("player_transform.position = "  + player_transform.position);
            }
            
        }
       
    }





    #endregion


    #region public function for getting private class property value
    public float getGravity()
    {
        return _gravity;
    }

    public float getJumpStrength()
    {
        return _jumpStrength;
    }

    #endregion



    /// <summary>
    ///     Implement jump feature for player
    ///     
    /// </summary>
    /// <returns>
    ///     return adding Vector 3 adding moving direction
    /// </returns>
    /// 
    private Vector3 DetectJump(Vector3 moveDirection)
    {
        if (_mycharacterController.isGrounded && Input.GetKey(KeyCode.Space))
        {
            moveDirection.y += _jumpStrength;
            return moveDirection;
        }
        else
        {
            return moveDirection;
        }
    }
}