/*
 * Author: Tyler Kaplan
 * Contributors: 
 * Summary: not implemented yet
 *
 * Description
 * - 
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class DogSelectionManager : MonoBehaviour
{

    #region Private Fields

    [SerializeField] private Transform audioPos;
    
    #endregion


    #region Properties
    
    
    
    #endregion


    #region MonoBehavior Callbacks
    
    

    #endregion


    #region Public Methods

    public void SelectDog()
    {
        AudioManager.Instance.PlaySFX(AudioNames.click, audioPos.position);
        SceneManager.LoadScene("Grant");
    }
    
    #endregion


    #region Private Methods
    
    
    
    #endregion
}