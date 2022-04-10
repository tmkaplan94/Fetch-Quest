/*
 * Author: Grant Reed
 * Contributors: Tyler Kaplan
 * Summary: This should be a brief description.
 *
 * Description
 * - This is for more details if necessary.
 * - Also, let's keep any notes here as well.
 *
 * Updates
 * - Loc Trinh 4/10: added additional functionality
 */

using UnityEngine;

public class CodingConventions : MonoBehaviour
{
    // USE ACCESS MODIFIERS FOR ALL NON-LOCAL FIELDS/METHODS
    
    #region Private Serialized Fields

    [Tooltip("This can be helpful in the editor for those not familiar with your code." +
             "It's also useful, because it comments itself.")]
    [SerializeField] private string thisCanBeModifiedInEditor;

    #endregion


    #region Private Fields

    // fields should have an underscore and be in camelCase
    private string _thisIsUsedByTheClass;
    private Transform _alsoForCachingNeededComponents;

    #endregion

    
    #region MonoBehavior Callbacks

    // comments should be like this
    // Or like this, doesn't matter.
    private void Start()
    {
        _thisIsUsedByTheClass = "Initialize variables here if necessary";
        _alsoForCachingNeededComponents = GetComponent<Transform>();
    }

    // be careful what functions you put in Update()
    private void Update()
    {
        ExpensiveFunction();
    }
    
    // functions that rely on the physics engine (rigidbodies) should be here
    private void FixedUpdate()
    {
        UsesPhysicsEngine();
    }

    #endregion


    #region Public Methods

    // functions should have a brief description and be PascalCase !
    public void PascalCase()
    {
        Debug.Log("Functions should be PascalCase");
    }

    #endregion


    #region Private Methods

    // functions should have a brief description and be PascalCase !
    private void ExpensiveFunction()
    {
        // this is an expensive function
        Debug.Log("I am expensive! Don't put me in Update() unless absolutely necessary");
    }

    // functions should have a brief description
    private void UsesPhysicsEngine()
    {
        Debug.Log("I should be put in FixedUpdate()");

        // I don't need an access modifier
        string temp = "temporary";
    }

    #endregion
}