/**
 * @Author: Shuai ( Nathaniel ) Wang
 * @Contributor: 
 * @Time:   04/11/2022
 * @version: -
 * 
 * @Update:
 *      1. player movement extend verision: 
 *          add feature jump -> using space to control
 *      2. 
 *      
 * @Bug history:
 *      1. None - 04/11/2022
 *      
**/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpFeature : MonoBehaviour
{
    #region private member for JumpFeatures
    CharacterController _mycharacterController;
    PlayerController_N pc_script;

    private float _pc_scriptJumpStrength = 0f;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    ///     Implement jump feature for player
    ///     
    /// </summary>
    /// <returns>
    ///     return adding Vector 3 adding moving direction
    /// </returns>
    /// 
    Vector3 DetectJump(Vector3 moveDirection)
    {
        if (_mycharacterController.isGrounded && Input.GetKey(KeyCode.Space))
        {
            Vector3 _addMoveDirection = new Vector3(0f, _pc_scriptJumpStrength, 0f);
            return moveDirection + _addMoveDirection;
        }
        else {
            return moveDirection;
        }
    }
}
