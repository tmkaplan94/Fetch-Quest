/*
 * Author: Nathaniel
 * Contributors: 
 * Summary: 
 *
 * Description
 * - reference from Unity Documentation of  < CharacterController.OnControllerColliderHit(ControllerColliderHit) > 
 * 
 * Updates
 *          1. Player has a physic interaction with level design
 *              @pre-request : the level design object need to have a rigdbody
 *          2. Adding the mass weight setting
 *              @pre-request: dog's model has tags : small, medium, big
 *                                                  range: 5,10, 15
 *          
 */
using System.Collections;
using System.Collections.
    Generic;
using UnityEngine;

public class Player_Physic : MonoBehaviour
{
    enum _player_types
    {
        small = 0,
        medium = 1,
        big = 2

    }

    
    #region private
    float _playerspeed;
    string _player_type;
    float _range_player;
    float _power_Different = 0f;
    #endregion


    #region Physic mess setting Variables
    [Header("Physic mess setting")]

    //# if we want to set Dog rather than Beagle object
    //# then you can set upt this one to be the Dog
    [SerializeField]
    GameObject _player_object;

    // Set for push power when dog and object have the save level 
    [SerializeField]
    float pushPower = 2.0f;


    [SerializeField]
    float _smallMass_setting = 5.0f;
    [SerializeField]
    float _mediumMass_setting = 10.0f;
    [SerializeField]
    float _bigMass_setting = 15.0f;
    #endregion 

    void Start()
    {
        // default is te Beagle Object, the toppest obeject
        //if (_player_object == null)
        //{
        //    _player_object = this.transform.parent.gameObject;
        //}
        
        // The pyshic will depends on the players speed too
        _playerspeed = GetComponent<TempMove_unused>().get_player_speed();

        //get player's tag to know the player type
        //      Extension: Could base on the different clone to do furture in-code changes and detection
        _player_type = this.tag;
       
    }
    void Update()
    {
        
    }

    /**
     * - Control pyshic interaction between player and objects
     * - Base on the player type and object tag to determine
     *      whether dog can push object or not
     * - push power factors: 
     *      1. default push power
     *      2. bigger size have more power on small size object
     *      3. player speed
     **/
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        bool _debug_pyshic_Nate = true;
        
        #region detect player type
        if (_player_type == "small")
        {
            _range_player = _smallMass_setting;
            _power_Different = 0;
        }
        else if (_player_type == "medium")
        {
            _range_player = _mediumMass_setting;
            _power_Different = 1;
        }
        else if (_player_type == "big")
        {
            _range_player = _bigMass_setting;
            _power_Different = 2;
        }
        else {
            return;
        }
        #endregion

        #region hit physic controll
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
        {
            return;
        }

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        if (_debug_pyshic_Nate)
        {
            // Debug.Log(_player_object.name +"\t" + _player_type);
            Debug.Log("player's mass : " + _range_player + "\thit object mass:" + body.mass);
            Debug.Log(this.name + "\t" + this.tag);
            Debug.Log(hit.transform.name + "\t" + hit.transform.tag);
        }

        //check the player's mass and hit body's mass
        if (_range_player >= body.mass)
        {
            // Calculate push direction from move direction,
            // we only push objects to the sides never up and down
            Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

            // If you know how fast your character is trying to move,
            // then you can also multiply the push velocity by that.

            // different size have a different power on same size object
            // Apply the push
            body.velocity = pushDir * (pushPower + _power_Different) * _playerspeed;

        }
        #endregion
    }
}
