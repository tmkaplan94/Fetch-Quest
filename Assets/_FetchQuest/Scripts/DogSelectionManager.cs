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


    void Update(){
        if(Input.GetKeyDown("return")){
            SelectDog();    
        }
    }

    public void SelectDog()
    {
        AudioManager.Instance.PlaySFX(AudioNames.click, audioPos.position);
        SceneManager.LoadScene("Grant");
    }

    public void GoHome(){
        AudioManager.Instance.PlaySFX(AudioNames.click, audioPos.position);
        SceneManager.LoadScene("MainMenu");
    }
    
    #endregion


    #region Private Methods
    
    
    
    #endregion
}