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
    [SerializeField]
    float pushPower = 2.0f;

    float _playerspeed;
    string _player_type;
    float _range_player;

    enum _player_types {
       small = 0,
       medium = 1,
       big = 2

    }
    [Header("Physic mess setting")]
    [SerializeField]
    float _smallMass_setting = 5.0f;
    float _mediumMass_setting = 10.0f;
    float _bigMass_setting = 15.0f;

    private void Start()
    {
        _playerspeed = GetComponent<TempMove_unused>().get_player_speed();
        _player_type = GetComponent<TempMove_unused>().tag;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        #region detect player type
        if (_player_type == "small")
        {
            _range_player = _smallMass_setting;
        }
        else if (_player_type == "medium")
        {
            _range_player = _mediumMass_setting;
        }
        else if (_player_type == "big")
        {
            _range_player = _bigMass_setting;
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

        //check the player's mass and hit body's mass
        if(_range_player <= body.mass)
        {
            // Calculate push direction from move direction,
            // we only push objects to the sides never up and down
            Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

            // If you know how fast your character is trying to move,
            // then you can also multiply the push velocity by that.

            // Apply the push
            body.velocity = pushDir * pushPower * _playerspeed;

        }
        #endregion
    }
}
