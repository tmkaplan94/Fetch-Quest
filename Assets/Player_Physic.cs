/*
 * Author: Nathaniel
 * Contributors: 
 * Summary: 
 *
 * Description
 * - reference from Unity Documentation of  < CharacterController.OnControllerColliderHit(ControllerColliderHit) > 
 * 
 * Updates
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Physic : MonoBehaviour
{
    [SerializeField]
    float _playerspeed;
    float pushPower = 2.0f;

    private void Start()
    {
        _playerspeed = GetComponent<TempMove_unused>().get_player_speed();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
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

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * pushPower * _playerspeed;
    }
}
