using UnityEngine;

public class Rotate : MonoBehaviour
{
    #region Private Serialized Fields
    
    
    
    #endregion


    #region Private Fields
    
    
    
    #endregion


    #region Properties
    
    
    
    #endregion


    #region MonoBehavior Callbacks
    
    
    
    #endregion


    #region Public Methods

    public void Left()
    {
        transform.Rotate(new Vector3(0, -30, 0));
    }

    public void Right()
    {
        transform.Rotate(new Vector3(0, 30, 0));
    }

    #endregion


    #region Private Methods
    
    
    
    #endregion
}