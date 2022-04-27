using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playButtonManager : MonoBehaviour
{
    
    #region Public Methods

    public void SelectDog()
    {
        SceneManager.LoadScene("DogSelection");
    }
    
    #endregion
}